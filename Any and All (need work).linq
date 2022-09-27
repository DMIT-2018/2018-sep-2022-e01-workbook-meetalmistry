<Query Kind="Statements">
  <Connection>
    <ID>e69b3623-7543-4e6e-a932-1ba4f3208a62</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WC320-03\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Any and All
//these filter tests return a true or false condition
//they work at the complete collection level


//Genres.Count().Dump();
//25

//show genres that have tracks which are not on any playlist
//17
Genres
  .Where(g => g.Tracks.Any(tr => tr.PlaylistTracks.Count() == 0))
  .Select (g => g)
  .Dump()
  ;
  
//Show genres that have all their tracks appearing at least once on a playlist
//8 records
Genres
	.Where(g => g.Tracks.All(tr => tr.PlaylistTracks.Count() > 0))
	.Select (g => g)
	.Dump()
	;
	

//there may be times that using a !Any() -> All(!relationship)
//   and !All  -> Any(!relationship)


//Using ALl and ANY in comparing 2 collections
//if your collection is not a complex record is a LINQ method called .Except that can be ussed to solve your query

//