using GestionnaireRecettes.data;
using GestionnaireRecettes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RecettesAppContextConnection") ?? throw new InvalidOperationException("Connection string 'RecettesAppContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<RecettesAppContext>(options =>
{
    options.UseSqlServer(connectionString);
});




builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<RecettesAppContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



app.UseEndpoints(endpoints =>
{

    // Route for ingredients with a recipe ID
    _ = endpoints.MapControllerRoute(
        name: "ingredients",
        pattern: "ingredients/{recetteId}",
        defaults: new { controller = "Ingredient", action = "Index" });
 
    _ = endpoints.MapControllerRoute(
        name: "ingredientDelete",
        pattern: "ingredients/delete",
        defaults: new { controller = "Ingredient", action = "Delete" });

    _ = endpoints.MapControllerRoute(
        name: "steps",
        pattern: "steps/{recetteId}",
        defaults: new { controller = "Step", action = "Index" });

    _ = endpoints.MapControllerRoute(
        name: "steDelete",
        pattern: "^step/delete",
        defaults: new { controller = "Step", action = "Delete" });


});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.MapRazorPages();

app.Run();
