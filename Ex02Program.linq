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
 
 
 try
 {
 
 //coded and tested the FetchTracksBy query
  string searcharg = "Deep";
  string searchby = "Artist";
  List<TrackSelection> tracklist = Track_FetchTracksBy(searcharg, searchby);
  //tracklist.Dump();
  
  
  //coded and tested fetchPlaylist query
  string playlistname= "hansenb1";
  string username = "HansenB";//this is an user name which will come from o/s via security
  List<PlaylistTrackInfo> playlist = PlaylistTrack_FetchPlaylist(playlistname, username);
  //playlist.Dump();
  
  
  //coded and tested the Add_Track trx
  //the command method recieve no collection but will receive individual arguments
  //trackid, playlistname, username
  //test tracks
  //793 A castle full of Rascals
  //822 A Twist in the Tail
  //543 Burn
  //756 Child in Time
  
  //on the web page the post method would have already access to the NindProperty variables containing the input values.
  playlistname = "hansenbtest";
  int trackid = 793;
  
  //call the service method to process the data
   PlaylistTrack_AddTrack (playlistname,  username,  trackid);
   
   //once the service emethod is complete, the web page would refresh
   playlist = PlaylistTrack_FetchPlaylist(playlistname, username);
   playlist.Dump();

  
 }
  catch (ArgumentNullException ex)
 {
   GetInnerException(ex).Message.Dump();
 }
 
 catch (ArgumentException ex)
 {
   GetInnerException(ex).Message.Dump();
 }
 catch (Exception ex)
 {
 	 GetInnerException(ex).Message.Dump();
 }
	
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


//general method to drill down into an exeption of obtain the InnerException where your actual error is detailed

private Exception GetInnerException(Exception ex)
{
  while (ex.InnerException != null)
    ex = ex.InnerException;
	return ex;
}

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
													Price = x.UnitPrice,
												});
										return results.ToList();
}

public List<PlaylistTrackInfo> PlaylistTrack_FetchPlaylist(string playlistname, string username)


{
	if (string.IsNullOrWhiteSpace(playlistname))
	{
	  throw new ArgumentNullException("No playlist name submitted");
	}
	if (string.IsNullOrWhiteSpace(username))
	{
	  throw new ArgumentNullException("No username submitted");
	}
	IEnumerable<PlaylistTrackInfo> results = PlaylistTracks
											.Where (x => x.Playlist.Name.Equals(playlistname) &&
													x.Playlist.UserName.Equals(username))
											.Select (x => new PlaylistTrackInfo
												{
													TrackId = x.TrackId,
													TrackNumber = x.TrackNumber,
													SongName = x.Track.Name,					
													Milliseconds = x.Track.Milliseconds,
													
													
												})
												.OrderBy(x => x.TrackNumber);
										return results.ToList();
}
#endregion


#region Command TRX method

void  PlaylistTrack_AddTrack(string playlistname, string username, int trackid)
  {
  
  	//locals
	Tracks trackexists = null;
	Playlists playlistexists = null;
	PlaylistTracks  playlisttrackexists = null;
	int tracknumber = 0;
	
  	if (string.IsNullOrWhiteSpace(playlistname))
	{
	  throw new ArgumentNullException("No playlist name submitted");
	}
	if (string.IsNullOrWhiteSpace(username))
	{
	  throw new ArgumentNullException("No username submitted");
	}
	
	trackexists = Tracks
					.Where(x => x.TrackId == trackid)
					.Select(x => x)
					.FirstOrDefault();
  	if (trackexists == null)
	{
	  throw new ArgumentException("Selected Track no longer on file. Refresh track table.");
	}
	
	playlistexists = Playlists
						.Where (x => x.Name.Equals(playlistname)
						    && x.UserName.Equals(username))
						.Select (x => x)
						.FirstOrDefault();// Business Rule - playlist name need to be unique for user so we need FirstorDefault.
	
	if (playlistexists == null)
		{
			playlistexists = new Playlists()
			{
				Name = playlistname,
				UserName = username
			};			
			//staging the playlist record
			Playlists.Add(playlistexists)
			tracknumber = 1;
		}
		else
		{
		  //B/R a track may only exist once on a playlist
		  playlisttrackexists = PlaylistTracks
		  							.Where (x => x.Playlist.Name.Equals(playlistname)
									  && x.Playlist.UserName.Equals(username)
									  && x.TrackId == trackid)
									 .Select (x => x)
									 .FirstOrDefault();
			if (playlisttrackexists == null)
			{ //
			  //generate the next tracknumber
			   tracknumber = PlaylistTracks
			   					.Where (x => x.Playlist.Name.Equals(playlistname)
									  && x.Playlist.UserName.Equals(username)
									  && x.TrackId == trackid)
									 .Count();
				tracknumber++;
			}
			else
			{ 
			  var songname = Tracks
			  					.Where (x => x.TrackId == trackid)
								.Select (x => x.Name)
								.SingleOrDefault();
			  throw new ArgumentException($"Selected Track ({songname}) already exists on the playlist");
			}
			
		}
		
		//processing to stage the new Track to the playlist
		playlisttrackexists = new PlaylistTracks();
		
		//load the data to the new instance of playlist track
		playlisttrackexists.TrackNumber = tracknumber;
		playlisttrackexists.TrackId = trackid;
  }

#endregion










