using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp
{
    using System.Linq;

    // TODO 5: unauthorized users should receive 401 status code
    [Authorize]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IAccountCache _accountCache;

        public AccountController(IAccountService accountService, IAccountCache accountCache)
        {
            _accountService = accountService;
            _accountCache = accountCache;
        }

        [Authorize]
        [HttpGet] //TODO 3: Get user id from cookie
        public ValueTask<Account> Get()
        { 
            var userClaim =  HttpContext.User.Claims.FirstOrDefault(c => c.Type == "externalId");
            return _accountService.LoadOrCreateAsync(userClaim.Value);
        }

        //TODO 6: Endpoint should works only for users with "Admin" Role
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public Account GetByInternalId([FromRoute] int id)
        {
            return _accountService.GetFromCache(id);
        }

        [Authorize]
        [HttpPost("counter")]
        public async Task UpdateAccount()
        {
            //Update account in cache, don't bother saving to DB, this is not an objective of this task.
            var account = await Get();
            account.Counter++;
            _accountCache.AddOrUpdate(account);
        }
    }
}