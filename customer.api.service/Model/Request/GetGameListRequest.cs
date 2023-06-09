using customer.api.service.Service;
using System.ComponentModel.DataAnnotations;

namespace customer.api.service.Model.Request;

public class GetGameListRequest
{
    /// <summary>
    /// ListGames – 固定值
    /// </summary>
    [Required]
    public string Method { get; set; } = "ListGames";
    /// <summary>
    /// UNIX 时间戳
    /// </summary>
    [Required]
    public long Timestamp { get; set; } = Helper.GetCurrentTimestamp();
}