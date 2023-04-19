namespace customer.api.service.Model
{
    public class ResCodeBase
    {
        /// <summary>
        /// api response code
        /// 0:success others:fail  
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// api response error message
        /// </summary>
        public string Message { get; set; }
        public ResCodeBase()
        {
            code = (int)ResponseCode.Success;
            Message = MessageCode.Message[(int)ResponseCode.Success];
        }
    }
}
