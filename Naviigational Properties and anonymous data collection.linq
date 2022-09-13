<Query Kind="Expression">
  <Connection>
    <ID>10398237-b90a-42ab-92b0-8354a2c46fb3</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WB320-12\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Using navigational properties and Anonymous data set (collection)

//reference: Student Notes/Demo/eRestaurant/Linq:Query and method Syntax

//Find all albums released in the 90's (1990-1999)
//Order the Albums by ascending year and then alphabatically by Album title
//Dispaly the year, title,Artist NAme and Release label

//concerns: a) not all properties of Album are to be displayed
//			b) the order of the prop. are to be displayed
//			in the differnt sequence then the defination of the prop.on the entity Album
//			c)The artist name is not on the Album table but is on the Artist table

//Solution: use an anonymous data collection

//the anonymous data instance is defiend within the select by declared fiels(prop)
//the order of the fields on the new defined instance will be done in specifying the prop of the anonymous data collection

Albums
 .Where (x => x.ReleaseYear > 1989 && x.ReleaseYear < 2000)
 //.OrderBy(x => x.ReleaseYear)
 //.ThenBy (x => x.Title)
 //creating new class with fields
 .Select(x => new
	{
		Year = x.ReleaseYear,
		Title = x.Title,
		Artist = x.Artist.Name,
		Label = x.ReleaseLabel	
	})
	
	
	.OrderBy(x => x.Year) // year is in the anonymous datatype defination, release year is NOT
 .ThenBy (x => x.Title)