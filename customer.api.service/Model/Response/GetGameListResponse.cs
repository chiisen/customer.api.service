namespace customer.api.service.Model.Response;

public class GetGameListResponse
{
    public List<Listgame>? ListGames { get; set; }

    public class Listgame
    {
        /// <summary>
        /// 遊戲類型：Slot、Fishing、E-Casino、Single Player、Multiplayer v.v.
        /// </summary>
        public string? GameType { get; set; }
        public string? GameTypeName { get; set; }
        public string? Code { get; set; }
        public string? GameOCode { get; set; }
        /// <summary>
        /// 遊戲別名 – 用於玩遊戲
        /// </summary>
        public string? GameCode { get; set; }
        /// <summary>
        /// 遊戲名稱
        /// </summary>
        public string? GameName { get; set; }
        /// <summary>
        /// 包含關鍵的熱門遊戲或新遊戲
        /// </summary>
        public string? Specials { get; set; }
        public string? Technology { get; set; }
        public string? SupportedPlatForms { get; set; }
        /// <summary>
        /// 遊戲次序
        /// </summary>
        public int Order { get; set; }
        public int DefaultWidth { get; set; }
        public int DefaultHeight { get; set; }
        /// <summary>
        /// 橫向模式下的遊戲圖像
        /// </summary>
        public string? Image1 { get; set; }
        /// <summary>
        /// 縱向模式下的遊戲圖像
        /// </summary>
        public string? Image2 { get; set; }
        public bool FreeSpin { get; set; }
        public List<Localization>? Localizations { get; set; }
    }

    public class Localization
    {
        public string? Language { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}