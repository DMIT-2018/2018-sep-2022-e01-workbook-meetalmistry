using FSIS.App.BLL;
using FSIS.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inclass04Sample.Pages
{
    public class GameStatModel : PageModel
    {
        //1 you need service
        private readonly FsisServices _service;
        public GameStatModel(FsisServices service)
        {
            _service = service;
        }

        //2 Declare your variables
        [BindProperty]
        public int GameId { get; set; }
        
        [BindProperty]
        public Games GetGame { get; set; }

        [BindProperty]
        public List<Games> LoadGames { get; set; }

        [BindProperty]
        public List<PlayerGameStats> HomeTeamPlayers { get; set; }

        [BindProperty]
        public List<PlayerGameStats> VisitingTeamPlayers { get; set; }

        [TempData]
        public int GetGameID { get; set; }

        //3 Set the default OnGet
        public void OnGet()
        {
            LoadGames = _service.GetAllGames();
            //HomeTeamPlayers = _service.GetPlayersByTeamId(3);
            //VisitingTeamPlayers = _service.GetPlayersByTeamId(10);
        }

        public IActionResult OnPostFetch()
        {
            LoadGames = _service.GetAllGames();
            GetGame = _service.getGameById(GameId);
            GetGameID = GameId;
            HomeTeamPlayers = _service.GetPlayersByTeamId(GetGame.HomeTeamID);
            VisitingTeamPlayers = _service.GetPlayersByTeamId(GetGame.VisitingTeamID);
            return Page();
        }

        public IActionResult OnPostAddRecord()
        {
            LoadGames = _service.GetAllGames();
            _service.RecordGamePlayerStats(GetGameID, HomeTeamPlayers, VisitingTeamPlayers);
            return Page();
        }

        public IActionResult OnPostClear()
        {
            LoadGames = _service.GetAllGames();
            return Page();
        }
    }
}
