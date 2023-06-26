using customer.api.service.Service;
using System.ComponentModel.DataAnnotations;

namespace customer.api.service.Model.Request
{
    public class GetGameTokenRequest
    {
        /// <summary>
        /// PLAY – 固定值
        /// </summary>
        [Required]
        public string Method { get; set; } = "PLAY";
        /// <summary>
        /// 玩家用戶名（4~20 個字母數字字符、下劃線，不區分大小寫）
        /// </summary>
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string? Username { get; set; }
        /// <summary>
        /// UNIX 時間戳
        /// </summary>
        [Required]
        public long Timestamp { get; set; } = Helper.GetCurrentTimestamp();
    }
}
