using customer.api.service.Model.Request;
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
    /// 创建玩家账号
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create")]
    [Produces("application/json")]
    public async Task<CreateResponse> CreateAsync([FromBody] CreateRequest source)
    {
        return await _service.CreateAsync(source);
    }
}