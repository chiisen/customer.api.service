using customer.api.service.Middleware;
using customer.api.service.Model;
using customer.api.service.Service;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Serilog will handle logging based on appsettings.json

builder.Host.UseSerilog((context, logger) =>
{
    logger
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.FromLogContext();
});

// 開啟將註解寫到 swagger 上的設定程式碼
# region 開啟將註解寫到 swagger 上的設定程式碼
builder.Services.AddSwaggerGen(options =>
{
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
#endregion 將註解寫到 swagger 上


// Add services to the container.
#region 新增 Service

builder.Services.AddScoped<ICustomerService, CustomerService>();

#endregion 新增 Service

// 新增 Common Service
#region 新增 Common Service

builder.Services.AddScoped<HttpLogHandlerMiddleware>();
builder.Services.AddSingleton<LogHelper<HttpLogHandlerMiddleware>>();
builder.Services.AddSingleton<LogHelper<LogMiddleware>>();

#endregion 新增 Common Service


// 新增 HttpClient
#region 新增 HttpClient

builder.Services.AddHttpClient("log").AddHttpMessageHandler<HttpLogHandlerMiddleware>();

#endregion 新增 HttpClient


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// 顯示目前的 Seq 基本設定
var serverUrl = builder.Configuration.GetValue<string>("Seq:ServerUrl");
app.Logger.LogInformation($"目前 Seq 的 serverUrl 【{serverUrl}】");

var apiKey = builder.Configuration.GetValue<string>("Seq:ApiKey");
app.Logger.LogInformation($"目前 Seq 的 apiKey 【{apiKey}】");

var appId = Environment.GetEnvironmentVariable("AP_ID");
app.Logger.LogInformation($"目前的 appId 【{appId}】");


#region loggerFormat

app.Logger.LogInformation("{System} - {type}, {dbName}, {commandType}, {sqlOrSPName}, {parameters}, {response}, {executionTime}, {environment}",
    "projectName123",
    "DB123",
    "dbName123",
    "commandType123",
    "sqlOrSPName123",
    "parameters123",
    "response123",
    "elapsedMilliseconds123",
    "EnvironmentName123");

app.Logger.LogInformation("{System} - {type}, {url}, {body}, {response}, {executionTime}, {environment}",
    "projectName123",
    "API123",
    "url123",
    "body123",
    "response123",
    "elapsedMilliseconds123",
    "EnvironmentName123");

app.Logger.LogWarning("{System} - {type}, {url}, {body}, {response}, {environment}, {timeSpan}",
    "projectName123",
    "APIRetry123",
    "url123",
    "body123",
    "response123",
    "EnvironmentName123",
    "timeSpan123");

app.Logger.LogInformation("");

#endregion loggerFormat
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 註冊 Middleware
#region 註冊 Middleware

app.UseMiddleware<LogMiddleware>();

#endregion 註冊 Middleware


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
