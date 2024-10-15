using gcapi.DataBase;
using gcapi.DataBaseModels;
using gcapi.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OtpNet;
namespace gcapi.Realizations
{
    public class AuthService(gContext context) : IAuthService
    {

        private readonly gContext _context = context;

        public async Task AuthorizeUser()
        {
            throw new NotImplementedException();
        } 

        public async Task<bool> RegisterUser(UserModel user)
        {
            //ты типа сам не догадался сюда это вставить
            if (user == null || user.Username == null || user.FirstName == null)
            {
                return false;
            }
            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return false;
            }
    
        }
    }
}
