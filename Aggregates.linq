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

//Aggregates
//.Count() counts the number of instances in the collection
//.Sum(x => ....) sums (totals) a numeric field (numeric expression) in a collection
//.Min(x => ...) finds the minimum value of a collection for a field
//.Max(x => ...) finds themaximum value of a collection for a field
//.Average (x => ...) finds the Average value of a nummeric field (numeric expression in a collection)


//IMPORTANT!!!!!
//Aggregateds works ONLY on a collection of Value
//Aggregates do not work on a single row (instance - non declare collection)

//.Sum, .Min, .Max and .Average must have at least one record in their collection
//.Sum and .Average MUSt work on numeric fields and the field CAN NOT be null.

//Syntax  : method
//collectionset. aggregate( x=> expression)
//collectionset.Select(...).aggregate()
//collectionset.Count() //.Count() does not contain an expression

//For sum, min, Max and Average: the results is a single value

//you can use multiple aggregates on a single column
//    .Sum( x => expression).Min( x => expression)

//Find the average playing time (length) of tracks in our music collection

//thought process
//average is and aggregate
//what is the collection? the Tracks table is a collection
//what is the expression? Milliseconds

Tracks.Average(x => x.Milliseconds) // each x has multiple fields need to determin which field)

Tracks.Select(x => x.Milliseconds).Average() //sinle list of numbers

// Tracks.Average() - aborts because no specific field was referred to on the track record


//List all Albums of the 60s showing the title , Artist and various aggregates for Albums contaning Tracks
//For each Album show the number of tracks, the total price of all tracks and the average playing length of the Album tracks

//Thought Process
//I will start with Albums because  for aggregate I need collection
//start at Albums
//can i get the artist Name (.Artist)
//Can I get a collection of Tracks for an albums( x.Tracks)
//Can I get the number of tracks in the collection (.Count())
//Ca I get the total price of the Tracks (.Sum())
//Can I get the Average of the play length (.Average())

Albums
  .Where(x => x.Tracks.Count()> 0  //filtering albums contain atleats 1 track
     && (x.ReleaseYear > 1959 &&x.ReleaseYear < 1970) )
  .Select(x => new 
  {
  	Title = x.Title,
	Artist = x.Artist.Name,
	NumberOfTracks = x.Tracks.Count(),
	TotalPrice = x.Tracks.Sum(tr => tr.UnitPrice),
	AverageTrackLength = x.Tracks.Select(tr => tr.Milliseconds).Average()  
  
  })
  
































