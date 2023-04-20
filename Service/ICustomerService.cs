using customer.api.service.Model.Response;

namespace customer.api.service.Service;

public interface ICustomerService
{
    /// <summary>
    /// 获取游戏列表
    /// </summary>
    /// <returns></returns>
    Task<GetGameListResponse> GetGameListAsync();
}