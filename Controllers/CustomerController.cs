using Microsoft.AspNetCore.Mvc;

namespace customer.api.service.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;


    public CustomerController(ILogger<CustomerController> logger)
    {
        _logger = logger;
    }
}