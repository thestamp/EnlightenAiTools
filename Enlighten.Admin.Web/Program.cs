using Enlighten.Admin.Core.Services;
using Enlighten.Admin.Web.Data;
using Enlighten.Core.Models;
using Enlighten.Core.Services;
using Enlighten.Data.Configuration;
using Enlighten.Data.Infrastructure;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Study.Core.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();



builder.Services.AddTransient<TextbookService>();//dbcontext should be injected too, since we are using it in the service
builder.Services.AddTransient<TextbookAdminService>();//dbcontext should be injected too, since we are using it in the service

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

var gptDefaults = new DefaultGptAppSettingsModel();
configuration.GetSection("GptAppDefaults").Bind(gptDefaults);
builder.Services.AddSingleton(gptDefaults);

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