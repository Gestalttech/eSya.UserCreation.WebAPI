using eSya.UserCreation.DO;
using eSya.UserCreation.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.UserCreation.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeActivateController : ControllerBase
    {
        private readonly IDeActivateRepository _deActivateRepository;
        public DeActivateController(IDeActivateRepository deActivateRepository)
        {
            _deActivateRepository = deActivateRepository;
        }
        #region Authenticate User
        /// <summary>
        /// Get All Activated Users List.
        /// UI Reffered - User De-Activation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetActivatedUsers()
        {
            var b_users = await _deActivateRepository.GetActivatedUsers();
            return Ok(b_users);
        }
        /// <summary>
        /// Update User Mater to De Activate User
        /// UI Reffered - User  De-Activation
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> DeActivateUser(DO_DeActivated obj)
        {
            var msg = await _deActivateRepository.DeActivateUser(obj);
            return Ok(msg);
        }
        #endregion
    }
}
