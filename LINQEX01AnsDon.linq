<Query Kind="Statements" />

//01)List all the skills for which we do not have any qualfied employees.
//if skill has any children that is in employee skill table

var q1 = Skills
			.Where(s => s.EmployeeSkills.Count() == 0)
			.Select (x => x.Description);			
	q1.Dump();

//2)Show all skills requiring a ticket and which employees have those skills. 
//Include all the data as seen in the following image. 
//Order the employees by years of experience (highest to lowest). 
//Use the following text for the levels: 1 = Novice, 2 = Proficient, 3 = Expert. (Hint: Use nested ternary operators to handle the levels as text.)

var q2 = Skills
			.Where (s => s.RequiresTicket)
			.Select (s => new 
			 {
			 	Description = s.Description,
				Employees = s.EmployeeSkills
				            .OrderByDescending (x => x.YearsOfExperience)
								.Select (x => new 
									{
									  Name = x.Employee.FirstName + " " + x.Employee.FirstName,
									  Level = x.Level == 1? "Novice":
									  		  x.Level == 2 ? "Proficient" : "Expert",
									  YearsOfexperience = x.YearsOfExperience
									})
			 });
			 q2.Dump();
			 
//List all employees with multiple skills; ignore employees with only one skill. 
//Show the name of the employee and the list of their skillsets; for each skill, show the name of the skill, 
//the level of competance and the years of experience. Use the following text for the levels: 1 = Novice, 2 = Proficient, 3 = Expert.	

//all the employees with empoyeeskill - employee to employeeskill (children) we can count on that because its a collection

var q3 = Employees
			.Where (e => e.EmployeeSkills.Count() > 1)
			.Select (e => new 
			 {
			 	name = e.FirstName + " " + e.LastName,
				Skills = e.EmployeeSkills
							.Select (es => new 
								{
								  Description = es.Skill.Description,
								   
									  Level = es.Level == 1? "Novice":
									  		  es.Level == 2 ? "Proficient" : "Expert",
									  YearsOfexperience = es.YearsOfExperience
								})
			  });

q3.Dump();


//04)From the shifts scheduled for NAIT's placement contracts, 
//show the number of employees needed for each day (ordered by day-of-week). 
//Display the name of the day of week (Sunday, as the first day of the week, is number zero) and the number of employees needed.

//Shift - employees, dayofWeek
//Schedule Shift for NAIt
var q4 = Shifts
			.Where (s => s.PlacementContract.Location.Name.Contains ("Nait")) // (filters shifts for Nait)
			.GroupBy (s => s.DayOfWeek)
			//how do i get the information each day of the week
			.Select (gsd => new {
						DatofWeek = gsd.Key == 0 ? "Sun" :
						gsd.Key == 1 ? "Mon" : 
						gsd.Key == 2 ? "Tue" : 
						gsd.Key == 3 ? "Wed" :
						gsd.Key == 4 ? "Thu" :
						gsd.Key == 5 ? "Fri" : "Sat",
						
						NumberOfEmployees = gsd.Sum(x => x.NumberOfEmployees),
						shifts = gsd
						       .Select ( y => new 
							   		{
										id = y.ShiftID, 
										Start = y.StartTime,
										End = y.EndTime,
										NoE = y.NumberOfEmployees
									})
				});
q4.Dump();

//05)List all the employees with the most years of experience.
//what is the most years of expe.(add up yOf exp for each employees)
//once i know that I can compare to that with my employees (yoExp).
var parta = Employees
				.Select( x =>  new 
					{
					  Name = x.FirstName + " " + x.LastName,
					  YOE = x.EmployeeSkills.Sum(es => es.YearsOfExperience)//add up yOf exp for each employees
					})
					.OrderByDescending (x => x.YOE);					
					//.Dump();
var maxYears = parta
				.Max( p => p.YOE);
				//.Dump();

var q5 = parta
		.Where (x => x.YOE == maxYears);
		
q5.Dump();


//Question 5 second Ans
//got original list
//put in piles (group)descending
//max on the top of the piles
var q5onequery = Employees
					.Select( x =>  new 
					{
					  Name = x.FirstName + " " + x.LastName,
					  YOE = x.EmployeeSkills.Sum(es => es.YearsOfExperience)//add up yOf exp for each employees
					})
					.GroupBy(x => x.YOE)
					.OrderByDescending (x => x.Key)
					.First()
					.Dump();
					


//06)For the month of March, list the total earnings per employee along with the number of shifts, the regular earnings, and overtime earnings.
//Note 1: Remember that handling DateTime and TimeSpan calculations is best done in-memory; therefore, you should use a .ToList() in your linq's from clause so that the linq query is not converted to SQL.
//Note 2: When doing your earnings calculations, remember that it's permissible to use method syntax inside of your linq query syntax.

//Scedules - date (i can get month), wages, overtime 

var q6 = Schedules
			.Where (x => x.Day.Month == 3)
			.ToList()// got data into memory
			.GroupBy(x => x.Employee)
			// group on the firstname
			.Select(x => new 
				{
				  Name = x.Key.FirstName + " " + x.Key.LastName,
				  RegularEarnings = x.Where(y => !y.OverTime).Sum(y => y.HourlyWage * (y.Shift.EndTime - y.Shift.StartTime).Hours).ToString("0.00"),  //record is not an ovetime record i want to sum m
				  OverTimeEarnings = x.Where(y => y.OverTime).Sum(y => y.HourlyWage * (y.Shift.EndTime - y.Shift.StartTime).Hours * 1.5m).ToString("0.00"),
				  NumberOfShifts = x.Count()
				});
			
	q6.Dump();




 























