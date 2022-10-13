<Query Kind="Program">
  <Connection>
    <ID>4362a240-56bb-4770-a26a-6b4feb36a734</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>.\SQLEXPRESS</Server>
    <Database>Chinook</Database>
    <DisplayName>Chinnok-Entity</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	//Main is going to represents the web page post method
 string searcharg = "Deep";
 string searchby = "Artist";
 List<TrackSelection> tracklist = Track_FetchTracksBy(searcharg, searchby);
 tracklist.Dump();

	
}

// You can define other methods, fields, classes and namespaces here

#region CQRS Queries

public class TrackSelection
{
    public int TrackId {get; set;}
    public string SongName {get; set;}
    public string AlbumTitle{get; set;}
    public string ArtistName{get; set;}
    public int Milliseconds {get; set;}
    public decimal Price {get; set;}
}
public class PlaylistTrackInfo 
{
    public int TrackId {get; set;}
    public int TrackNumber {get; set;}
    public string SongName {get; set;}
    public int Milliseconds {get; set;}
}
#endregion

#region TrackServices class
public List<TrackSelection>Track_FetchTracksBy(string searcharg, string searchby)


{
	if (string.IsNullOrWhiteSpace(searcharg))
	{
	  throw new ArgumentNullException("No search value submitted");
	}
	if (string.IsNullOrWhiteSpace(searcharg))
	{
	  throw new ArgumentNullException("No search style submitted");
	}
	IEnumerable<TrackSelection> results = Tracks
											.Where (x => (x.Album.Artist.Name.Contains(searcharg) && searchby.Equals("Artist")) ||
													( x.Album.Title.Contains(searcharg) && searchby.Equals("Album")))
											.Select (x => new TrackSelection 
												{
													TrackId = x.TrackId,
													SongName = x.Name,
													AlbumTitle =x.Album.Title,
													ArtistName = x.Album.Artist.Name,
													Milliseconds = x.Milliseconds,
													Price = x.UnitPrice
												};
										return results.ToList();
}
#endregion