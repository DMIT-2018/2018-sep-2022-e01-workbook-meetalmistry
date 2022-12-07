using FSIS.App.DAL;
using FSIS.App.Entities;
using FSIS.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSIS.App.BLL
{
	public class FsisServices
	{
		private readonly FSIS_2018Context _context;

		internal FsisServices(FSIS_2018Context context)
		{
			_context = context;
		}

		public List<Games> GetAllGames()
		{
			var result = _context.Games
							  .Select(x => new Games
							  {
								  GameDate = (x.GameDate).ToString("dd/MM/yyyy"),
								  GameID = x.GameId,
								  HomeTeamID = x.HomeTeamId,
								  HomeTeamName = x.HomeTeam.TeamName,
								  VisitingTeamID = x.VisitingTeamId,
								  VisitingTeamName = x.VisitingTeam.TeamName
							  }).ToList();
			return result;
		}

		public Games getGameById(int gameId)
		{
			Games result = _context.Games.Where(x => x.GameId == gameId).Select(x => new Games
			{
				GameDate = (x.GameDate).ToString("dd/MM/yyyy"),
				GameID = x.GameId,
				HomeTeamID = x.HomeTeamId,
				HomeTeamName = x.HomeTeam.TeamName,
				VisitingTeamID = x.VisitingTeamId,
				VisitingTeamName = x.VisitingTeam.TeamName,
			}).FirstOrDefault();
			return result;
		}

		public List<PlayerGameStats> GetPlayersByTeamId(int teamId)
		{
			var result = _context.Players.Where(x => x.TeamId == teamId).Select(x => new PlayerGameStats
			{
				PlayerID = x.PlayerId,
				FullName = x.FirstName + " " + x.LastName
			});

			return result.ToList();
		}

		public void RecordGamePlayerStats(int gameID, List<PlayerGameStats> playerStat1, List<PlayerGameStats> playerStat2)
		{
			//Error Exception
			if (gameID == null)
			{
				throw new ArgumentNullException("No GameID provided.");
			}
			if (playerStat1.Count == 0)
			{
				throw new ArgumentNullException("Palyer Stat 1 is 0, must be positive.");
			}
			if (playerStat2.Count == 0)
			{
				throw new ArgumentNullException("Palyer Stat 2 is 0, must be positive.");
			}

			Entities.Game gameExist = null;
			Entities.PlayerStat playerStatExist = null;
			Entities.Player playerExist = null;

			//Check the gameID passed exist
			gameExist = _context.Games.Where(x => x.GameId.Equals(gameID))
							.Select(x => x)
							.FirstOrDefault();

			if (gameExist == null)
			{
				throw new ArgumentNullException("GameID does not exist. Please enter existing GameID.");
			}
			else
			{
				//Loop for List<playerGameStats> playerStat1
				foreach (PlayerGameStats item in playerStat1)
				{
					//Check if playerStat Exist
					playerStatExist = _context.PlayerStats.Where(x => x.GameId.Equals(gameID) && x.PlayerId.Equals(item.PlayerID)).Select(x => x)
							.FirstOrDefault();
					//if playerStat not exist, add new record in PlayerStates
					if (playerStatExist == null)
					{
						Entities.PlayerStat ps1 = new PlayerStat()
						{
							GameId = gameID,
							PlayerId = item.PlayerID,
							Goals = item.Goal,
							Assists = item.Assist,
							YellowCard = item.YellowCard,
							RedCard = item.RedCard,
						};
						gameExist.PlayerStats.Add(ps1);

						//New game entry, increment GamesPlayed for Player 
						playerExist = _context.Players.Where(x => x.PlayerId.Equals(item.PlayerID)).Select(x => x).FirstOrDefault();
						playerExist.GamesPlayed = playerExist.GamesPlayed + 1;
						_context.Players.Update(playerExist);
					}
					// if PlayerStat exist, update the record in playerstates
					else
					{
						playerStatExist.Goals = item.Goal;
						playerStatExist.Assists = item.Assist;
						playerStatExist.YellowCard = item.YellowCard;
						playerStatExist.RedCard = item.RedCard;

						_context.PlayerStats.Update(playerStatExist);
					}
				}

				//Loop for List<playerGameStats> playerStat2
				foreach (PlayerGameStats item in playerStat2)
				{
					//Check if playerStat Exist
					playerStatExist = _context.PlayerStats.Where(x => x.GameId.Equals(gameID) && x.PlayerId.Equals(item.PlayerID)).Select(x => x)
							.FirstOrDefault();
					//if playerStat not exist, add new record in PlayerStates
					if (playerStatExist == null)
					{
						Entities.PlayerStat ps2 = new PlayerStat()
						{
							GameId = gameID,
							PlayerId = item.PlayerID,
							Goals = item.Goal,
							Assists = item.Assist,
							YellowCard = item.YellowCard,
							RedCard = item.RedCard,
						};
						gameExist.PlayerStats.Add(ps2);

						//New game entry, increment GamesPlayed for Player 
						playerExist = _context.Players.Where(x => x.PlayerId.Equals(item.PlayerID)).Select(x => x).FirstOrDefault();
						playerExist.GamesPlayed = playerExist.GamesPlayed + 1;
						_context.Players.Update(playerExist);
					}
					// if PlayerStat exist, update the record in playerstates
					else
					{
						playerStatExist.Goals = item.Goal;
						playerStatExist.Assists = item.Assist;
						playerStatExist.YellowCard = item.YellowCard;
						playerStatExist.RedCard = item.RedCard;

						_context.PlayerStats.Update(playerStatExist);
					}
				}
				_context.SaveChanges();
			}
		}
	}
}