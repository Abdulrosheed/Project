using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Project.ApplicationContext;
using Project.BackGroundTaskConfiguration;
using Project.Repository.Implementation;
using Project.Repository.Interface;
using Project.Service.Implementation;
using Project.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserRepository , UserRepository>();
builder.Services.AddScoped<IUserService , UserService>();
builder.Services.AddScoped<ICarRepository , CarRepository>();
builder.Services.AddScoped<ICarService , CarService>();
var connectionString = "Data Source=db;Initial Catalog=car-db; User Id=sa;PassWord=A&VeryComplex123Password";
builder.Services.AddDbContext<ContextClass>(options =>
                options.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.LoginPath = "/car/login";
                    config.Cookie.Name = "CarMVC";
                    config.LogoutPath = "/car/logout";
                });
                
builder.Services.Configure<BackGroundConfig>(builder.Configuration.GetSection("ReminderConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
