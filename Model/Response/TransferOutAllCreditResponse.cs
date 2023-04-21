namespace customer.api.service.Model.Response
{
    public class TransferOutAllCreditResponse
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
        /// 玩家當前的信用餘額轉移至提供者的系統中
        /// 如金額為零，這表示玩家未在提供者的系統上存入信用餘額
        /// 系統不支持交易金額為零的 "驗證轉移信用額"
        /// </summary>
        public decimal Amount { get; set; }
    }
}
