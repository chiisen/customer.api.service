using customer.api.service.Model.Response;
using customer.api.service.Service;
using Microsoft.AspNetCore.Mvc;

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
    /// 获取游戏列表
    /// </summary>
    /// <returns></returns>
    [HttpPost("GetGameList")]
    [Produces("application/json")]
    public async Task<GetGameListResponse> GetGameListAsync()
    {
        return await _service.GetGameListAsync();
    }
}