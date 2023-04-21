using customer.api.service.Service;
using System.ComponentModel.DataAnnotations;

namespace customer.api.service.Model.Request
{
    public class TransferCreditRequest
    {
        /// <summary>
        /// TC – 固定值
        /// </summary>
        [Required]
        public string Method { get; set; } = "TC";
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
        /// <summary>
        /// 這是一個唯一的密鑰，用於驗證轉賬到提供者系統的金額（字母數字字符。最大長度是 50）
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? RequestID { get; set; }
        /// <summary>
        /// 1.正數：將金額轉入提供者的系統
        /// 2.負數：將金額從提供者的系統中轉出
        /// </summary>
        [Required]
        public string? Amount { get; set; }
    }
}
