<Query Kind="Program">
  <Connection>
    <ID>e69b3623-7543-4e6e-a932-1ba4f3208a62</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WC320-03\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//Conversions 
	//collectoion we will look at are Iqueryable, Ienumerable and List
	
	//Display all Albums and their tracks.Display the album title 
	//artist name and album tracks. For each track show the song name and play time.Show only albums with 25 or more tracks
	
	List <AlbumTracks> albumList = Albums
						.Where (a => a.Tracks.Count >= 25)
						.Select( a=> new  AlbumTracks
						{
							Title = a.Title,
							Artist = a.Artist.Name,
							Songs = a.Tracks
									.Select (tr => new SongItem
										{
											Song = tr.Name,
											Playtime = tr.Milliseconds / 1000.0
										})
										.ToList()
						})
						.ToList()
						//.Dump()
						;
						

//Using .FirstOrDeault()
//You first saw in CPSC1517 when check to see the record is existed in BLL service method

//Find first album by DEEp Purple
var artistparam = "Deep Purpple";
var resultsFOD = Albums
				.Where( a => a.Artist.Name.Equals(artistparam))
				.Select (a => a)//take the whole record
				.OrderBy(a => a.ReleaseYear)
				.FirstOrDefault()
				//.Dump()
				;
				
if (resultsFOD != null)
{

	resultsFOD.Dump();
}
else
{
	Console.WriteLine($"No Albums found for artist {artistparam}");
}

//Distinct()
//remove duplicate reported lines

//Get a list of customer countries
var resultsDistinct = Customers
						.OrderBy( c => c.Country)
						.Select(c => c.Country)
						.Distinct()
						//.Dump()
						;



//.Take() and .Skip()
//In CPSC 1517, when you wnated to use the suppled paginator
//the query method was to return ONLY the need records for the displat NOT the entire collection
//a) the query was executed returning a collection of size x
//b) obtained the total count (x) of return records
//c) calculated the number of records to skip (pagenumber - 1)* pagesize
//d) On the return method statement you used return variablename.Skip(rowSkiped).Take(pagesize).ToList()


//Union
//rules in linq are the same as SQL
//result is the same as the SQL, combine seperate collection into one.
//Syntax  (queryA.Union(queryB)[.Union(query...)]
//rules: 
//number of collumn the same
//collumn datatype must be the same
//ordering should be done as a method after the last Union


var resultsUnionA = (Albums
					.Where (x => x.Tracks.Count() == 0)
					.Select (x => new {
						 Title = x.Title,
						 totalTracks = 0,
						 totalCost = 0.00m,
						 AverageLength = 0.00d


						})
						)
						
						
		.Union(Albums
		.Where(x => x.Tracks.Count() > 0)
					.Select (x => new {
						 Title = x.Title,
						 totalTracks = x.Tracks.Count(),
						 totalCost = x.Tracks.Sum(tr => tr.UnitPrice),
						 AverageLength = x.Tracks.Average(tr => tr.Milliseconds)
						 
						})
						)
						.OrderBy( x => x.totalTracks)
						.Dump()
						;


}


public class SongItem 
{
	public string Song {get;set;}
	public double Playtime {get;set;}
}

public class AlbumTracks
{
	public string Title {get;set;}
	public string Artist {get;set;}
	public  List <SongItem> Songs {get;set;}
}
// You can define other methods, fields, classes and namespaces here