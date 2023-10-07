using eSya.UserCreation.DO;
using eSya.UserCreation.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.UserCreation.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserCreationController : ControllerBase
    {
        private readonly IUserCreationRepository _UserCreationRepository;
        public UserCreationController(IUserCreationRepository UserCreationRepository)
        {
            _UserCreationRepository = UserCreationRepository;
        }

        #region  User Group
        /// <summary>
        /// Get User Group.
        /// UI Reffered - Fill User Group Tree view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMenulistbyUserGroup(int UserGroup, int UserType, int UserRole)
        {
            var user_Master = await _UserCreationRepository.GetMenulistbyUserGroup(UserGroup, UserType, UserRole);
            return Ok(user_Master);
        }
        /// <summary>
        /// Get User Group Form Action .
        /// UI Reffered - Fill Grid for User Group Form Action Link
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFormActionLinkbyUserGroup(int UserGroup, int UserType, int UserRole, int MenuKey)
        {
            var business_Key = await _UserCreationRepository.GetFormActionLinkbyUserGroup(UserGroup, UserType, UserRole, MenuKey);
            return Ok(business_Key);
        }

        /// <summary>
        /// Insert into User Group Menu Link and  Form Action Link Table
        /// UI Reffered - User Group
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoUserGroupMenuAction(DO_UserGroupRole obj)
        {
            var msg = await _UserCreationRepository.InsertIntoUserGroupMenuAction(obj);
            return Ok(msg);
        }
        #endregion

        #region  User Group & Role
        /// <summary>
        /// Getting  User Types by UserGroup for dropdown.
        /// UI Reffered - User Creation
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUserTypesbyUserGroup(int usergroup)
        {
            var _types = await _UserCreationRepository.GetUserTypesbyUserGroup(usergroup);
            return Ok(_types);
        }
        /// <summary>
        /// Getting  User Roles by  User Group UserType for dropdown
        /// UI Reffered - User Creation
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUserRolesbyUserType(int usergroup, int usertype)
        {
            var _roles = await _UserCreationRepository.GetUserRolesbyUserType(usergroup, usertype);
            return Ok(_roles);
        }

        /// <summary>
        /// Getting  User Role Menu Link based  on User ID and Busienss Key.
        /// UI Reffered - User Creation
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUserRoleMenuLinkbyUserId(short UserID, int BusinessKey)
        {
            var _rolelinks = await _UserCreationRepository.GetUserRoleMenuLinkbyUserId(UserID, BusinessKey);
            return Ok(_rolelinks);
        }

        /// <summary>
        /// Insert into User Role Menu Link
        /// UI Reffered -User Creation
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoUserRoleMenuLink(DO_UserRoleMenuLink obj)
        {
            var msg = await _UserCreationRepository.InsertIntoUserRoleMenuLink(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Update User Role Menu Link
        /// UI Reffered - User Creation
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateUserRoleMenuLink(DO_UserRoleMenuLink obj)
        {
            var msg = await _UserCreationRepository.UpdateUserRoleMenuLink(obj);
            return Ok(msg);
        }
        #endregion

        #region User Creation
        /// <summary>
        /// Get User Master.
        /// UI Reffered - Fill Grid for User Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserMaster()
        {
            var user_Master = await _UserCreationRepository.GetUserMaster();
            return Ok(user_Master);
        }

        /// <summary>
        /// Get User Master.
        /// UI Reffered - Fill Grid for User Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserDetails(int UserID)
        {
            var user_Master = await _UserCreationRepository.GetUserDetails(UserID);
            return Ok(user_Master);
        }

        /// <summary>
        /// Get Business Location and Business Key.
        /// UI Reffered - Fill Grid for Business Location
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserBusinessLocation(short UserID, int CodeTypeUG, int CodeTypeUT)
        {
            var business_Key = await _UserCreationRepository.GetUserBusinessLocation(UserID, CodeTypeUG, CodeTypeUT);
            return Ok(business_Key);
        }

        /// <summary>
        /// Insert into User Master Table
        /// UI Reffered - User Creation,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoUserMaster(DO_UserMaster obj)
        {
            var msg = await _UserCreationRepository.InsertIntoUserMaster(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Update into User Master Table
        /// UI Reffered - User Creation,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateUserMaster(DO_UserMaster obj)
        {
            var msg = await _UserCreationRepository.UpdateUserMaster(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Insert into User Business Link Table
        /// UI Reffered - User Craetion,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoUserBL(DO_UserBusinessLink obj)
        {
            var msg = await _UserCreationRepository.InsertIntoUserBL(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Update into User Business Link Table
        /// UI Reffered - User Craetion,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateUserBL(DO_UserBusinessLink obj)
        {
            var msg = await _UserCreationRepository.UpdateUserBL(obj);
            return Ok(msg);
        }

        /// UI Reffered - Fill JS Tree
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMenulist(int UserGroup, int UserType, short UserID, int BusinessKey)
        {
            var menu_list = await _UserCreationRepository.GetMenulist(UserGroup, UserType, UserID, BusinessKey);
            return Ok(menu_list);
        }

        /// <summary>
        /// Get User Form Action .
        /// UI Reffered - Fill Grid for User Form Action Link
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserFormActionLink(short UserID, int BusinessKey, int MenuKey)
        {
            var business_Key = await _UserCreationRepository.GetUserFormActionLink(UserID, BusinessKey, MenuKey);
            return Ok(business_Key);
        }

        /// <summary>
        /// Insert into User Menu Link and User Form Action Link Table
        /// UI Reffered - User Craetion
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoUserMenuAction(DO_UserMenuLink obj)
        {
            var msg = await _UserCreationRepository.InsertIntoUserMenuAction(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Getting  Menu Key based on User ID and Busienss Key.
        /// UI Reffered - User Creation
        /// </summary>
        [HttpGet]
        public IActionResult GetMenuKeysforUser(short UserID, int BusinessKey)
        {
            var menukeys = _UserCreationRepository.GetMenuKeysforUser(UserID, BusinessKey);
            return Ok(menukeys);
        }

        /// <summary>
        /// Get User Master.
        /// UI Reffered - Fill Grid for User Master for Authentication
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserMasterForUserAuthentication()
        {
            var user_Master = await _UserCreationRepository.GetUserMasterForUserAuthentication();
            return Ok(user_Master);
        }

        /// <summary>
        /// Update User Master on Authentication Table
        /// UI Reffered - User Creation,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateUserMasteronAuthentication(DO_UserMaster obj)
        {
            var msg = await _UserCreationRepository.UpdateUserMasteronAuthentication(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Get User Master.
        /// UI Reffered - Fill Grid for User Master for Authentication
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserMasterForUserDeactivation()
        {
            var user_Master = await _UserCreationRepository.GetUserMasterForUserDeactivation();
            return Ok(user_Master);
        }

        /// <summary>
        /// Update User Master on Authentication Table
        /// UI Reffered - User Creation,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateUserForDeativation(DO_UserMaster obj)
        {
            var msg = await _UserCreationRepository.UpdateUserForDeativation(obj);
            return Ok(msg);
        }
        #endregion User Creation
    }
}
