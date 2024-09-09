using eSya.UserCreation.DL.Repository;
using eSya.UserCreation.DO;
using eSya.UserCreation.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.UserCreation.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        private readonly IApprovalRepository _approvalRepository;
        public ApprovalController(IApprovalRepository approvalRepository)
        {
            _approvalRepository = approvalRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetApproverUserListbyBusinessKey(int BusinessKey)
        {
            var ds = await _approvalRepository.GetApproverUserListbyBusinessKey(BusinessKey);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetApprovalRequiredFormMenulist(int BusinessKey, int UserID)
        {
            var ds = await _approvalRepository.GetApprovalRequiredFormMenulist(BusinessKey, UserID);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetApprovalLevelsbyFormID(int businesskey, int formId)
        {
            var ds = await _approvalRepository.GetApprovalLevelsbyFormID(businesskey, formId);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateUserApprovalForm(List<DO_UserApprovalForm> obj)
        {
            var ds = await _approvalRepository.InsertOrUpdateUserApprovalForm(obj);
            return Ok(ds);
        }
        
    }
}
