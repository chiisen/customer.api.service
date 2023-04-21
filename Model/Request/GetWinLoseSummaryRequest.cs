using customer.api.service.Service;
using System.ComponentModel.DataAnnotations;

namespace customer.api.service.Model.Request
{
    public class GetWinLoseSummaryRequest
    {
        /// <summary>
        /// RWL – 固定值
        /// </summary>
        [Required]
        public string Method { get; set; } = "RWL";
        /// <summary>
        /// Ex: 2020-09-25
        /// </summary>
        [Required]
        public string? StartDate { get; set; }
        /// <summary>
        /// Ex: 2020-09-26
        /// </summary>
        [Required]
        public string? EndDate { get; set; }
        /// <summary>
        /// 根據用戶名篩選
        /// </summary>
        public string? Username { get; set; }
        /// <summary>
        /// UNIX 時間戳
        /// </summary>
        [Required]
        public long Timestamp { get; set; } = Helper.GetCurrentTimestamp();
    }
}
