Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Http.Controllers
Imports System.Xml

Public Class startupController
    Inherits ApiController

    Dim context As MobileServiceContext

    Protected Overrides Sub Initialize(ByVal controllerContext As HttpControllerContext)
        MyBase.Initialize(controllerContext)
        context = New MobileServiceContext()
        context.Configuration.AutoDetectChangesEnabled = False
    End Sub

    ' POST api/startup
    Public Async Function POST(request As startupRequest) As Task(Of startupResponse)
        Dim response As New startupResponse
        response.filterElements = New List(Of elementResult)


        'Dim sqlStatement As String = "
        'select e.Id as Id
        '     , e.name as name
        '     , e.orderSeq as orderSeq
        '     , e.displayName as display
        '   , e.displayType as displayType
        '  , eI.[resource] as elementResource
        '  , ei.filterNum as filterNumber
        '  , ei.filterPosition as filterPosition
        '  from emptyT.element e inner join 
        '       emptyT.elementItems eI on e.Id = eI.elementId
        ' where e.deleted = 0 and ei.deleted = 0
        ' order by e.orderSeq
        '"
        Dim sqlStatement As String = "
select 
(select c.id as ""@id"", c.username as ""@username"",
	(select cp.setting as ""@name"", cp.value as ""@value""
	   from emptyt.consumerProperties cp
	  where cp.consumerId = c.id
	   for XML path('property'), type, root('properties')),
	 (select v.visualswitch as ""@visualSwitch"", cvo.[value] as ""@value""
	    from emptyT.visuals v inner join
		     emptyT.consumerVisualOption cvo on v.Id = cvo.visualId
	   where cvo.consumerId = c.id
	     for xml path('visual'), type, root('visuals'))
  from emptyT.consumer c 
 where c.username = '" + request.user + "' 
        For xml path('consumer'), type, root('consumers')),
  (select e.name as ""@name"", e.orderSeq as ""@orderSeq"", e.displayName as ""@displayName""
        , rtrim(e.displayType) as ""@displayType"", 
	(select ei.value as ""@value"", ei.filterNum As ""@filterNum"", ei.filterPosition As ""@filterPos""
	   from emptyT.elementItems ei
	  where ei.elementId = e.Id
	    and ei.Deleted = 0
		for xml PATH('value'), type, root('values'))
    from emptyT.element e
   where e.Deleted = 0
     for XML path('element'), type, root('filter'))
for XML path('result')
"

        Dim a As New XmlDocument
        Dim b As List(Of String)
        Dim b1 As String = ""
        b = Await context.Database.SqlQuery(Of String)(sqlStatement).ToListAsync()
        For Each t As String In b
            b1 += t
        Next
        Dim c As Linq.XDocument = Linq.XDocument.Parse(b1)

        response.filterElements = (From _filterElement In c.Element("result").Element("filter").Elements("element")
                                   Select New elementResult With {
                                       .name = _filterElement.Attribute("name").Value _
                                       , .orderSeq = _filterElement.Attribute("orderSeq").Value _
                                       , .display = _filterElement.Attribute("displayName").Value _
                                       , .displayType = _filterElement.Attribute("displayType").Value _
                                       , .values = (From _value In _filterElement.Element("values").Elements("value")
                                                    Select New elementValue With {
                                                        .value = _value.Attribute("value"),
                                                        .filterNum = Byte.Parse(_value.Attribute("filterNum")),
                                                        .filterPos = Byte.Parse(_value.Attribute("filterPos"))}).ToList()
                                    }).ToList()



        response.consumerProperties = (From _consumerProperty In c.Element("result").Element("consumers").Element("consumer").Element("properties").Elements("property")
                                       Select New propertyTO With {
                                                .setting = _consumerProperty.Attribute("name").Value,
                                                .value = _consumerProperty.Attribute("value").Value
                                            }).ToList()

        response.visualProperties = (From _visualProperty In c.Element("result").Element("consumers").Element("consumer").Element("visuals").Elements("visual")
                                     Select New propertyTO With {
                                                .setting = _visualProperty.Attribute("visualSwitch").Value,
                                                .value = _visualProperty.Attribute("value").Value
                                            }).ToList()


        Return response

    End Function

End Class