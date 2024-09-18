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
        public async Task<IActionResult> GetUnAuthenticatedUsers(string authenticate)
        {
            var b_users = await _authorizeRepository.GetUnAuthenticatedUsers(authenticate);
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
        /// <summary>
        /// Update User Mater to Reject User
        /// UI Reffered - User Authenticated
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RejectUser(DO_Authorize obj)
        {
            var msg = await _authorizeRepository.RejectUser(obj);
            return Ok(msg);
        }
        /// <summary>
        /// Get All Un Authenticated Users List.
        /// UI Reffered - User Authenticated
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserLinkedFormMenulist(int UserID, int UserGroup, int UserRole)
        {
            var b_users = await _authorizeRepository.GetUserLinkedFormMenulist(UserID, UserGroup, UserRole);
            return Ok(b_users);
        }
        /// <summary>
        /// Get All Un Authenticated Users List.
        /// UI Reffered - User Authenticated
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetActionListByUserRole(int userID, int UserGroup, int UserRole, int formID)
        {
            var b_users = await _authorizeRepository.GetActionListByUserRole(userID, UserGroup, UserRole, formID);
            return Ok(b_users);
        }
        #endregion
    }
}
