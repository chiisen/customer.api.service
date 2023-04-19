using customer.api.service.Model.Request;
using customer.api.service.Model.Response;

namespace customer.api.service.Service;

public interface ICustomerService
{
    /// <summary>
    /// 创建玩家账号
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    Task<CreateResponse> CreateAsync(CreateRequest source);
}