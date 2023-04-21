using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using _31Library;
using customer.api.service.Model;
using customer.api.service.Model.Request;
using customer.api.service.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace customer.api.service.Service;

public class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private HttpHelper _httpHelper;

    public CustomerService(ILogger<CustomerService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _httpHelper = new HttpHelper(_httpClientFactory);
    }

    /// <summary>
    /// 註銷用戶
    /// </summary>
    public async Task<KickPlayerResponse> KickPlayerAsync(KickPlayerRequest source)
    {
        var response = await ApiHandler(source);
        return JsonConvert.DeserializeObject<KickPlayerResponse>(response);
    }

    /// <summary>
    /// 取得注單明細
    /// </summary>
    public async Task<GetBetDetailResponse> GetBetDetailAsync(GetBetDetailRequest source)
    {
        var response = await ApiHandler(source);
        return JsonConvert.DeserializeObject<GetBetDetailResponse>(response);
    }

    /// <summary>
    /// 檢索歷史 URL
    /// </summary>
    public async Task<GetGameHistoryUrlResponse> GetGameHistoryUrlAsync(GetGameHistoryUrlRequest source)
    {
        var response = await ApiHandler(source);
        return JsonConvert.DeserializeObject<GetGameHistoryUrlResponse>(response);
    }

    /// <summary>
    /// 檢索輸贏
    /// </summary>
    public async Task<GetWinLoseSummaryResponse> GetWinLoseSummaryAsync(GetWinLoseSummaryRequest source)
    {
        var response = await ApiHandler(source);
        return JsonConvert.DeserializeObject<GetWinLoseSummaryResponse>(response);
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
            // 響應-失敗：HTTP/1.1 404 Not Found 表示 requestId 不存在
            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception(HttpStatusCode.NotFound.ToString());
            }

            throw new Exception($"Call JokerApi Failed:{ex}");
        }
    }

    /// <summary>
    /// 獲取遊戲列表
    /// </summary>
    /// <returns></returns>
    public async Task<GetGameListResponse> GetGameListAsync()
    {
        var response = await ApiHandler(new GetGameListRequest());
        return JsonConvert.DeserializeObject<GetGameListResponse>(response);
    }

    /// <summary>
    /// 取得遊戲 Token
    /// </summary>
    public async Task<GetGameTokenResponse> GetGameTokenAsync(GetGameTokenRequest source)
    {
        var response = await ApiHandler(source);
        return JsonConvert.DeserializeObject<GetGameTokenResponse>(response);
    }

    /// <summary>
    /// 獲取信用
    /// </summary>
    public async Task<GetCreditResponse> GetCreditAsync(GetCreditRequest source)
    {
        var response = await ApiHandler(source);
        return JsonConvert.DeserializeObject<GetCreditResponse>(response);
    }

    /// <summary>
    /// 轉移信用
    /// </summary>
    public async Task<TransferCreditResponse> TransferCreditAsync(TransferCreditRequest source)
    {
        var response = await ApiHandler(source);
        return JsonConvert.DeserializeObject<TransferCreditResponse>(response);
    }

    /// <summary>
    /// 驗證轉移信用
    /// 響應 - 成功：HTTP / 1.1 200 OK
    /// 響應-失敗：HTTP/1.1 404 Not Found 表示 requestId 不存在
    /// </summary>
    public async Task<ValidTransferCreditResponse> ValidTransferCreditAsync(ValidTransferCreditRequest source)
    {
        var response = await ApiHandler(source);
        return JsonConvert.DeserializeObject<ValidTransferCreditResponse>(response);
    }

    /// <summary>
    /// 提款所有信用
    /// </summary>
    public async Task<TransferOutAllCreditResponse> TransferOutAllCreditAsync(TransferOutAllCreditRequest source)
    {
        var response = await ApiHandler(source);
        return JsonConvert.DeserializeObject<TransferOutAllCreditResponse>(response);
    }

    /// <summary>
    /// 創建用戶
    /// </summary>
    public async Task<CreatePlayerResponse> CreatePlayerAsync(CreatePlayerRequest source)
    {
        var response = await ApiHandler(source);
        return JsonConvert.DeserializeObject<CreatePlayerResponse>(response);
    }
}


