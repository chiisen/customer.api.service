﻿namespace customer.api.service.Model.Response
{
    public class KickPlayerResponse
    {
        /// <summary>
        /// OK - 固定值
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// 成功會回覆 Sign out successful.
        /// </summary>
        public string? Message { get; set; }
    }
}
