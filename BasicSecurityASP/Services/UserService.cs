using BasicSecurityASP.Models;
using System.Text.Json;

namespace BasicSecurityASP.Services
{
    public class UserService:IUserService
    {

        public bool IsUser(string email, string pass)
        {
            string path = @"C:\Users\omary\OneDrive\Escritorio\LearningDEV\BasicSecurityASP\BasicSecurityASP\users.json";
            string content = File.ReadAllText(path);
            var users = JsonSerializer.Deserialize<List<User>>(content);
            bool result=users.Where(user => user.Email == email && user.PassWord == pass).Count() > 0;
            return result;
        }
        

    }
}
