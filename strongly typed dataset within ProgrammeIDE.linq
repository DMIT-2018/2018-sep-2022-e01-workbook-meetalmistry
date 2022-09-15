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
{	//pretend that the MAIN() is the web page

	//Find songs by partial song name.
	//Display the Album title, song, and artist name.
	//orderby song.
	
	//assume a value was entered into the web page
	//assume that a post buttton was pressed
	//assume Main() is the Onpost event
	
	string inputvalue = "dance";
	List<SongList> songCollection = SongsByPartialName(inputvalue);
	songCollection.Dump();// aasume is the web page display	
								
}

// You can define other methods, fields, classes and namespaces here

//C# really enjoys strongly type data fields.
// whether these fields are premitive datatype (int, double... ets) or developer defined datatypes (class)


//auto implimented prop. not fully implemented
public class SongList
{
	public string Album{get;set;}
	public string Song{get;set;}
	public string Artist{get;set;}
	
}

//imagine the following method exist in a service in your BLL
//this methd receives the web page parameter value for the query
//this method will need to return a collection

List<SongList> SongsByPartialName(string partialSongName)
{
   //instead using var you can use Ienumerable<SongList> songCollecton = tracks
   var songCollection = Tracks
							.Where(t => t.Name.Contains(partialSongName))
							.OrderBy (t => t.Name)
							.Select (t => new SongList
								  {
								  Album = t.Album.Title,
								  Song = t.Name,
								  Artist = t.Album.Artist.Name
								  }
								);
								return songCollection.ToList();
								}
								
								
								
								