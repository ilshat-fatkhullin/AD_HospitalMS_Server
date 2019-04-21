namespace HospitalMSServer.Models.Authentication
{
    public class ChangePasswordRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public bool IsInvalid()
        {
            return string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(NewPassword);
        }
    }
}
