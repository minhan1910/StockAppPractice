using StockAppPractice;
using StockAppPractice.HttpClientHandler;
using StockAppPractice.ServiceContracts;
using StockAppPractice.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IHttpClientProcessor, HttpClientProcessor>();

builder.Services.Configure<TradingOptions>(
    builder.Configuration.GetSection(nameof(TradingOptions)));

var app = builder.Build();

app.UseStaticFiles();   
app.UseRouting();
app.MapControllers();

app.Run();
