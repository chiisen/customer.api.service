using customer.api.service.Service;
using System.ComponentModel.DataAnnotations;

namespace customer.api.service.Model.Request
{
    public class GetCreditRequest
    {
        /// <summary>
        /// GC – 固定值
        /// </summary>
        [Required]
        public string Method { get; set; } = "GC";
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
