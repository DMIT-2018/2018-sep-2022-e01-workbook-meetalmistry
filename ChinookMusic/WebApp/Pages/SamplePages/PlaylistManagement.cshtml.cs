using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional 
using ChinnokSystem.ViewModels;
using WebApp.Helpers;
using ChinnokSystem.BLL;
#endregion


namespace WebApp.Pages.SamplePages
{

    public class PlaylistManagementModel : PageModel
    {
        #region Private variables and DI constructor
        private readonly TrackServices _trackServices;
        private readonly PlaylistTrackServices _playlisttrackServices;


        public PlaylistManagementModel(TrackServices trackservices,
                                PlaylistTrackServices playlisttrackservices)
        {
            _trackServices = trackservices;
            _playlisttrackServices = playlisttrackservices;
        }
        #endregion

        #region Messaging and Error Handling
        [TempData]
        public string FeedBackMessage { get; set; }
        
        public string ErrorMessage { get; set; }

        //a get property that returns the result of the lamda action
        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
        public bool HasFeedBack => !string.IsNullOrWhiteSpace(FeedBackMessage);

        //used to display any collection of errors on web page
        public List<string> ErrorDetails { get; set; } = new();

        //PageModel local error list for collection 
        public List<Exception> Errors { get; set; } = new();

        #endregion

        #region Paginator
        private const int PAGE_SIZE = 5;
        public Paginator Pager { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? currentpage { get; set; }   
        #endregion

        [BindProperty(SupportsGet = true)]
        public string searchBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string searchArg { get; set; }

        [BindProperty(SupportsGet = true)]
        public string playlistname { get; set; }

        public List<TrackSelection> trackInfo { get; set; } //do not need bind prop nothing is cmming out just a display.

        public List<PlaylistTrackTRX> qplaylistInfo { get; set; }


        //this prop willl be tied to the input fields of the web page
        //in this case this list tied to the table data elements for the playlist
       // [BindProperty]
        //public List<PlaylistMove> cplaylistInfo { get; set; }

        [BindProperty]
        public int addtrackid { get; set; }

        public const string USERNAME = "HansenB";
        public void OnGet()
        {
            //this method is executed every time the page is called for the first time or whenever a Get request is made to the page SUCH AS RedirectTopage()
            GetTrackInfo();
            GetPlaylist();
        }

        public void GetTrackInfo()
        {
            if (!string.IsNullOrWhiteSpace(searchArg) &&
                            !string.IsNullOrWhiteSpace(searchBy))
            {
                int totalcount = 0;
                int pagenumber = currentpage.HasValue ? currentpage.Value : 1;
                PageState current = new(pagenumber, PAGE_SIZE);
                trackInfo = _trackServices.Track_FetchTracksBy(searchArg.Trim(),
                    searchBy.Trim(), pagenumber, PAGE_SIZE, out totalcount);
                Pager = new(totalcount, current);
            }
        }

        public void GetPlaylist()
        {
            if (!string.IsNullOrWhiteSpace(playlistname))
            {
                string username = USERNAME;
                qplaylistInfo = _playlisttrackServices.PlaylistTrack_Fetch_Playlist(playlistname.Trim(), username);
            }
        }
        public IActionResult OnPostTrackSearch()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchBy))
                {
                    Errors.Add(new Exception("Track search type not selected"));
                }
                if (string.IsNullOrWhiteSpace(searchArg))
                {
                    Errors.Add(new Exception("Track search string not entered"));
                }
                if (Errors.Any())
                {
                    throw new AggregateException(Errors);
                }
                //RedirectToPage() willl cause an Get request to be issue (OnGet())
                return RedirectToPage(new
                {
                    searchBy = searchBy.Trim(),
                    searchArg = searchArg.Trim(),
                    playlistname = string.IsNullOrWhiteSpace(playlistname) ? " " : playlistname.Trim()
                });
            }
            catch (AggregateException ex)
            {
                ErrorMessage = "Unable to process search";
                foreach (var error in ex.InnerExceptions)
                {
                    ErrorDetails.Add(error.Message);
                    
                }
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                return Page();
            }
        }

        public IActionResult OnPostFetch()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(playlistname))
                {
                    throw new Exception("Enter a playlist name to fetch.");
                }
                return RedirectToPage(new
                {
                    searchBy = string.IsNullOrWhiteSpace(searchBy) ? " " : searchBy.Trim(),
                    searchArg = string.IsNullOrWhiteSpace(searchArg) ? " " : searchArg.Trim(),
                    playlistname = playlistname.Trim()
                });
            }
            catch(Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                return Page();

            }
        }

        public IActionResult OnPostAddTrack()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(playlistname))
                {
                    throw new Exception("You need to have a playlist select first. Enter a playlist name and Fetch");
                }

                // Add the code to add a track via the service.
                //the data needed for your call has ALREADY been placed in your local property by the use of [BindProperty] which is two way output/input)
                //once security is install, you would be able to obtain the user name from the operating system.
                //we dont have set up for security yeet so we will use const for username for testing
                string username = USERNAME;
                //call your service sending in the expected data
                _playlisttrackServices.PlaylistTrack_AddTrack(playlistname, username, addtrackid);
                
                FeedBackMessage = "adding the track";
                return RedirectToPage(new
                {
                    searchby = searchBy,
                    searcharg = searchArg,
                    playlistname = playlistname
                });
            }
            catch (AggregateException ex)
            {
                              
                ErrorMessage = "Unable to process add track";
                foreach (var error in ex.InnerExceptions)
                {
                    ErrorDetails.Add(error.Message);

                }
                GetTrackInfo();
                GetPlaylist();

                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                GetTrackInfo();
                GetPlaylist();

                return Page();
            }
            
        }

        public IActionResult OnPostRemove()
        {
            try
            {
               //Add the code to process the list of tracks via the service.

                return RedirectToPage(new
                {
                    searchBy = string.IsNullOrWhiteSpace(searchBy) ? " " : searchBy.Trim(),
                    searchArg = string.IsNullOrWhiteSpace(searchArg) ? " " : searchArg.Trim(),
                    playlistname = playlistname
                });
            }
            catch (AggregateException ex)
            {

                ErrorMessage = "Unable to process remove tracks";
                foreach (var error in ex.InnerExceptions)
                {
                    ErrorDetails.Add(error.Message);

                }
                GetTrackInfo();
                GetPlaylist();

                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                GetTrackInfo();
                GetPlaylist();

                return Page();
            }

        }

        private Exception GetInnerException(Exception ex)
        {
            while(ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}
