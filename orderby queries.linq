<Query Kind="Expression">
  <Connection>
    <ID>54bf9502-9daf-4093-88e8-7177c12aaaaa</ID>
    <NamingService>2</NamingService>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\ChinookDemoDb.sqlite</AttachFileName>
    <DisplayName>Demo database (SQLite)</DisplayName>
    <DriverData>
      <PreserveNumeric1>true</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.Sqlite</EFProvider>
      <MapSQLiteDateTimes>true</MapSQLiteDateTimes>
      <MapSQLiteBooleans>true</MapSQLiteBooleans>
    </DriverData>
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

