using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Repositories;
using SmartPortalApp.Security;
using SmartPortalApp.Middleware;
using SmartPortalApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<CourseService>();
//builder.Services.AddScoped<StudentService>();
//builder.Services.AddScoped<DepartmentService>();
//builder.Services.AddScoped<GradeService>();
//builder.Services.AddScoped<MinimumRequirementService>();
//builder.Services.AddScoped<PointService>();
//builder.Services.AddScoped<SchoolService>();
//builder.Services.AddScoped<SubjectService>();
//builder.Services.AddScoped<TeachingSubjectService>();
//builder.Services.AddScoped<RoleService>();

// Configure database context.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories.
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configure authentication and cookie settings.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
    });

// Add custom cookie authentication events.
builder.Services.AddScoped<CustomCookieAuthenticationEvents>();

// Configure cookie policy options globally.
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

// Register IHttpContextAccessor.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Add custom cookie policy middleware.
app.UseMiddleware<CookiePolicyMiddleware>();

// Apply global cookie policy configuration.
app.UseCookiePolicy();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Map default route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();

//builder.Services.AddScoped<IUserRepository, UserRepository>();


//// Configure the database context.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Update the connection string in appsettings.json

//// Configure authentication.
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Account/Login";
//        options.LogoutPath = "/Account/Logout";
//        options.AccessDeniedPath = "/Account/AccessDenied";
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//        options.SlidingExpiration = true;
//    });

//// Configure IHttpContextAccessor (to prevent related errors).
//builder.Services.AddHttpContextAccessor();

//builder.Services.AddScoped<CustomCookieAuthenticationEvents>();


//// Register IHttpContextAccessor as a singleton service.
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//// Use Cookie Policy
//app.UseCookiePolicy(new CookiePolicyOptions
//{
//    MinimumSameSitePolicy = SameSiteMode.Strict // Enforce strict SameSite policy for cookies
//});

//app.UseRouting();

//app.UseAuthentication(); // Ensure authentication middleware is added
//app.UseAuthorization();

//// Map default routes.
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Account}/{action=Login}/{id?}"); // Account/Login as the default route

//app.Run();
