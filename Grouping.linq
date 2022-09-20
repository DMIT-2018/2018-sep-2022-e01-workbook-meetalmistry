<Query Kind="Expression">
  <Connection>
    <ID>e69b3623-7543-4e6e-a932-1ba4f3208a62</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WC320-03\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Grouping

//When you create a group it builds two componanents
//a) Key component (deciding criteria values(S)) defining the group
//   reference this component using the groupname.Key[.propertyname]
//   1 value for Key:groupname.Key
//	 n values for Key:groupname.Key.propertyname
//(property < - > field < - >attribute < - > value )
//b) data of he group (raw instances of the collection)

//ways to group 
//a) by a single colummn (field, attribute, property) groupname.Key
//b) by a set of columns (anonymous dataset)          groupname.Key.Property
//c) by use an entity (entity name / navproperty)	  groupname.Key.Property

//concept processing 
//start with a "pile " of data (original collection priot to grouping)
//specify the groupng proprty or properties 
//result of the group of the operation will be to "place the data into smaller piles"
//the piles are dependant on the grouping property(ies) value(s)
//the grouping  property(ies) become the Key
//the individul instances are the data in the smaller piles
//the entire individul instancesof the original collection is place in the smaller piles
//manupulate each of the "smaller piles" using your linq commands

//grouping is differnent than the ordering
//ordering is the final resequencing of the collection for display
//grouping re-organizes a collection inti sepearate, usually smaller collection for further processing (ie aggregates)

//Grouping is an excellent way to organize your data especially if you need to process a data on a prop that is"NOT" a relative Key such 
//as a foreign Key which forms a natural group using the navigational prop.

//Dispay albums by ReleaseYear
//this request does not need grouping
//this request is an ordering of output : OrderBy
//this ordering affects only display

Albums
.OrderBy(a => a.ReleaseYear)

//Display Albums grouped by releaseyear
//eplicit request to breakup thr display into the desire "piles"

Albums 
 .GroupBy (a => a.ReleaseYear)
 
//processing ont he groups created by the Group command

//Display the number of Albums produced each year

//List only the years which has more then 10 Albums 

Albums 
 .GroupBy (a => a.ReleaseYear)
 .Where(egP => egP.Count() > 10) // filtering against each gropu pile
 .Select (eachgroupPile => new 
   {
     year = eachgroupPile.Key,//we are using this key for our next sorting of filtering 
	 NumOfAlbums = eachgroupPile.Count()
	
	})
 //.Where(x => x.NumOfAlbums > 10) // filtering against the output of the .Select() command
 
 //use a multiple set of properties to form the group
 //included a nested query to report on the small pile group
 
 //Display Album groups by releaseLabel, releaseyear. Display the releaseYear and number of Albums. List only thr year with 3 or more Album released.
 //For each Album display the title artist , num of Tracks on the Album and releaseYear
 
Albums 
 .GroupBy (a => new {a.ReleaseLabel, a.ReleaseYear})
 .Where(egP => egP.Count() > 2) // filtering against each gropu pile
 .Select (eachgroupPile => new 
   {
     Label = eachgroupPile.Key.ReleaseLabel,
     year = eachgroupPile.Key.ReleaseYear,//we are using this key for our next sorting of filtering 
	 NumOfAlbums = eachgroupPile.Count(),
	 AlbumItems = eachgroupPile
	 				.Select ( egPInstance => new 
						{
							title = egPInstance.Title,
							artist = egPInstance.Artist.Name,
							trackcount = egPInstance.Tracks.
							               Select (x => x ),
							YearOfAlbums = egPInstance.ReleaseYear
						})
	
	})

































