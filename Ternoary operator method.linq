<Query Kind="Expression">
  <Connection>
    <ID>e69b3623-7543-4e6e-a932-1ba4f3208a62</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WC320-03\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Chinook</Database>
  </Connection>
</Query>

//List all Albums by release label. Any album with no label should be indicated as Unknown
//List Title, label and Artist Name
//order by releaselabel


//Understand the problem
//collection: albums
//selective data: anonymous dataset
//label (nullable):either Unknown or label name ****
//order by the releaselabel field

//design
//Albums
//Select(new{})
//fields: title
//		:label ??? ternary opertator(condition(s) ? true value : false value)
//		:Artist.Name

//coding and testing


Albums
	//.OrderBy(x => x.ReleaseLabel)
	.Select(x => new
	{
		Title = x.Title,
		Label = x.ReleaseLabel == null ? "Unknown" : x.ReleaseLabel,
		Artist = x.Artist.Name
	
	}
	)
	.OrderBy(x => x.Label)// this order by puts Unknown in alphabatically
	
//List all Albums showing the title, Artist name, Year and decade of release using oldies, 70s, 90s, modern.
//order by decades

//nested ternary opertator
//< 1970
// oldies
//else
//(<1980 then 70's
//    else 
// ( < 1990 then 80's
//   (< 2000 then 90's
//      else
//        modern)))

//collection: Albums
//selective data:
//Year: oldies, 70s, 90s, modern.
//orderBy decades

Albums
	.Select(x => new 
	 {
	   Title = x.Title,
	   Artist = x.Artist.Name,
	   Year = x.ReleaseYear,
	   Decade = x.ReleaseYear < 1970 ? "Oldies":
	   				x.ReleaseYear <1980 ? "70s":
					x.ReleaseYear <1990 ? "80s":
					x.ReleaseYear < 2000 ? "90s":"Modern"
	 })
	 .OrderBy (x => x.Year) // no .Decade
	 
	 
	 
	 
	 
	 
	 
	 
	 
	 
	 
	 
	 
	 