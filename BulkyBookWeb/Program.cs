// Program.cs is your entry point to the web application (like server.js file)
// you need a builder object to build your web app
using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the builder container.
builder.Services.AddControllersWithViews();
// use the DB service
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
// build your application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// all the below middlewares are order specific 
// different order leads to different order in the pipeline process of .NET
// 1. enable https redirection of different routes
app.UseHttpsRedirection();
// 2. use the static files before they got overriden by the dynamic content of routing
app.UseStaticFiles();
// 3. the dynamic content comes after
app.UseRouting();
// 4. requests need to be authorized (user is authorized to use the system)
app.UseAuthorization();
// now start mapping the requests to the right controller
// starting with the first default controller
// controller and actions are NOT optional like the ID
//that's why we specified a default one!
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();