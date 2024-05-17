using eSya.UserCreation.DO;
using eSya.UserCreation.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.UserCreation.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
       
        private readonly IAuthorizeRepository _authorizeRepository;
        public AuthorizeController(IAuthorizeRepository authorizeRepository)
        {
            _authorizeRepository = authorizeRepository;
        }

        #region Authenticate User
        /// <summary>
        /// Get All Un Authenticated Users List.
        /// UI Reffered - User Authenticated
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUnAuthenticatedUsers()
        {
            var b_users = await _authorizeRepository.GetUnAuthenticatedUsers();
            return Ok(b_users);
        }
        /// <summary>
        /// Update User Mater to Authenticated User
        /// UI Reffered - User Authenticated
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AuthenticateUser(DO_Authorize obj)
        {
            var msg = await _authorizeRepository.AuthenticateUser(obj);
            return Ok(msg);
        }
        #endregion
    }
}
