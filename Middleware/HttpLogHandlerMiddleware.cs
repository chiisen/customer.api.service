using customer.api.service.Model;
using System.Diagnostics;

namespace customer.api.service.Middleware
{
    public class HttpLogHandlerMiddleware : DelegatingHandler
    {
        private readonly LogHelper<HttpLogHandlerMiddleware> _logHelper;
        public HttpLogHandlerMiddleware(LogHelper<HttpLogHandlerMiddleware> logHelper)
        {
            _logHelper = logHelper;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // 蒐集紀錄 Log 需要的資料
            var url = request.RequestUri?.ToString();
            var platform = FindGamePlatformName(url);
            var method = request.Method.ToString();
            var requestData = request.Content != null ? await request.Content.ReadAsStringAsync(cancellationToken) : string.Empty;
            var responseData = string.Empty;
            var httpStatusCode = 0;
            var sw = new Stopwatch();
            HttpResponseMessage response;

            try
            {
                sw.Start();
                response = await base.SendAsync(request, cancellationToken);
                sw.Stop();

                responseData = await response.Content.ReadAsStringAsync(cancellationToken);
                httpStatusCode = (int)response.StatusCode;

                _logHelper.APILog(platform,
                    url,
                    method,
                    CheckStringLength(requestData),
                    CheckStringLength(responseData),
                    httpStatusCode,
                    sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                _logHelper.APIErrorLog(ex,
                    platform,
                    url,
                    method,
                    CheckStringLength(requestData),
                    CheckStringLength(responseData),
                    httpStatusCode,
                    sw.ElapsedMilliseconds);

                throw;
            }

            return response;
        }

        /// <summary>
        /// 限制資料長度，避免 GCP Log 寫不進去
        /// </summary>
        /// <returns></returns>
        private static string CheckStringLength(string data)
        {
            data ??= string.Empty;

            const int maxlength = 3000;
            return data.Length > maxlength ? $"Log 過長已截斷，保留最大資料長度為: {maxlength}，{data.Substring(0, maxlength)}" : data;
        }

        /// <summary>
        /// 找出遊戲廠商名稱
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string FindGamePlatformName(string? url)
        {
            _ = url ?? string.Empty;

            return "undefined";
        }
    }
}
