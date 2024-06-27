namespace WAGym.Common.Model.Login.Response
{
    public class LoginProfileResponse
    {
        public bool HasAccess { get; private set; }
        public string? Message { get; private set; }

        public LoginProfileResponse(bool hasAccess, string? message = null)
        {
            HasAccess = hasAccess;
            Message = message;
        }

        public void SetMessage(string message)  => Message = message;
    }
}
