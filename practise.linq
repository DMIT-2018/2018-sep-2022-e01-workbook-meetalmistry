<Query Kind="Program">
  <Connection>
    <ID>d51e277a-9bfe-4c01-8c06-8abdc6763e01</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>localhost\SQLEXPRESS01</Server>
    <Database>FSIS_2018</Database>
    <DisplayName>Entity-FSIS</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
    var a = DisplayGames();
    a.Dump();
    
    var b = DisplayTeams();
    b.Dump();
    
    try
    {
        GameStat testData1 = new GameStat();
        testData1.GameDate = DateTime.Now;
        testData1.HomeTeamId = 9;
        testData1.VisitingTeamId = 7;
        testData1.HomeTeamScore = 3;
        testData1.VisitingTeamScore = 0;
        
        Game_RecordGame(testData1);
        var c = DisplayGames();
        c.Dump();
        
        GameStat testData2 = new GameStat();
        testData2.GameDate = DateTime.Now;
        testData2.HomeTeamId = 9;
        testData2.VisitingTeamId = 7;
        testData2.HomeTeamScore = 3;
        testData2.VisitingTeamScore = 3;
        Game_RecordGame(testData2);
    }
    catch (ArgumentNullException ex)
    {
        //ex.Measage.Dump();
    }
}

//querry models

public class DisplayGame
{
    public int GameID {get; set;}
    public DateTime GameDate {get; set;}
    public int HomeTeamID {get; set;}
    public string HomeTeamName {get; set;}
    public int HomeTeamScore {get; set;}
    public int VisitingTeamID {get; set;}
    public string VisitingTeamName {get; set;}
    public int VisitingTeamScore {get; set;}
}

public class DisplayTeam
{
    public int TeamID {get; set;}
    public string TeamName {get; set;}
    public int? Wins {get; set;}
    public int? Losses {get; set;}
}

public class GameStat
{
    public DateTime GameDate {get; set;}
    public int HomeTeamId {get; set;}
    public int HomeTeamScore {get; set;}
    public int VisitingTeamId {get; set;}
    public int VisitingTeamScore {get; set;}
}

public List<DisplayGame> DisplayGames()
{
    var results = Games
                    .Select(x => new DisplayGame
                    {
                           GameID = x.GameID,
                           GameDate = x.GameDate,
                           HomeTeamID = x.HomeTeamID,
                           HomeTeamName = x.Home.TeamName,
                           HomeTeamScore = x.HomeTeamScore,
                           VisitingTeamID = x.VisitingTeamID,
                           VisitingTeamName = x.Visiting.TeamName,
                           VisitingTeamScore = x.VisitingTeamScore
                    });
                
                    return results.ToList();
}

public List<DisplayTeam> DisplayTeams()
{
    var results = Teams
                    .Select(x => new DisplayTeam
                    {
                        TeamID = x.TeamID,
                        TeamName = x.TeamName,
                        Wins = x.Wins,
                        Losses = x.Losses
                    });

                    return results.ToList();
}

//private exception GetInnerException(Exception ex)
//{
//    while (ex.InnerException != null)
//    ex = ex.InnerException;
//    return ex;
//}

public void Game_RecordGame(GameStat item)
{
    //Teams teamExists = null;
    Teams selectedTeamA = null;
    Teams selectedTeamB = null;
    DateTime todaysDate = DateTime.Now;
    Games gameExist = null;
    
    selectedTeamA = Teams
                        .Where(x => x.TeamID == item.HomeTeamId)
                            .FirstOrDefault();                        
    selectedTeamB = Teams
                        .Where(x => x.TeamID == item.VisitingTeamId)
                            .FirstOrDefault();
    if(selectedTeamA == null)
    {
        throw new ArgumentNullException("Team A Id does not exist!");
    }
    
    if(selectedTeamB == null)
    {
        throw new ArgumentNullException("Team B Id does not exist!");
    }
    
    if(selectedTeamA.TeamID == selectedTeamB.TeamID)
    {
        throw new ArgumentNullException("Both team ID are same, please select two different teams.");
    }
    
    if(item.GameDate > todaysDate)
    {
        throw new ArgumentNullException("Game date can not be of future date.");
    }
    
    if(item.HomeTeamScore == item.VisitingTeamScore)
    {
        throw new ArgumentNullException("Game can not be tied!");
    }
    
    gameExist = Games
                    .Where(x => x.GameDate.Equals(item.GameDate)
                    && x.HomeTeamID.Equals(item.HomeTeamId)
                    && x.VisitingTeamID.Equals(item.VisitingTeamId)
                    && x.HomeTeamScore.Equals(item.HomeTeamScore)
                    && x.VisitingTeamScore.Equals(item.VisitingTeamScore)
                    ).FirstOrDefault();
     
     if(gameExist == null)
     {
         //ADD new game
        gameExist = new Games()
        {
            GameDate = item.GameDate,
            HomeTeamID = item.HomeTeamId,
            VisitingTeamID = item.VisitingTeamId,
            HomeTeamScore = item.HomeTeamScore,
            VisitingTeamScore = item.VisitingTeamScore
        };
        Games.Add(gameExist);
     }
     else
     {
     // Modifiy / Update
             gameExist.HomeTeamScore = item.HomeTeamScore;
            gameExist.VisitingTeamScore = item.VisitingTeamScore;
            Games.Update(gameExist); 
     }
     
     SaveChanges();
}