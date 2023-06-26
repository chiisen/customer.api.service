namespace customer.api.service.Model
{
    public class ApiResponseData
    {
        public long reqDateTime { get; set; }
        public long ElapsedMilliseconds { get; set; }
        public ApiResponseData()
        {
            reqDateTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}
