using customer.api.service.Service;
using System.ComponentModel.DataAnnotations;

namespace customer.api.service.Model.Request
{
    public class ValidTransferCreditRequest
    {
        /// <summary>
        /// TCH – 固定值
        /// </summary>
        [Required]
        public string Method { get; set; } = "TCH";
        /// <summary>
        /// UNIX 時間戳
        /// </summary>
        [Required]
        public long Timestamp { get; set; } = Helper.GetCurrentTimestamp();
        /// <summary>
        /// 需要驗證 RequestID
        /// </summary>
        [Required]
        public string? RequestID { get; set; }
    }
}
