using gcapi.DataBase;
using gcapi.DataBaseModels;
using gcapi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> RegisterUser(UserModel user)
        {

            //ты типа сам не догадался сюда это вставить

            //ну как будто бы лучше отделить проверку от логики - куда-нибудь вообще в другое место вынести, а то грязно как-то
            //if (user == null || user.Username == null || user.FirstName == null)
            //{
            //    return new APIResponse(false, "Не все обязательные поля заполнены");
            //}
            //ну конкретно эта проверка не обязательна, конечно, модель сама проверяет свои поля

            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new BadRequestObjectResult(ex.ToString());//это кринж немного, потом бы переделать, но пока для отладки просто
            }

        }
    }
}
