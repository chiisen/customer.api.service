namespace customer.api.service
{
    public class MessageCode
    {
        public static Dictionary<int, string> Message = new()
        {
            {(int)ResponseCode.Success, "Success"},
            {(int)ResponseCode.Fail, "Fail"},
        };
    }

    public enum ResponseCode
    {
        Success = 0,
        Fail = 9999
    }
}
