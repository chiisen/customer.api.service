using customer.api.service.Service;
using System.ComponentModel.DataAnnotations;

namespace customer.api.service.Model.Request
{
    public class CreatePlayerRequest
    {
        /// <summary>
        /// WAC – 固定值
        /// </summary>
        [Required]
        public string Method { get; set; } = "CU";
        /// <summary>
        /// 玩家用戶名
        /// </summary>
        [Required]
        public string? Username { get; set; }
        /// <summary>
        /// UNIX 時間戳
        /// </summary>
        [Required]
        public long Timestamp { get; set; } = Helper.GetCurrentTimestamp();
    }
}
