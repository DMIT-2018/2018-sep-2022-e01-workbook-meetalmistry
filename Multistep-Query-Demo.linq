<Query Kind="Statements">
  <Connection>
    <ID>e69b3623-7543-4e6e-a932-1ba4f3208a62</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WC320-03\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Problem (You can use in question 5)
//One needs to have processed information from a collection
//to use against the same collection

//solution to this type of problem is to use multiple queries
//the  early quer(ies) will produced the needed information/criteria to execute against the same collection in later quer(ies)
//basically we need to do some pre-processing



//Query one will generate data/information that will be use in the next query (two)

//Display the employees that have the most customers to support.
//display the employ name and number of customers that employee supports.

//What is Not wanted is a list of all employess sorted by number of customers supported.

//One could create a list of all employees, with the customer support count, ordered descending by support count. BUT this is not NOT what is requested.

//What information do I need
//a) I need to know the maximum number of Customers that an particular emplyee is supporting
//b) I need to take that piece of data and compare to all employees.

//a) get a list of employees and the count of customers each supports
//b) from that list I can obtain the largest number 
//c) using the number, review all the employees and their counts reporting ONLY the busiest emplyees.


var PreprocessEmployeeList = Employees
								.Select(x => new
									{
										Name = x.FirstName + " " + x.LastName,
										CustomerCount = x.SupportRepCustomers.Count()
									})
									//.Dump()
									;
									
//var highcount = PreprocessEmployeeList
					//.Max(x => x.CustomerCount)
					//.Dump()
					//;
					
//var BusyEmployees = PreprocessEmployeeList
						//.Where ( x => x.CustomerCount == highcount)
						//.Dump()
						//;
						
						
						
						
var BusyEmployees = PreprocessEmployeeList
						.Where ( x => x.CustomerCount == 
						          PreprocessEmployeeList.Max(x => x.CustomerCount))
						.Dump()
						;						
						
						
						
						
						
						
						
						
						
						
						
						
						
						
						