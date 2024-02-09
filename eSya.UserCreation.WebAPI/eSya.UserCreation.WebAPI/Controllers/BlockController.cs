using eSya.UserCreation.DO;
using eSya.UserCreation.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.UserCreation.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly IBlockRepository _blockRepository;
        public BlockController(IBlockRepository blockRepository)
        {
            _blockRepository = blockRepository;
        }
        /// <summary>
        /// Get All Blocked Users.
        /// UI Reffered - User Un Blocked
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBlockedUsers()
        {
            var b_users = await _blockRepository.GetBlockedUsers();
            return Ok(b_users);
        }
        /// <summary>
        /// Update User Mater to Un Block User
        /// UI Reffered - User Un Blocked
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateBlockSignIn(DO_BlockUser obj)
        {
            var msg = await _blockRepository.UpdateBlockSignIn(obj);
            return Ok(msg);
        }
    }
}
