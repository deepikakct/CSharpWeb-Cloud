using Microsoft.AspNetCore.Mvc;

namespace UWRESTProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // This should require SSL
        [HttpPost]
        public dynamic LoginPost([FromBody] TokenRequest tokenRequest)
        {
            var token = TokenHelper.GetToken(tokenRequest.UserEmail, tokenRequest.Password);
            return new { Token = token };
        }

        // This should require SSL
        [HttpGet]
        [Route("{userEmail}/{password}")]
        public dynamic LoginGet(string userEmail, string password)
        {
            var token = TokenHelper.GetToken(userEmail, password);
            return new { Token = token };
        }
    }
}
