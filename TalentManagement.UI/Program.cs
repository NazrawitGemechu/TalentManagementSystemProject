using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TalentManagement.Application.Queries.TalentQuery;
using TalentManagement.Persistance.Configurations;
using TalentManagement.Persistance.Data;
using TalentManagement.UI.Data;
using TalentManagement.UI.Models.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), builder => builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), builder => builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllTalentsQuery).GetTypeInfo().Assembly));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultUI();
//builder.Services.Configure<IdentityOptions>(opts =>
//{
//    opts.SignIn.RequireConfirmedEmail = true;
//});
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Main/Accessdenied");
});
builder.Services.AddRazorPages();
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
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Home}/{id?}");
app.MapRazorPages();
    


app.Run();
