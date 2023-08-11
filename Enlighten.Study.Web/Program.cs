using Enlighten.Data.Configuration;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Study.Core.Configuration;
using Enlighten.Study.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);


//settings
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();


var coreSettings = new CoreSettingsModel();
configuration.GetSection("CoreSettings").Bind(coreSettings);
builder.Services.AddSingleton(coreSettings);


var gptClientSettings = new GptClientSettingsModel();
configuration.GetSection("GptClientSettings").Bind(gptClientSettings);
builder.Services.AddSingleton(gptClientSettings);


var dataSettingsModel = new DataSettingsModel();
configuration.GetSection("DataSettings").Bind(dataSettingsModel);
builder.Services.AddSingleton(dataSettingsModel);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
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