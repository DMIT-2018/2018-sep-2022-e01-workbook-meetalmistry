using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//
//
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");




//given for the db connection to Defaultconnection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//code the dbconnection to the application DB context for chinook
//the implementation of the connect AND registration of the chinooksystem services will be done in the chinook system class library
//so to accomplish this task we will beusing  an "extention method" 
//the extention methd will extend IserviceCollection class. it will requires a parameter options.UseSqlServer where xxx is the connection string variable
//builder.Services.ChinookSystemBackendDependencies(Options => Options.UseSqlServer(connectionStringChinook) );


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
