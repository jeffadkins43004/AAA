Imports System
Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Http.Controllers
Imports Microsoft.WindowsAzure.Mobile.Service
Imports System.Linq
Imports System.Collections.Generic

Public Class DealAPIController
    Inherits ApiController

    Public Property Services As ApiServices

    Dim context As MobileServiceContext

    Protected Overrides Sub Initialize(ByVal controllerContext As HttpControllerContext)
        MyBase.Initialize(controllerContext)
        context = New MobileServiceContext()
        context.Configuration.AutoDetectChangesEnabled = False
    End Sub

    ' Get api/LatLong
    Public Async Function POST(request As LatLonRequest) As Task(Of DealResults)
        Try

            Dim listDealsn As List(Of DealResult)
            Dim mDealResults As New DealResults
            'listDealsn =
            '    (From d In context.Deals
            '     Join m In context.MerchItems On d.MerchId Equals m.Id
            '     Join mc In context.MerchCityItem On m.Id Equals mc.MerchID
            '     Join c In context.CityItems On c.Id Equals mc.CityID
            '     Select New DealResult With {
            '                    .dealId = d.Id,
            '                    .pct = d.PctOff,
            '                    .name = m.Name,
            '                    .merchId = m.Id,
            '                    .totalAvailable = d.TotalAvailable,
            '                    .endDt = d.EndDate}).ToList()
            Dim sqlStatement As String =
                "select m.Name
                      , d.pctOff as pct
                      , d.totalAvailable
                      , d.Id as dealId
                      , m.Id as merchId
                      , d.endDate as endDt
                      , ma.address1 as address1
                      , ma.address2 as address2
                      , ma.city as city
                      , ma.state as state
                      , ma.postal as postal
                      , cast(substring(dbo.Split(ma.location.ToString(), ' ',2),2,len(dbo.Split(ma.location.ToString(), ' ',2))) as float) as longitude
                      , cast(reverse(substring(reverse(dbo.Split(ma.location.ToString(), ' ',3)),2,len(dbo.Split(ma.location.ToString(), ' ',3)))) as float) as latitude
                      , ma.location.ToString() as location
                      , CAST(ma.location.STDistance('POINT(" + request.Longitude.ToString + " " + request.Latitude.ToString() + ")') as float) as distanceMeters
                 from emptyT.MerchArea ma inner join
                      emptyT.MerchItems m on ma.MerchID = m.Id inner join
                      emptyT.DealItem d on d.merchId = m.id
                 where location is not null
                 order by distanceMeters"

            listDealsn = context.Database.SqlQuery(Of DealResult)(sqlStatement).ToList()

            mDealResults.results = listDealsn

            Return mDealResults

        Catch ex As Exception
            Dim aa As String = "aa"
            Return Nothing
        End Try


    End Function

End Class