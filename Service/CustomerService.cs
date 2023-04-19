using customer.api.service.Model.Request;
using customer.api.service.Model.Response;
using Newtonsoft.Json;

namespace customer.api.service.Service;

public class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiDomain = "https://api.pg-bo.me/external/";

    public CustomerService(ILogger<CustomerService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// 创建玩家账号
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public async Task<CreateResponse> CreateAsync(CreateRequest source)
    {
        var path = "Player/v1/Create";
        var query = $"?trace_id={Guid.NewGuid()}";
        var url = _apiDomain + path + query;

        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(source));
        var response = await Post(url, dictionary);
        return JsonConvert.DeserializeObject<CreateResponse>(response);
    }

    public async Task<string> Post(string url, Dictionary<string, string> postData, int retry = 3)
    {
        HttpResponseMessage? response = null;
        try
        {
            using (var request = _httpClientFactory.CreateClient("log"))
            {
                request.Timeout = TimeSpan.FromSeconds(14);
                var content = new FormUrlEncodedContent(postData);
                var sw = System.Diagnostics.Stopwatch.StartNew();
                response = await request.PostAsync(url, content);
                sw.Stop();

                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                try
                {
                    //拉注單log過長截斷
                    if (url.Contains("Bet/v4/GetHistory") && url.Length > 10000)
                    {
                        var reponse = body.Substring(0, 9999);
                        var dics = new Dictionary<string, object>
                            {
                                { "request", JsonConvert.SerializeObject(postData) },
                                { "response", reponse }
                            };
                        using (var scope = _logger.BeginScope(dics))
                        {
                            _logger.LogInformation("Get RequestPath: {RequestPath} | ResponseHttpStatus:{Status} | exeTime:{exeTime} ms", url, response.StatusCode, sw.Elapsed.TotalMilliseconds);
                        }
                    }
                    else
                    {
                        var dics = new Dictionary<string, object>
                            {
                                { "request", JsonConvert.SerializeObject(postData) },
                                { "response", body }
                            };
                        using (var scope = _logger.BeginScope(dics))
                        {
                            _logger.LogInformation("Get RequestPath: {RequestPath} | ResponseHttpStatus:{Status} | exeTime:{exeTime} ms", url, response.StatusCode, sw.Elapsed.TotalMilliseconds);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                    var errorFile = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileName();
                    _logger.LogError("log exception EX : {ex}  MSG : {Message} Error Line : {errorFile}.{errorLine}", ex.GetType().FullName, ex.Message, errorFile, errorLine);
                }

                return body;
            }
        }
        catch (TaskCanceledException ex)
        {
            throw new TaskCanceledException(ex.ToString());
        }
        catch (HttpRequestException)
        {
            if (retry == 0)
            {
                throw new Exception(string.Format("Call PgApi Failed:{0}", url));
            }

            return await Post(url, postData, retry - 1);
        }
    }
}