using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSIS.App.Models
{
    public class Games
    {
        public string GameDate { get; set; }
        public int GameID { get; set; }
        public int HomeTeamID { get; set; }
        public int VisitingTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public string VisitingTeamName { get; set; }
    }
}
