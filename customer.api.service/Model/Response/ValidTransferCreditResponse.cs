namespace customer.api.service.Model.Response
{
    public class ValidTransferCreditResponse
    {
        /// <summary>
        /// 唯一的密鑰，用於驗證轉賬到提供者系統的金額
        /// </summary>
        public string? RequestID { get; set; }
        /// <summary>
        /// 玩家用戶名
        /// </summary>
        public string? Username { get; set; }
        /// <summary>
        /// 轉賬時間
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 轉賬金額 正/負數
        /// </summary>
        public decimal Amount { get; set; }
    }
}
