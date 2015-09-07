select 
(select c.id as "@id", c.username as "@username",
	(select cp.setting as "@name", cp.value as "@value"
	   from emptyt.consumerProperties cp
	  where cp.consumerId = c.id
	   for XML path('property'), type, root('properties')),
	 (select v.visualswitch as "@visualSwitch", cvo.[value] as "@Value"
	    from emptyT.visuals v inner join
		     emptyT.consumerVisualOption cvo on v.Id = cvo.visualId
	   where cvo.consumerId = c.id
	     for xml path('visual'), type, root('visuals'))
  from emptyT.consumer c 
 where c.username = 'jeff' 
 for xml path('consumer'), type, root('consumers')),
  (select e.name as "@name", e.orderSeq as "@orderSeq", e.displayName as "@displayName"
        , rtrim(e.displayType) as "@displayType", 
	(select ei.value as "@value", ei.filterNum as "@filterNum", ei.filterPosition as "@filterPost"
	   from emptyT.elementItems ei
	  where ei.elementId = e.Id
	    and ei.Deleted = 0
		for xml PATH('Value'), type, root('values'))
    from emptyT.element e
   where e.Deleted = 0
     for XML path('element'), type, root('filter'))
for XML path('result')