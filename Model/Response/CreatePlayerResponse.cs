namespace customer.api.service.Model.Response
{
    public class CreatePlayerResponse
    {
        /// <summary>
        /// 確定 – 用戶已經存在於提供者的系統中(HttpStatusCode = 200)
        /// 已創建 – 已在提供者的系統上成功創建用戶(HttpStatusCode = 201)
        /// </summary>
        public string? Status { get; set; }
        public CreatePlayerResponseData? Data { get; set; }

        public class CreatePlayerResponseData
        {
            public string? Username { get; set; }
            public string? Status { get; set; }
        }
    }
}
