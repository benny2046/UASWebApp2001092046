using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UASWebApp2001092046.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//menambahkan pengaturan Identity dan menambahkan Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options=>
    options.SignIn.RequireConfirmedAccount=true)
    .AddRoles<IdentityRole>()
    .AddDefaultUI().AddDefaultTokenProviders().AddEntityFrameworkStores<UasDbContext>();

//mendaftarkan ef
object value = builder.Services.AddDbContext<UasDbContext>(
    options=> options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))
);

//menggunakan DI
builder.Services.AddTransient<IProduct,ProductDAL>();

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
app.UseAuthentication();;

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(name:"Identity",areaName:"Identity",
pattern:"Identity/{controller=Home}/{action=Index}/{Id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();
