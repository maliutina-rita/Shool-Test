using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp
{
    using System.Net;

    [Route("api")]
    public class LoginController : Controller
    {
        private readonly IAccountDatabase _db;

        public LoginController(IAccountDatabase db)
        {
            _db = db;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> Login(string userName)
        {
            var account = await _db.FindByUserNameAsync(userName);
            if (account != null)
            {
                //TODO 1: Generate auth cookie for user 'userName' with external id

               return Ok();
            }

            return NotFound();
            //TODO 2: return 404 if user not found
        }
    }
}