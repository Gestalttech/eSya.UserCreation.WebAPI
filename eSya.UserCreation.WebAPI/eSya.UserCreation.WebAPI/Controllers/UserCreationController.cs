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

        //#region  User Group
        ///// <summary>
        ///// Get User Group.
        ///// UI Reffered - Fill User Group Tree view
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetMenulistbyUserGroup(int UserGroup, int UserType, int UserRole)
        //{
        //    var user_Master = await _UserCreationRepository.GetMenulistbyUserGroup(UserGroup, UserType, UserRole);
        //    return Ok(user_Master);
        //}
        ///// <summary>
        ///// Get User Group Form Action .
        ///// UI Reffered - Fill Grid for User Group Form Action Link
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetFormActionLinkbyUserGroup(int UserGroup, int UserType, int UserRole, int MenuKey)
        //{
        //    var business_Key = await _UserCreationRepository.GetFormActionLinkbyUserGroup(UserGroup, UserType, UserRole, MenuKey);
        //    return Ok(business_Key);
        //}

        ///// <summary>
        ///// Insert into User Group Menu Link and  Form Action Link Table
        ///// UI Reffered - User Group
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> InsertIntoUserGroupMenuAction(DO_UserGroupRole obj)
        //{
        //    var msg = await _UserCreationRepository.InsertIntoUserGroupMenuAction(obj);
        //    return Ok(msg);
        //}
        //#endregion

        //#region  User Group & Role
        ///// <summary>
        ///// Getting  User Types by UserGroup for dropdown.
        ///// UI Reffered - User Creation
        ///// </summary>
        //[HttpGet]
        //public async Task<IActionResult> GetUserTypesbyUserGroup(int usergroup)
        //{
        //    var _types = await _UserCreationRepository.GetUserTypesbyUserGroup(usergroup);
        //    return Ok(_types);
        //}
        ///// <summary>
        ///// Getting  User Roles by  User Group UserType for dropdown
        ///// UI Reffered - User Creation
        ///// </summary>
        //[HttpGet]
        //public async Task<IActionResult> GetUserRolesbyUserType(int usergroup, int usertype)
        //{
        //    var _roles = await _UserCreationRepository.GetUserRolesbyUserType(usergroup, usertype);
        //    return Ok(_roles);
        //}

        ///// <summary>
        ///// Getting  User Role Menu Link based  on User ID and Busienss Key.
        ///// UI Reffered - User Creation
        ///// </summary>
        //[HttpGet]
        //public async Task<IActionResult> GetUserRoleMenuLinkbyUserId(short UserID, int BusinessKey)
        //{
        //    var _rolelinks = await _UserCreationRepository.GetUserRoleMenuLinkbyUserId(UserID, BusinessKey);
        //    return Ok(_rolelinks);
        //}

        ///// <summary>
        ///// Insert into User Role Menu Link
        ///// UI Reffered -User Creation
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> InsertIntoUserRoleMenuLink(DO_UserRoleMenuLink obj)
        //{
        //    var msg = await _UserCreationRepository.InsertIntoUserRoleMenuLink(obj);
        //    return Ok(msg);
        //}

        ///// <summary>
        ///// Update User Role Menu Link
        ///// UI Reffered - User Creation
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> UpdateUserRoleMenuLink(DO_UserRoleMenuLink obj)
        //{
        //    var msg = await _UserCreationRepository.UpdateUserRoleMenuLink(obj);
        //    return Ok(msg);
        //}
        //#endregion

        //#region User Creation
        ///// <summary>
        ///// Get User Master.
        ///// UI Reffered - Fill Grid for User Master
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetUserMaster()
        //{
        //    var user_Master = await _UserCreationRepository.GetUserMaster();
        //    return Ok(user_Master);
        //}

        ///// <summary>
        ///// Get User Master.
        ///// UI Reffered - Fill Grid for User Master
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetUserDetails(int UserID)
        //{
        //    var user_Master = await _UserCreationRepository.GetUserDetails(UserID);
        //    return Ok(user_Master);
        //}

        ///// <summary>
        ///// Get Business Location and Business Key.
        ///// UI Reffered - Fill Grid for Business Location
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetUserBusinessLocation(short UserID, int CodeTypeUG, int CodeTypeUT)
        //{
        //    var business_Key = await _UserCreationRepository.GetUserBusinessLocation(UserID, CodeTypeUG, CodeTypeUT);
        //    return Ok(business_Key);
        //}

        ///// <summary>
        ///// Insert into User Master Table
        ///// UI Reffered - User Creation,
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> InsertIntoUserMaster(DO_UserMaster obj)
        //{
        //    var msg = await _UserCreationRepository.InsertIntoUserMaster(obj);
        //    return Ok(msg);
        //}

        ///// <summary>
        ///// Update into User Master Table
        ///// UI Reffered - User Creation,
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> UpdateUserMaster(DO_UserMaster obj)
        //{
        //    var msg = await _UserCreationRepository.UpdateUserMaster(obj);
        //    return Ok(msg);
        //}

        ///// <summary>
        ///// Insert into User Business Link Table
        ///// UI Reffered - User Craetion,
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> InsertIntoUserBL(DO_UserBusinessLink obj)
        //{
        //    var msg = await _UserCreationRepository.InsertIntoUserBL(obj);
        //    return Ok(msg);
        //}

        ///// <summary>
        ///// Update into User Business Link Table
        ///// UI Reffered - User Craetion,
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> UpdateUserBL(DO_UserBusinessLink obj)
        //{
        //    var msg = await _UserCreationRepository.UpdateUserBL(obj);
        //    return Ok(msg);
        //}

        ///// UI Reffered - Fill JS Tree
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetMenulist(int UserGroup, int UserType, short UserID, int BusinessKey)
        //{
        //    var menu_list = await _UserCreationRepository.GetMenulist(UserGroup, UserType, UserID, BusinessKey);
        //    return Ok(menu_list);
        //}

        ///// <summary>
        ///// Get User Form Action .
        ///// UI Reffered - Fill Grid for User Form Action Link
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetUserFormActionLink(short UserID, int BusinessKey, int MenuKey)
        //{
        //    var business_Key = await _UserCreationRepository.GetUserFormActionLink(UserID, BusinessKey, MenuKey);
        //    return Ok(business_Key);
        //}

        ///// <summary>
        ///// Insert into User Menu Link and User Form Action Link Table
        ///// UI Reffered - User Craetion
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> InsertIntoUserMenuAction(DO_UserMenuLink obj)
        //{
        //    var msg = await _UserCreationRepository.InsertIntoUserMenuAction(obj);
        //    return Ok(msg);
        //}

        ///// <summary>
        ///// Getting  Menu Key based on User ID and Busienss Key.
        ///// UI Reffered - User Creation
        ///// </summary>
        //[HttpGet]
        //public IActionResult GetMenuKeysforUser(short UserID, int BusinessKey)
        //{
        //    var menukeys = _UserCreationRepository.GetMenuKeysforUser(UserID, BusinessKey);
        //    return Ok(menukeys);
        //}

        ///// <summary>
        ///// Get User Master.
        ///// UI Reffered - Fill Grid for User Master for Authentication
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetUserMasterForUserAuthentication()
        //{
        //    var user_Master = await _UserCreationRepository.GetUserMasterForUserAuthentication();
        //    return Ok(user_Master);
        //}

        ///// <summary>
        ///// Update User Master on Authentication Table
        ///// UI Reffered - User Creation,
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> UpdateUserMasteronAuthentication(DO_UserMaster obj)
        //{
        //    var msg = await _UserCreationRepository.UpdateUserMasteronAuthentication(obj);
        //    return Ok(msg);
        //}

        ///// <summary>
        ///// Get User Master.
        ///// UI Reffered - Fill Grid for User Master for Authentication
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetUserMasterForUserDeactivation()
        //{
        //    var user_Master = await _UserCreationRepository.GetUserMasterForUserDeactivation();
        //    return Ok(user_Master);
        //}

        ///// <summary>
        ///// Update User Master on Authentication Table
        ///// UI Reffered - User Creation,
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> UpdateUserForDeativation(DO_UserMaster obj)
        //{
        //    var msg = await _UserCreationRepository.UpdateUserForDeativation(obj);
        //    return Ok(msg);
        //}
        //#endregion User Creation



        #region User Creation New Process

        #region User Group

        /// <summary>
        /// Get User Group.
        /// UI Reffered - Fill User Roles
        /// </summary>
        /// <returns></returns>     
        [HttpGet]
        public async Task<IActionResult> GetActiveUserRolesByCodeType(int codeType)
        {
            var userroles = await _UserCreationRepository.GetActiveUserRolesByCodeType(codeType);
            return Ok(userroles);
        }
        /// <summary>
        /// Get User Group.
        /// UI Reffered - Fill User Group Tree view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserRoleMenulist(int UserGroup, short UserRole, int BusinessKey)
        {
            var userroles = await _UserCreationRepository.GetUserRoleMenulist(UserGroup, UserRole, BusinessKey);
            return Ok(userroles);
        }
       
        /// <summary>
        /// Insert into User Group Menu Link  Table
        /// UI Reffered - User Group
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateUserRoleMenuLink(List<DO_UserGroupRole> obj)
        {
            var msg = await _UserCreationRepository.InsertOrUpdateUserRoleMenuLink(obj);
            return Ok(msg);
        }
        #endregion

        #region Link Action to User Role  
        /// <summary>
        /// Getting  User Role for drop down.
        /// UI Reffered - Define User Role Action Grid
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUserRoleByCodeType(int codeType)
        {
            var user_role = await _UserCreationRepository.GetUserRoleByCodeType(codeType);
            return Ok(user_role);

        }
        /// <summary>
        /// Getting  User Role Action List.
        /// UI Reffered - Define User Role Action Grid
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUserRoleActionLink(int userRole)
        {
            var role_actions = await _UserCreationRepository.GetUserRoleActionLink(userRole);
            return Ok(role_actions);

        }
        /// <summary>
        /// Insert Or Update  User Role Action Grid .
        /// UI Reffered -Define User Role Action
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateUserRoleActionLink(List<DO_UserRoleActionLink> obj)
        {
            var msg = await _UserCreationRepository.InsertOrUpdateUserRoleActionLink(obj);
            return Ok(msg);
        }
        #endregion

        #region User Creation
        #region Tab-1
        /// <summary>
        /// Get User Master.
        /// UI Reffered - User Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserMaster()
        {
            var usermaster = await _UserCreationRepository.GetUserMaster();
            return Ok(usermaster);
        }
        /// <summary>
        /// Get User by User ID.
        /// UI Reffered - User Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserDetails(int UserID)
        {
            var user = await _UserCreationRepository.GetUserDetails(UserID);
            return Ok(user);
        }
        /// <summary>
        /// Get User Parameters by User ID.
        /// UI Reffered - User Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserParameters(int UserID)
        {
            var uparams = await _UserCreationRepository.GetUserParameters(UserID);
            return Ok(uparams);
        }
        /// <summary>
        /// Insert Into User Master .
        /// UI Reffered -User Master
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoUserMaster(DO_UserMaster obj)
        {
            var msg = await _UserCreationRepository.InsertIntoUserMaster(obj);
            return Ok(msg);
        }
        /// <summary>
        /// Update  User Master .
        /// UI Reffered -User Master
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateUserMaster(DO_UserMaster obj)
        {
            var msg = await _UserCreationRepository.UpdateUserMaster(obj);
            return Ok(msg);
        }
        #endregion

        #region Tab-2
        /// <summary>
        /// Get State Code by Business Key.
        /// UI Reffered - User Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetStateCodebyBusinessKey(int BusinessKey)
        {
            var states =  _UserCreationRepository.GetStateCodebyBusinessKey(BusinessKey);
            return Ok(states);
        }
        /// <summary>
        /// Get Preferred Language by Business Key for drop down.
        /// UI Reffered - User Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPreferredLanguagebyBusinessKey(int BusinessKey)
        {
            var langs = await _UserCreationRepository.GetPreferredLanguagebyBusinessKey(BusinessKey);
            return Ok(langs);
        }
        /// <summary>
        /// Get User Business Location By User ID for Grid.
        /// UI Reffered - User Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserBusinessLocationByUserID(int UserID)
        {
            var loc = await _UserCreationRepository.GetUserBusinessLocationByUserID(UserID);
            return Ok(loc);
        }
        /// <summary>
        /// Insert Into Or Update User Business Location .
        /// UI Reffered -User Master
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateUserBusinessLocation(DO_UserBusinessLocation obj)
        {
            var msg = await _UserCreationRepository.InsertOrUpdateUserBusinessLocation(obj);
            return Ok(msg);
        }

        #endregion

        #endregion

        #region Map User to User Group
        /// <summary>
        /// Get Active Users for dropdown.
        /// UI Reffered - Map User to User Group
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetActiveUsers()
        {
            var users = await _UserCreationRepository.GetActiveUsers();
            return Ok(users);
        }
        /// <summary>
        /// Get Mapped User Group By User ID.
        /// UI Reffered - Map User to User Group for Grid
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMappedUserGroupByUserID(int UserID)
        {
            var mapedgroups = _UserCreationRepository.GetMappedUserGroupByUserID(UserID);
            return Ok(mapedgroups);
        }

        /// <summary>
        /// Get User Group.
        /// UI Reffered - Map User to User Group Tree view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMappedUserRoleMenulist(int UserGroup, short UserRole, int BusinessKey)
        {
            var menulist = await _UserCreationRepository.GetMappedUserRoleMenulist(UserGroup, UserRole, BusinessKey);
            return Ok(menulist);
        }

        /// <summary>
        /// Insert into User Group Mapped with User Table
        /// UI Reffered - Map User to User Group
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateUserGroupMappedwithUser(DO_MapUsertoUserGroup obj)
        {
            var msg = await _UserCreationRepository.InsertOrUpdateUserGroupMappedwithUser(obj);
            return Ok(msg);
        }
        #endregion

        #region User Photo
        /// <summary>
        /// Get User Master.
        /// UI Reffered - Upload User Photo 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetActiveUsersforPhoto()
        {
            var usermaster = await _UserCreationRepository.GetActiveUsersforPhoto();
            return Ok(usermaster);
        }

        /// <summary>
        /// Get User by User ID.
        /// UI Reffered - Upload User Photo 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserPhotobyUserID(int UserID)
        {
            var user = await _UserCreationRepository.GetUserPhotobyUserID(UserID);
            return Ok(user);
        }
        /// <summary>
        /// Update  User Master .
        /// UI Reffered -Upload User Photo 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UploadUserPhoto(DO_UserPhoto obj)
        {
            var msg = await _UserCreationRepository.UploadUserPhoto(obj);
            return Ok(msg);
        }
        #endregion

        #endregion

        #region Change Password
        /// <summary>
        /// Update  Change Password .
        /// UI Reffered -Change Password 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ChangeUserPassword(DO_ChangePassword obj)
        {
            var msg = await _UserCreationRepository.ChangeUserPassword(obj);
            return Ok(msg);
        }
        #endregion
    }
}
