using customer.api.service.Model.Request;
using customer.api.service.Model.Response;
using customer.api.service.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace customer.api.service.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _service;


    public CustomerController(ILogger<CustomerController> logger, ICustomerService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// 獲取遊戲列表
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /GetGameList
    ///     {
    ///     }
    /// </remarks>
    [HttpPost("GetGameList")]
    [Produces("application/json")]
    public async Task<GetGameListResponse> GetGameListAsync()
    {
        return await _service.GetGameListAsync();
    }

    /// <summary>
    /// 取得遊戲 Token
    /// </summary>
    /// <param name="source"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /GetGameToken
    ///     {
    ///        "Username": "test007"
    ///     }
    /// </remarks>
    [HttpPost("GetGameToken")]
    [Produces("application/json")]
    public async Task<GetGameTokenResponse> GetGameTokenAsync(GetGameTokenRequest source)
    {
        return await _service.GetGameTokenAsync(source);
    }

    /// <summary>
    /// 獲取信用
    /// </summary>
    /// <param name="source"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /GetCredit
    ///     {
    ///        "Username": "test007"
    ///     }
    /// </remarks>
    [HttpPost("GetCredit")]
    [Produces("application/json")]
    public async Task<GetCreditResponse> GetCreditAsync(GetCreditRequest source)
    {
        return await _service.GetCreditAsync(source);
    }

    /// <summary>
    /// 轉移信用
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /TransferCredit
    ///     {
    ///        "Username": "test007",
    ///        "RequestID": "41005D02B17947C9AE9FEDAF069619D5",
    ///        "Amount": "100.00"
    ///     }
    /// </remarks>
    [HttpPost("TransferCredit")]
    [Produces("application/json")]
    public async Task<TransferCreditResponse> TransferCreditAsync(TransferCreditRequest source)
    {
        return await _service.TransferCreditAsync(source);
    }

    /// <summary>
    /// 驗證轉移信用
    /// 響應 - 成功：HTTP / 1.1 200 OK
    /// 響應-失敗：HTTP/1.1 404 Not Found 表示 requestId 不存在
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /ValidTransferCredit
    ///     {
    ///        "RequestID": "41005D02B17947C9AE9FEDAF069619D5"
    ///     }
    /// </remarks>
    [HttpPost("ValidTransferCredit")]
    [Produces("application/json")]
    public async Task<ValidTransferCreditResponse> ValidTransferCreditAsync(ValidTransferCreditRequest source)
    {
        return await _service.ValidTransferCreditAsync(source);
    }

    /// <summary>
    /// 提款所有信用
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /TransferOutAllCredit
    ///     {
    ///        "Username": "test007",
    ///        "RequestID": "BA879793A97946BB91EF5FEF970FE60B"
    ///     }
    /// </remarks>
    [HttpPost("TransferOutAllCredit")]
    [Produces("application/json")]
    public async Task<TransferOutAllCreditResponse> TransferOutAllCreditAsync(TransferOutAllCreditRequest source)
    {
        return await _service.TransferOutAllCreditAsync(source);
    }

    /// <summary>
    /// 創建用戶
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /CreatePlayer
    ///     {
    ///        "Username": "test007"
    ///     }
    /// </remarks>
    [HttpPost("CreatePlayer")]
    [Produces("application/json")]
    public async Task<CreatePlayerResponse> CreatePlayerAsync(CreatePlayerRequest source)
    {
        return await _service.CreatePlayerAsync(source);
    }

    /// <summary>
    /// 註銷用戶
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /KickPlayer
    ///     {
    ///        "Username": "test007"
    ///     }
    /// </remarks>
    [HttpPost("KickPlayer")]
    [Produces("application/json")]
    public async Task<KickPlayerResponse> KickPlayerAsync(KickPlayerRequest source)
    {
        return await _service.KickPlayerAsync(source);
    }

    /// <summary>
    /// 取得注單明細
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /GetBetDetail
    ///     {
    ///        "StartDate": "2023-04-24 15:00",
    ///        "EndDate": "2023-04-24 15:10",
    ///        "NextId": "",
    ///        "Delay": 0
    ///     }
    /// </remarks>
    [HttpPost("GetBetDetail")]
    [Produces("application/json")]
    public async Task<GetBetDetailResponse> GetBetDetailAsync(GetBetDetailRequest source)
    {
        return await _service.GetBetDetailAsync(source);
    }

    /// <summary>
    /// 檢索歷史 URL
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /GetGameHistoryUrl
    ///     {
    ///        "OCode": "s1yc3uqnaxycn",
    ///        "Language": "en",
    ///        "Type": "Game"
    ///     }
    /// </remarks>
    [HttpPost("GetGameHistoryUrl")]
    [Produces("application/json")]
    public async Task<GetGameHistoryUrlResponse> GetGameHistoryUrlAsync(GetGameHistoryUrlRequest source)
    {
        return await _service.GetGameHistoryUrlAsync(source);
    }

    /// <summary>
    /// 檢索輸贏
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /GetWinLoseSummary
    ///     {
    ///        "StartDate": "2023-04-24 15:00",
    ///        "EndDate": "2023-04-24 15:10",
    ///        "Username": "test007"
    ///     }
    /// </remarks>
    [HttpPost("GetWinLoseSummary")]
    [Produces("application/json")]
    public async Task<GetWinLoseSummaryResponse> GetWinLoseSummaryAsync(GetWinLoseSummaryRequest source)
    {
        return await _service.GetWinLoseSummaryAsync(source);
    }
}