<Query Kind="Program">
  <Connection>
    <ID>e69b3623-7543-4e6e-a932-1ba4f3208a62</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WC320-03\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
//what is composite class - for me
//Nested queries
//sometimes referred to as subqueries
//
//Simply put: it is a query within a query [....]

//List all sales support employees showing their fullname(last,first), title and phone
//For each employee, show alist of customers they support.
//show the customer full name (last, first, city and state

//employee 1, title, phone
//  sublist of customer 2000, cith state
//  customer 2109, city, state
//  customer 5000, stste

//Employee 22, titile, phone
//  customer 301, city , state

//ther appears to be 2 seperate lists that need to be within one final dataset collection
//List of employees
//List of employee customers
//concern: the lists are intermixed!!!

//C# point of view in a class definition
//first: this is a composite class
//  the class is describing an employee
//  each instance of the employee will have a list of emplyee cutomers

//class EmployeeList
//fullname(property)
//title(roperty)
//phone (property)
//collection of customers associated with employee (property: List<T>)

//class Customerlist
//fullNme (prop)
//city (prop)
//State(prop)

var results  = Employees
				.Where(e => e.Title.Contains("sales Support"))
				.Select( e => new EmployeeItem
				{
				   FullName = e.LastName + ", " + e.FirstName,
				   Title = e.Title,
				   Phone = e.Phone, 
				   CustomerList = e.SupportRepCustomers
				   					.Select (c =>  new CustomerItem 
									{
										FullName = c.LastName + ", " + c.FirstName,
										City = c.City,
										State = c.State,
									
									}
									)
				   
				}
				);
				results.Dump();

	
}

public class CustomerItem
{
	public string FullName {get;set;}
	public string City{get;set;}
	public string State{get;set;}
}

public class EmployeeItem
{
	public string FullName {get;set;}
	public string Title {get;set;}
	public string Phone {get;set;}
	public IEnumerable<CustomerItem> CustomerList {get;set;}
}












