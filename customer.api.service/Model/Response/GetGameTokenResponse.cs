namespace customer.api.service.Model.Response
{
    public class GetGameTokenResponse
    {
        /// <summary>
        /// 用於構建玩遊戲 Url 的令牌（令牌只能使用一次）
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        /// 玩家用戶名
        /// </summary>
        public string? Username { get; set; }
    }
}
