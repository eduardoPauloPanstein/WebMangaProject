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
using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddTransient<IMangaService, MangaService>();
builder.Services.AddTransient<IMangaDAL, MangaDAL>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserDAL, UserDAL>();

builder.Services.AddTransient<IAnimeService, AnimeService>();
builder.Services.AddTransient<IAnimeDAL, AnimeDAL>();

builder.Services.AddTransient<IMangaComentary, MangaComentaryService>();
builder.Services.AddTransient<IAnimeComentary, AnimeComentaryService>();
builder.Services.AddTransient<IUserMangaItemService, UserMangaItemService>();
builder.Services.AddTransient<IUserAnimeItemService, UserAnimeItemService>();

builder.Services.AddTransient<IUserMangaItemDAL, UserMangaItemDAL>();
builder.Services.AddTransient<IMangaComentaryDAL, MangaComentaryDAL>();
builder.Services.AddTransient<IAnimeComentaryDAL, AnimeComentaryDAL>();
builder.Services.AddTransient<IUserAnimeItemDAL, UserAnimeItemDAL>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<ILog>(LogManager.GetLogger(typeof(object)));

builder.Services.AddDbContext<MangaProjectDbContext>(options => options.UseSqlServer("name=ConnectionStrings:SqlServerMangaProjectConnection"));


builder.Services.AddCors();
var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
    {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //no nosso caso de ser apenas uma api, não sendo um login distribuido fica como padrão mesmo
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true; //configrar onde salvar
        x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, //
            ValidateAudience = false //
        };
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Logging.AddLog4Net();

var app = builder.Build();
// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors();
app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
