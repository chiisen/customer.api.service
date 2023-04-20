using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using _31Library;
using customer.api.service.Model.Request;
using customer.api.service.Model.Response;
using Newtonsoft.Json;

namespace customer.api.service.Service;

public class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiDomain = "https://api.pg-bo.me/external/";
    private HttpHelper _httpHelper;

    public CustomerService(ILogger<CustomerService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _httpHelper = new HttpHelper(_httpClientFactory);
    }

    /// <summary>
    /// 获取游戏列表
    /// </summary>
    /// <returns></returns>
    public async Task<GetGameListResponse> GetGameListAsync()
    {
        var response = await ApiHandler(new GetGameListRequest());
        return JsonConvert.DeserializeObject<GetGameListResponse>(response);
    }

    /// <summary>
    /// API 請求
    /// </summary>
    private async Task<string> ApiHandler(object source)
    {
        var sw = new Stopwatch();
        sw.Start();

        var apiResInfo = new ApiResponseData();

        try
        {
            var rowData = Helper.ConvertToKeyValue(source);
            var signature = Helper.GetHMACSHA1Signature(rowData, "aghcgqczxur9a");

            var apiUrl = "https://w.api788.net/" + $"?appid=THCN&signature={Uri.EscapeDataString(signature)}";

            var response = await _httpHelper.Post(apiUrl, source);
            apiResInfo.ElapsedMilliseconds = sw.ElapsedMilliseconds;
            sw.Stop();


            var dics = new Dictionary<string, object>
            {
                { "request", JsonConvert.SerializeObject(source) },
                { "response", response.body.Length > 10000 ? response.body.Substring(0, 9999):response }
            };
            using (var scope = _logger.BeginScope(dics))
            {
                _logger.LogInformation("Get RequestPath: {RequestPath} | ResponseHttpStatus:{Status} | exeTime:{exeTime} ms", apiUrl, response.statusCode, sw.Elapsed.TotalMilliseconds);
            }

            return response.body;
        }
        catch (TaskCanceledException ex)
        {
            apiResInfo.ElapsedMilliseconds = 99999;
            throw new TaskCanceledException(ex.ToString());
        }
        catch (HttpRequestException ex)
        {
            // 响应-失败：HTTP/1.1 404 Not Found 表示 requestId 不存在
            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception(HttpStatusCode.NotFound.ToString());
            }

            throw new Exception($"Call JokerApi Failed:{ex}");
        }
    }
}





public class ApiResponseData
{
    public long reqDateTime { get; set; }
    public long ElapsedMilliseconds { get; set; }
    public ApiResponseData()
    {
        reqDateTime = DateTimeOffset.Now.ToUnixTimeSeconds();
    }
}

public static class Helper
{
    /// <summary>
    /// 轉換成 Key=Value
    /// </summary>
    public static string ConvertToKeyValue<T>(T source) where T : class
    {
        var type = source.GetType();
        var properties = type.GetProperties();
        var list = properties.OrderBy(x => x.Name).Select(x => x.Name + "=" + x.GetValue(source)).ToList();
        return string.Join("&", list);
    }

    /// <summary>
    /// 簽名
    /// </summary>
    public static string GetHMACSHA1Signature(string rawData, string secretKey)
    {
        using (var sha = new HMACSHA1(Encoding.UTF8.GetBytes(secretKey)))
        {
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return Convert.ToBase64String(hash);
        }
    }

    public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
    /// <summary>
    /// 時間戳
    /// </summary>
    public static int GetCurrentTimestamp()
    {
        return (int)DateTime.UtcNow.Subtract(UnixEpoch).TotalSeconds;
    }
}