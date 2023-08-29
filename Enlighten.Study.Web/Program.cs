using Common.Web;
using Enlighten.Core.Models;
using Enlighten.Core.Services;
using Enlighten.Data.Configuration;
using Enlighten.Data.Infrastructure;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Study.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

builder.Services.AddTransient<TextbookService>();//dbcontext should be injected too, since we are using it in the service

//settings
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();

var gptClientSettings = configuration.BindAndAddSingleton<GptClientSettingsModel>(builder.Services, "GptClientSettings");
var dataSettingsModel = configuration.BindAndAddSingleton<DataSettingsModel>(builder.Services, "DataSettings");
var gptDefaults = configuration.BindAndAddSingleton<DefaultGptAppSettingsModel>(builder.Services, "GptAppDefaults");


//ef context
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(dataSettingsModel.DataContext));
builder.Services.AddTransient<GptPromptService>();//all the constructors are injected too



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();