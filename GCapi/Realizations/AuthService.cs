using gcapi.DataBase;
using gcapi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using gcapi.Models;
using gcapi.Dto;
namespace gcapi.Realizations
{
    public class AuthService(gContext context) : IAuthService
    {

        private readonly gContext _context = context;

        public async Task<IActionResult> CheckAuth(AuthDto authData)
        {
            var user = await _context.UserTable.FindAsync(authData.Username);
            if (user == null)
            {
                return new BadRequestObjectResult("Пользователь не найден"); //ну тебе проще самому тут все делать
                //ладно я забыл что ты просил меня инвайты сделать
            }
            return new OkResult();
         }

            public async Task<IActionResult> RegisterUser(RegisterDto user)
            {

                //ты типа сам не догадался сюда это вставить

                //ну как будто бы лучше отделить проверку от логики - куда-нибудь вообще в другое место вынести, а то грязно как-то
                //if (user == null || user.Username == null || user.FirstName == null)
                //{
                //    return new APIResponse(false, "Не все обязательные поля заполнены");
                //}
                //ну конкретно эта проверка не обязательна, конечно, модель сама проверяет свои поля

                var userModel = new UserModel
                {
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    Email = user.Email,
                    TgId = user.TgId,
                    Username = user.Username
                };
                try
                {
                    await _context.AddAsync(userModel);
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
