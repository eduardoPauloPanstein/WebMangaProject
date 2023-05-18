using BusinessLogicalLayer.ApiConsumer.AnimeApi;
using BusinessLogicalLayer.ApiConsumer.CategoryApi;
using BusinessLogicalLayer.ApiConsumer.MangaApi;
using BusinessLogicalLayer.ApiConsumer.NovaPasta;
using BusinessLogicalLayer.Implementations;
using BusinessLogicalLayer.Implementations.UserComentaryService;
using BusinessLogicalLayer.Implementations.UserItemService;
using BusinessLogicalLayer.Interfaces.IAnimeInterfaces;
using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using BusinessLogicalLayer.Interfaces.IUserComentaryService;
using BusinessLogicalLayer.Interfaces.IUserInterfaces;
using BusinessLogicalLayer.Interfaces.IUserItemService;
using DataAccessLayer;
using DataAccessLayer.Implementations;
using DataAccessLayer.Implementations.UserComentaryDAL;
using DataAccessLayer.Implementations.UserItemDAL;
using DataAccessLayer.Interfaces.IAnimeInterfaces;
using DataAccessLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer.Interfaces.IUserComentary;
using DataAccessLayer.Interfaces.IUSerInterfaces;
using DataAccessLayer.Interfaces.IUserItem;
using DataAccessLayer.UnitOfWork;
using Entities.Enums;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using MvcPresentationLayer.Apis.MangaProjectApi;
using MvcPresentationLayer.Apis.MangaProjectApi.Animes;
using MvcPresentationLayer.Apis.MangaProjectApi.ItemComentary.AnimeComentary;
using MvcPresentationLayer.Apis.MangaProjectApi.ItemComentary.MangaComentary;
using MvcPresentationLayer.Apis.MangaProjectApi.Mangas;
using MvcPresentationLayer.Apis.MangaProjectApi.UserItem.UserAnimeItem;
using MvcPresentationLayer.Apis.MangaProjectApi.UserItem.UserMangaItem;
using MvcPresentationLayer.Utilities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);


#region Services

builder.Services.AddTransient<IUserService, BusinessLogicalLayer.Implementations.UserService>();
builder.Services.AddTransient<IMangaService, MangaService>();
builder.Services.AddTransient<IAnimeService, AnimeService>();
builder.Services.AddTransient<IUserMangaItemService, UserMangaItemService>();
builder.Services.AddTransient<IUserAnimeItemService, UserAnimeItemService>();
builder.Services.AddTransient<IMangaComentary, MangaComentaryService>();
builder.Services.AddTransient<IAnimeComentary, AnimeComentaryService>();

builder.Services.AddTransient<ICacheService, CacheService>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

#endregion
#region DAL

builder.Services.AddTransient<IMangaDAL, MangaDAL>();
builder.Services.AddTransient<IUserDAL, UserDAL>();
builder.Services.AddTransient<IAnimeDAL, AnimeDAL>();
builder.Services.AddTransient<IUserMangaItemDAL, UserMangaItemDAL>();
builder.Services.AddTransient<IMangaComentaryDAL, MangaComentaryDAL>();
builder.Services.AddTransient<IAnimeComentaryDAL, AnimeComentaryDAL>();
builder.Services.AddTransient<IUserAnimeItemDAL, UserAnimeItemDAL>();

#endregion
#region MvcApi

builder.Services.AddSingleton<IMangaProjectApiUserService, MangaProjectApiUserService>();
builder.Services.AddSingleton<IMangaProjectApiMangaService, MangaProjectApiMangaService>();
builder.Services.AddSingleton<IMangaProjectApiAnimeService, MangaProjectApiAnimeService>();
builder.Services.AddSingleton<IMangaProjectApiAnimeComentary, MangaProjectApiAnimeComentary>();
builder.Services.AddSingleton<IMangaProjectApiMangaComentary, MangaProjectApiMangaComentary>();
builder.Services.AddSingleton<IMangaProjectApiAnimeItem, MangaProjectApiAnimeItem>();
builder.Services.AddSingleton<IMangaProjectApiMangaItem, MangaProjectApiMangaItem>();

#endregion

builder.Services.AddTransient<ICategoryApiConnect, CategoryApiConnect>();
builder.Services.AddTransient<IApiConnect, ApiConnect>();
builder.Services.AddTransient<IAnimeApiConnect, AnimeApi>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<MangaProjectDbContext>(options => options.UseSqlServer("name=ConnectionStrings:SqlServerMangaProjectConnection"));

builder.Services.AddDistributedRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("AzureRedisCacheConnection");
});

builder.Services.AddAuthentication("CookieSerieAuth")
    .AddCookie("CookieSerieAuth", opt =>
    {
        opt.Cookie.Name = "SeriesAuthCookie";
        opt.LoginPath = "/User/Login"; //401
        opt.AccessDeniedPath = "/Home/Index"; //403 Forbidden !Create view for errors
        opt.ExpireTimeSpan = TimeSpan.FromDays(1);
    });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("User", p => p.RequireRole(UserRoles.User.ToString()));
    opt.AddPolicy(UserRoles.Admin.ToString(), p => p.RequireRole(UserRoles.Admin.ToString()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always
}); 

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
