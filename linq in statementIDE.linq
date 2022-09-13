<Query Kind="Statements">
  <Connection>
    <ID>10398237-b90a-42ab-92b0-8354a2c46fb3</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WB320-12\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Chinook</Database>
  </Connection>
</Query>

//this is  the statement ide
//this environment expects the use of the C# statement grammar
//the results of a query is not automatically displaed as is the expression enviroment
//to display the results you need to .Dump() the variable holdinng the data result
//IMPRTANT !!.Dump() is a LINQPAD method.It is not a C# method
//within the statement environemntone can run ALL the queries in one execution.


var qsyntaxlist = from arowoncollection in Albums
                    select arowoncollection;
//qsyntaxlist.Dump();

var msyntaxlist = Albums
   .Select (arowoncollection => arowoncollection)
   .Dump();
 
//msyntaxlist.Dump();

var QueenAlbums = Albums
	.Where(a => a.Artist.Name.Contains("Queen"))
	//.Dump()
	;

