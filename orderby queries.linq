<Query Kind="Expression">
  <Connection>
    <ID>10398237-b90a-42ab-92b0-8354a2c46fb3</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WB320-12\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Sorting

//there is a significant difference between query syntax and method syntax

//query syntax is much like SQL
//orderby field {[ascending]|descending} [,field....]

//ascending is the default option

//method syntax is a series of individual methods
//.Orderby(x => x.field) firstfield only
//.OrderByDescending(x => x.field)first field only
//.ThenBy(x => x.field) each following field
//.ThenByDescending(x => x.field)each following field






//Find all of the Album Tracks for the band Queen. order the Track by the track name alphabatically

//query syntax
from x in Tracks
where x.Album.Artist.Name.Contains("Queen")
orderby x.AlbumId, x.Name 
select x

//method Syntax
Tracks
.Where(x => x.Album.Artist.Name.Contains("Queen"))
.OrderBy(x => x.Album.Title)
.ThenBy(x => x.Name)

//order of sorting and filtering can be interchanged
Tracks
.OrderBy(x => x.Album.Title)
.ThenBy(x => x.Name)
.Where(x => x.Album.Artist.Name.Contains("Queen"))

