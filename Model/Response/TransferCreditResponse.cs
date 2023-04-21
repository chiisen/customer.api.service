namespace customer.api.service.Model.Response
{

    public class TransferCreditResponse
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
        /// 玩家的當前信用餘額
        /// </summary>
        public decimal Credit { get; set; }
        /// <summary>
        /// 轉賬前的信用餘額
        /// </summary>
        public decimal BeforeCredit { get; set; }
        /// <summary>
        /// 玩家的剩餘信用
        /// </summary>
        public decimal OutstandingCredit { get; set; }
    }
}
