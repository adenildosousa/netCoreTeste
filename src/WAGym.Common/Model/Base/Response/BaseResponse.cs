using System.Net;

namespace WAGym.Common.Model.Base.Response
{
    public class BaseResponse
    {
        public HttpStatusCode Status { get; private set; }
        public string? Message { get; private set; }

        public BaseResponse(HttpStatusCode status, string? message = null)
        {
            Status = status;
            Message = message;
        }
    }
}
