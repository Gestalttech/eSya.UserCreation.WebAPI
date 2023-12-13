using eSya.UserCreation.DL.Repository;
using eSya.UserCreation.DO;
using eSya.UserCreation.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.UserCreation.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class eSignatureController : ControllerBase
    {
        private readonly IeSignatureRepository _eSignatureRepository;
        public eSignatureController(IeSignatureRepository eSignatureRepository)
        {
            _eSignatureRepository = eSignatureRepository;
        }
        #region Upload User eSignature

        /// <summary>
        /// Get User All Active Users.
        /// UI Reffered - User eSignature
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetActiveUsersforSignature()
        {
            var users = await _eSignatureRepository.GetActiveUsersforSignature();
            return Ok(users);
        }
        /// <summary>
        /// Get User eSignature.
        /// UI Reffered - User eSignature
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsereSignaturebyUserID(int UserID)
        {
            var userroles = await _eSignatureRepository.GetUsereSignaturebyUserID(UserID);
            return Ok(userroles);
        }

        /// <summary>
        /// Insert into User eSignature
        /// UI Reffered - User eSignature
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateUsereSignature(DO_eSignature obj)
        {
            var msg = await _eSignatureRepository.InsertOrUpdateUsereSignature(obj);
            return Ok(msg);
        }
        #endregion
    }
}
