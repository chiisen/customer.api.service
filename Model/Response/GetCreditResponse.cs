namespace customer.api.service.Model.Response
{
    public class GetCreditResponse
    {
        /// <summary>
        /// 玩家用戶名
        /// </summary>
        public string? Username { get; set; }
        /// <summary>
        /// 玩家的當前信用餘額
        /// </summary>
        public decimal Credit { get; set; }
        /// <summary>
        /// 玩家的剩餘信用
        /// </summary>
        public decimal OutstandingCredit { get; set; }
        public decimal FreeCredit { get; set; }
        public decimal OutstandingFreeCredit { get; set; }
    }
}
