using eSya.UserCreation.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.UserCreation.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonDataController : ControllerBase
    {
        private readonly ICommonDataRepository _commonDataRepository;
        public CommonDataController(ICommonDataRepository commonDataRepository)
        {
            _commonDataRepository = commonDataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicationCodesByCodeType(int codeType)
        {
            var ds = await _commonDataRepository.GetApplicationCodesByCodeType(codeType);
            return Ok(ds);
        }

        [HttpPost]
        public async Task<IActionResult> GetApplicationCodesByCodeTypeList(List<int> l_codeType)
        {
            var ds = await _commonDataRepository.GetApplicationCodesByCodeTypeList(l_codeType);
            return Ok(ds);
        }

        [HttpGet]
        public async Task<IActionResult> GetBusinessKey()
        {
            var ds = await _commonDataRepository.GetBusinessKey();
            return Ok(ds);
        }

        [HttpGet]
        public async Task<IActionResult> GetConfigureMenulist()
        {
            var config_Menu = await _commonDataRepository.GetConfigureMenulist();
            return Ok(config_Menu);
        }


        [HttpGet]
        public async Task<IActionResult> GetISDCodes()
        {
            var ds = await _commonDataRepository.GetISDCodes();
            return Ok(ds);
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorsforCombo()
        {
            var ds = await _commonDataRepository.GetDoctorsforCombo();
            return Ok(ds);
        }
    }
}
