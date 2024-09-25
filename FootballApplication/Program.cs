using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using FootballApplication1;
using FootballApplication1.Mapper;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    });
});
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ITeamsService, TeamsManager>();
builder.Services.AddScoped<ITeamsDal, EfTeamsDal>();

builder.Services.AddScoped<ITeamsPointsService, TeamsPointsManager>();
builder.Services.AddScoped<ITeamPointsDal, EfTeamPointsDal>();

builder.Services.AddScoped<IWinRateService, Win_winRateService>();
builder.Services.AddScoped<IWinRateDal, EfWinRateDal>();

builder.Services.AddScoped<IMatchDatesService, Match_matchDatesService>();
builder.Services.AddScoped<IMatchDatesDal, EfMatchDatesDal>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<SignalRHub>("/signalrhub");
app.Run();
