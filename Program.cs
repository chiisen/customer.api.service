using customer.api.service.Middleware;
using customer.api.service.Model;
using customer.api.service.Service;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
