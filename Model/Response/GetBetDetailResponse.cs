namespace customer.api.service.Model.Response
{
    public class GetBetDetailResponse
    {
        /// <summary>
        /// 密鑰是事務類型。例如 Game 或Jackpot 或 Competition。值是以下模型的數組。
        /// </summary>
        public GetBetDetailResponseData? data { get; set; }
        /// <summary>
        /// 如果返回的 NextId 不是空，則請求具有相同 StartDate 和 EndDate 的 NextId的 API。重複此操作直到 NextId 是空
        /// </summary>
        public string? nextId { get; set; }
        public List<Game1>? games { get; set; }

        public class GetBetDetailResponseData
        {
            public List<Game>? Game { get; set; }
            public List<Jackpot>? Jackpot { get; set; }
            public List<Competition>? Competition { get; set; }
        }

        public class Game
        {
            /// <summary>
            /// 事務的唯一標識符
            /// </summary>
            public string? OCode { get; set; }
            /// <summary>
            /// 與事務關聯的玩家
            /// </summary>
            public string? Username { get; set; }
            /// <summary>
            /// 事務的遊戲代碼
            /// </summary>
            public string? GameCode { get; set; }
            /// <summary>
            /// 敘述
            /// </summary>
            public string? Description { get; set; }
            public string? Type { get; set; }
            /// <summary>
            /// 下注金額
            /// </summary>
            public decimal Amount { get; set; }
            /// <summary>
            /// 下注結果
            /// 輸贏：結果 – 金額
            /// </summary>
            public decimal Result { get; set; }
            /// <summary>
            /// 事務時間
            /// </summary>
            public DateTime Time { get; set; }
            public string? RoundID { get; set; }
            public string? TransactionOCode { get; set; }
        }

        public class Jackpot
        {
            /// <summary>
            /// 事務的唯一標識符
            /// </summary>
            public string? OCode { get; set; }
            /// <summary>
            /// 與事務關聯的玩家
            /// </summary>
            public string? Username { get; set; }
            /// <summary>
            /// 事務的遊戲代碼
            /// </summary>
            public string? GameCode { get; set; }
            /// <summary>
            /// 敘述
            /// </summary>
            public string? Description { get; set; }
            public string? RoundID { get; set; }
            /// <summary>
            /// 下注金額
            /// </summary>
            public decimal Amount { get; set; }
            /// <summary>
            /// 下注結果
            /// 輸贏：結果 – 金額
            /// </summary>
            public decimal Result { get; set; }
            /// <summary>
            /// 事務時間
            /// </summary>
            public DateTime Time { get; set; }
            public string? Type { get; set; }
            public string? TransactionOCode { get; set; }
        }

        public class Competition
        {
            /// <summary>
            /// 事務的唯一標識符
            /// </summary>
            public string? OCode { get; set; }
            /// <summary>
            /// 與事務關聯的玩家
            /// </summary>
            public string? Username { get; set; }
            /// <summary>
            /// 事務的遊戲代碼
            /// </summary>
            public string? GameCode { get; set; }
            /// <summary>
            /// 敘述
            /// </summary>
            public string? Description { get; set; }
            public string? RoundID { get; set; }
            /// <summary>
            /// 下注金額
            /// </summary>
            public decimal Amount { get; set; }
            /// <summary>
            /// 下注結果
            /// 輸贏：結果 – 金額
            /// </summary>
            public decimal Result { get; set; }
            /// <summary>
            /// 事務時間
            /// </summary>
            public DateTime Time { get; set; }
            public string? Type { get; set; }
            public string? TransactionOCode { get; set; }
        }

        public class Game1
        {
            public string? GameCode { get; set; }
            public string? GameName { get; set; }
            public string? GameType { get; set; }
        }
    }
}
