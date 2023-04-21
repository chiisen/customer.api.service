using customer.api.service.Model.Request;
using customer.api.service.Model.Response;

namespace customer.api.service.Service;

public interface ICustomerService
{
    /// <summary>
    /// 獲取遊戲列表
    /// </summary>
    /// <returns></returns>
    Task<GetGameListResponse> GetGameListAsync();

    /// <summary>
    /// 取得遊戲 Token
    /// </summary>
    Task<GetGameTokenResponse> GetGameTokenAsync(GetGameTokenRequest source);

    /// <summary>
    /// 獲取信用
    /// </summary>
    Task<GetCreditResponse> GetCreditAsync(GetCreditRequest source);

    /// <summary>
    /// 轉移信用
    /// </summary>
    Task<TransferCreditResponse> TransferCreditAsync(TransferCreditRequest source);

    /// <summary>
    /// 驗證轉移信用
    /// 響應 - 成功：HTTP / 1.1 200 OK
    /// 響應-失敗：HTTP/1.1 404 Not Found 表示 requestId 不存在
    /// </summary>
    Task<ValidTransferCreditResponse> ValidTransferCreditAsync(ValidTransferCreditRequest source);

    /// <summary>
    /// 提款所有信用
    /// </summary>
    Task<TransferOutAllCreditResponse> TransferOutAllCreditAsync(TransferOutAllCreditRequest source);

    /// <summary>
    /// 創建用戶
    /// </summary>
    Task<CreatePlayerResponse> CreatePlayerAsync(CreatePlayerRequest source);

    /// <summary>
    /// 註銷用戶
    /// </summary>
    Task<KickPlayerResponse> KickPlayerAsync(KickPlayerRequest source);

    /// <summary>
    /// 取得注單明細
    /// </summary>
    Task<GetBetDetailResponse> GetBetDetailAsync(GetBetDetailRequest source);

    /// <summary>
    /// 檢索歷史 URL
    /// </summary>
    Task<GetGameHistoryUrlResponse> GetGameHistoryUrlAsync(GetGameHistoryUrlRequest source);

    /// <summary>
    /// 檢索輸贏
    /// </summary>
    Task<GetWinLoseSummaryResponse> GetWinLoseSummaryAsync(GetWinLoseSummaryRequest source);
}