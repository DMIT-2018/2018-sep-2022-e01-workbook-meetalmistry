void Main()
{
 	int gameID = 1;
	List<playerGameStats> pgameSet1 = new List<playerGameStats>();
	pgameSet1.Add(new playerGameStats()
	{
		GameID = gameID,
		PlayerID = 185,
		Goal = 3,
		Assist = 0,
		YellowCard = true,
		RedCard = true
	});
	pgameSet1.Add(new playerGameStats()
	{
		GameID = gameID,
		PlayerID = 144,
		Goal = 2,
		Assist = 2,
		YellowCard = false,
		RedCard = false
		
	});
	pgameSet1.Add(new playerGameStats()
	{
		GameID = gameID,
		PlayerID = 155,
		Goal = 1,
		Assist = 0,
		YellowCard = false,
		RedCard = false
	});
	
	List<playerGameStats> pgameSet2 = new List<playerGameStats>();
	pgameSet2.Add(new playerGameStats()
	{
		GameID = gameID,
		PlayerID = 205,
		Goal = 2,
		Assist = 0,
		YellowCard = true,
		RedCard = true
	});
	pgameSet2.Add(new playerGameStats()
	{
		GameID = gameID,
		PlayerID = 199,
		Goal = 2,
		Assist = 2,
		YellowCard = true,
		RedCard = true
		
	});
	
	RecordGamePlayerStats(gameID, pgameSet1, pgameSet2);
}

#region CQRS Queries/Command models

//query models
public class playerGameStats
{
	public int PlayerStatsID{get; set;}
	public int GameID{get; set;}
	public int PlayerID{get; set;}
	public int Goal{get; set;}
	public int Assist{get; set;}
	public bool YellowCard{get; set;}
	public bool RedCard{get; set;}
}

#endregion

#region Command TRX methods
//Passing 3 variables in method, 1, gameID, List of playersGameStats 1 and 2
void RecordGamePlayerStats(int gameID, List<playerGameStats> playerStat1, List<playerGameStats> playerStat2)
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
	
	Games gameExist = null;
	PlayerStats playerStatExist = null;
	Players playerExist = null;
	
	//Check the gameID passed exist
	gameExist = Games.Where(x => (x.GameID.Equals(gameID)))
					.Select(x => x)
					.FirstOrDefault();
					
	if (gameExist == null)
	{
		throw new ArgumentNullException("GameID does not exist. Please enter existing GameID.");
	}
	else
	{
		//Loop for List<playerGameStats> playerStat1
		foreach(playerGameStats item in playerStat1)
		{
			//Check if playerStat Exist
			playerStatExist = PlayerStats.Where(x => x.GameID.Equals(gameID) && x.PlayerID.Equals(item.PlayerID)).Select(x => x)
					.FirstOrDefault(); 
			//if playerStat not exist, add new record in PlayerStates
			if(playerStatExist == null)
			{
				PlayerStats ps1 = new PlayerStats()
				{
					GameID = gameID,
					PlayerID = item.PlayerID,
					Goals = item.Goal,
					Assists = item.Assist,
					YellowCard = item.YellowCard,
					RedCard = item.RedCard,
				};
				gameExist.PlayerStats.Add(ps1);
				
				//New game entry, increment GamesPlayed for Player 
				playerExist = Players.Where(x => x.PlayerID.Equals(item.PlayerID)).Select(x =>x).FirstOrDefault();
				playerExist.GamesPlayed = playerExist.GamesPlayed + 1;
				Players.Update(playerExist);
			} 
			// if PlayerStat exist, update the record in playerstates
			else 
			{
				playerStatExist.Goals = item.Goal;
				playerStatExist.Assists = item.Assist;
				playerStatExist.YellowCard = item.YellowCard;
				playerStatExist.RedCard = item.RedCard;
				
				PlayerStats.Update(playerStatExist);
			}
		}
		
		//Loop for List<playerGameStats> playerStat2
		foreach(playerGameStats item in playerStat2)
		{
			//Check if playerStat Exist
			playerStatExist = PlayerStats.Where(x => x.GameID.Equals(gameID) && x.PlayerID.Equals(item.PlayerID)).Select(x => x)
					.FirstOrDefault(); 
			//if playerStat not exist, add new record in PlayerStates
			if(playerStatExist == null)
			{
				PlayerStats ps2 = new PlayerStats()
				{
					GameID = gameID,
					PlayerID = item.PlayerID,
					Goals = item.Goal,
					Assists = item.Assist,
					YellowCard = item.YellowCard,
					RedCard = item.RedCard,
				};
				gameExist.PlayerStats.Add(ps2);
				
				//New game entry, increment GamesPlayed for Player 
				playerExist = Players.Where(x => x.PlayerID.Equals(item.PlayerID)).Select(x =>x).FirstOrDefault();
				playerExist.GamesPlayed = playerExist.GamesPlayed + 1;
				Players.Update(playerExist);
			} 
			// if PlayerStat exist, update the record in playerstates
			else 
			{
				playerStatExist.Goals = item.Goal;
				playerStatExist.Assists = item.Assist;
				playerStatExist.YellowCard = item.YellowCard;
				playerStatExist.RedCard = item.RedCard;
				
				PlayerStats.Update(playerStatExist);
			}			
		}
		SaveChanges();
	}
}
#endregion
