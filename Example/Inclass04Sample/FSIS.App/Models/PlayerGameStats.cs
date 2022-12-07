using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSIS.App.Models
{
	public class PlayerGameStats
	{
		public int PlayerStatsID { get; set; }
		public int GameID { get; set; }
		public int PlayerID { get; set; }
		public string FullName { get; set; }
		public int Goal { get; set; }
		public int Assist { get; set; }
		public bool YellowCard { get; set; }
		public bool RedCard { get; set; }
	}
}
