namespace SmartCarePatientPortal.Services
{
    public class LoginService
    {
        public bool Authenticate(string email, string password)
        {
            // For demo purposes: hardcoded valid user
            return email == "admin@example.com" && password == "Password123";
        }
    }
}
