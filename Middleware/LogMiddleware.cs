using customer.api.service.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace customer.api.service.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LogHelper<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, LogHelper<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsIgnoreLogPath(context.Request.Path.ToString()))
            {
                await _next(context);
                return;
            }

            var sw = new Stopwatch();
            sw.Start();

            var originalBodyStream = context.Response.Body;
            await using var fakeResponseBody = new MemoryStream();

            try
            {
                // 啟用讀取 Request
                context.Request.EnableBuffering();
                // Request Body
                var requestContent = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;
                // 設定 Stream 存放 ResponseBody
                context.Response.Body = fakeResponseBody;

                _logger.HttpLog(context, "Request", CheckStringLength(requestContent), null);

                // 執行 Middleware
                await _next(context);

                // 讀取 Response
                fakeResponseBody.Seek(0, SeekOrigin.Begin);
                context.Request.EnableBuffering();
                var responseContent = await new StreamReader(fakeResponseBody).ReadToEndAsync();
                fakeResponseBody.Seek(0, SeekOrigin.Begin);
                await fakeResponseBody.CopyToAsync(originalBodyStream);

                sw.Stop();
                _logger.HttpLog(context, "Response", CheckStringLength(responseContent), sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var responseContent = JsonConvert.SerializeObject(new ResCodeBase()
                {
                    code = (int)ResponseCode.Fail,
                    Message = MessageCode.Message[(int)ResponseCode.Fail]
                }, serializerSettings);

                // Request Body
                context.Request.Body.Position = 0;
                var requestContent = await new StreamReader(context.Request.Body).ReadToEndAsync();

                sw.Stop();
                _logger.HttpLog(context, "Response", CheckStringLength(responseContent), sw.ElapsedMilliseconds);
                _logger.HttpErrorLog(context, requestContent, ex.Message, ex, sw.ElapsedMilliseconds);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                await context.Response.WriteAsync(responseContent, Encoding.UTF8);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                await fakeResponseBody.CopyToAsync(originalBodyStream);
            }
        }

        /// <summary>
        /// 要忽略的 API 路徑
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static bool IsIgnoreLogPath(string url)
        {
            var ignoreList = new List<string>()
            {
                "/swagger",
                "/health",
                "/hc-ui",
                "/hc",
            };

            return ignoreList.Any(x => url.IndexOf(x, StringComparison.Ordinal) != -1);
        }

        /// <summary>
        /// 限制資料長度，避免 GCP Log 寫不進去
        /// </summary>
        /// <returns></returns>
        private static string CheckStringLength(string data)
        {
            data ??= string.Empty;
            var maxlength = 3000;
            if (data.Length > maxlength)
            {
                return $"Log 過長已截斷，保留最大資料長度為: {maxlength}，{data.Substring(0, maxlength)}";
            }

            return data;
        }
    }
}
