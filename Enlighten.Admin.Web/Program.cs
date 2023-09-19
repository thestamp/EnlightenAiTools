using Common.Web;
using Enlighten.Admin.Core.Services;
using Enlighten.Admin.Web.Data;
using Enlighten.Core.Models;
using Enlighten.Core.Services;
using Enlighten.Data.Configuration;
using Enlighten.Data.Infrastructure;
using Enlighten.Gpt.Client.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using ConfigurationExtensions = Common.Web.ConfigurationExtensions;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();



builder.Services.AddTransient<TextbookService>();//dbcontext should be injected too, since we are using it in the service
builder.Services.AddTransient<TextbookAdminService>();//dbcontext should be injected too, since we are using it in the service

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()) // Ensures that the app can find the appsettings.json file in the current directory
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables() // Adds environment variables to the configuration
    .AddUserSecrets<Program>()
    .Build();



var gptClientSettings = configuration.BindAndAddSingleton<GptClientSettingsModel>(builder.Services, "GptClientSettings");
var dataSettingsModel = configuration.BindAndAddSingleton<DataSettingsModel>(builder.Services, "DataSettings");
var gptDefaults = configuration.BindAndAddSingleton<DefaultGptAppSettingsModel>(builder.Services, "GptAppDefaults");


//or we can use this if we don't care so much
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(dataSettingsModel.DataContext));
builder.Services.AddTransient<GptPromptService>();//all the constructors are injected too



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