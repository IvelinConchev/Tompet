using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tompet.Core.Constants;
using Tompet.Extensions;
using Tompet.Infrastructure.Data;
using Tompet.Infrastructure.Data.Identity;
using Tompet.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationDbContexts(builder.Configuration);

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    //options.Password.RequireDigit = false;
    //options.Password.RequireLowercase = false;
    //options.Password.RequireNonAlphanumeric = false;
    //options.Password.RequireUppercase = false;
})

    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TompetDbContext>();

builder.Services.AddAuthentication()
.AddFacebook(options =>
{
    options.AppId = builder.Configuration.GetValue<string>("Facebook:AppId");
    options.AppSecret = builder.Configuration.GetValue<string>("Facebook:AppSecret");
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential 
    // cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;

    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(FormatingConstant.NormalDateFormat));
        options.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
    });

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddStackExchangeRedisCache(options => 
{
    options.Configuration = builder.Configuration.GetConnectionString("Трябва да се попълне!");
});

builder.Services.ApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.PreparateDatabase();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultAreaRoute();
    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

app.MapControllerRoute(
    name: "Area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
