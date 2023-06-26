using customer.api.service.Service;
using System.ComponentModel.DataAnnotations;

namespace customer.api.service.Model.Request
{
    public class GetGameHistoryUrlRequest
    {
        /// <summary>
        /// History – 固定值
        /// </summary>
        [Required]
        public string Method { get; set; } = "History";
        /// <summary>
        /// 遊戲事務代碼（遊戲注單號）
        /// </summary>
        [Required]
        public string? OCode { get; set; }
        /// <summary>
        /// 玩家的首選語言（ISO 639-1，包含 2 個字母的代碼） 
        /// 例如 en、zh、th
        /// </summary>
        [Required]
        public string? Language { get; set; }
        /// <summary>
        /// 事務類型。例如 Game
        /// </summary>
        [Required]
        public string? Type { get; set; }
        /// <summary>
        /// UNIX 時間戳
        /// </summary>
        [Required]
        public long Timestamp { get; set; } = Helper.GetCurrentTimestamp();
    }
}
