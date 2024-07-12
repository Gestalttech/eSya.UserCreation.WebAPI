using eSya.UserCreation.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.IF
{
    public interface IUserCreationRepository
    {


        //#region  User Group
        //Task<DO_ConfigureMenu> GetMenulistbyUserGroup(int UserGroup, int UserType, int UserRole);

        //Task<List<DO_UserFormAction>> GetFormActionLinkbyUserGroup(int UserGroup, int UserType, int UserRole, int MenuKey);

        //Task<DO_ReturnParameter> InsertIntoUserGroupMenuAction(DO_UserGroupRole obj);
        //#endregion

        //#region  User Group & Role
        //Task<List<DO_UserRoleMenuLink>> GetUserRoleMenuLinkbyUserId(short UserID, int BusinessKey);

        //Task<DO_ReturnParameter> InsertIntoUserRoleMenuLink(DO_UserRoleMenuLink obj);

        //Task<DO_ReturnParameter> UpdateUserRoleMenuLink(DO_UserRoleMenuLink obj);

        //Task<List<DO_UserType>> GetUserTypesbyUserGroup(int usergroup);

        //Task<List<DO_UserRole>> GetUserRolesbyUserType(int usergroup, int usertype);

        //#endregion

        //#region User Creation
        //Task<List<DO_UserMaster>> GetUserMaster();

        //Task<DO_UserMaster> GetUserDetails(int UserID);

        //Task<List<DO_UserBusinessLink>> GetUserBusinessLocation(short UserID, int CodeTypeUG, int CodeTypeUT);

        //Task<DO_ReturnParameter> InsertIntoUserMaster(DO_UserMaster obj);

        //Task<DO_ReturnParameter> UpdateUserMaster(DO_UserMaster obj);

        //Task<DO_ReturnParameter> InsertIntoUserBL(DO_UserBusinessLink obj);

        //Task<DO_ReturnParameter> UpdateUserBL(DO_UserBusinessLink obj);

        //Task<List<DO_UserFormAction>> GetUserFormActionLink(short UserID, int BusinessKey, int MenuKey);

        //Task<DO_ConfigureMenu> GetMenulist(int UserGroup, int UserType, short UserID, int BusinessKey);

        //Task<DO_ReturnParameter> InsertIntoUserMenuAction(DO_UserMenuLink obj);

        //List<int> GetMenuKeysforUser(short UserID, int BusinessKey);

        //Task<List<DO_UserMaster>> GetUserMasterForUserAuthentication();

        //Task<DO_ReturnParameter> UpdateUserMasteronAuthentication(DO_UserMaster obj);

        //Task<List<DO_UserMaster>> GetUserMasterForUserDeactivation();

        //Task<DO_ReturnParameter> UpdateUserForDeativation(DO_UserMaster obj);

        //#endregion User Creation


        #region User Creation New Process

        #region User Group
        Task<List<DO_ApplicationCodes>> GetActiveUserRolesByCodeType(int codeType);
        Task<DO_ConfigureMenu> GetUserRoleMenulist(int UserGroup, short UserRole, int BusinessKey);
        Task<DO_ReturnParameter> InsertOrUpdateUserRoleMenuLink(List<DO_UserGroupRole> obj);
        #endregion

        #region Link Action to User Role 
        Task<List<DO_ApplicationCodes>> GetUserRoleByCodeType(int codeType);
        Task<List<DO_UserRoleActionLink>> GetUserRoleActionLink(int userRole);
        Task<DO_ReturnParameter> InsertOrUpdateUserRoleActionLink(List<DO_UserRoleActionLink> obj);
        #endregion

        #region User Creation
        #region Tab-1
        Task<List<DO_UserMaster>> GetUserMaster();
        Task<DO_UserMaster> GetUserDetails(int UserID);
        Task<List<DO_eSyaParameter>> GetUserParameters(int UserID);
        Task<DO_ReturnParameter> InsertIntoUserMaster(DO_UserMaster obj);
        Task<DO_ReturnParameter> UpdateUserMaster(DO_UserMaster obj);
        #endregion

        #region Tab-2
        int GetStateCodebyBusinessKey(int BusinessKey);
        Task<List<DO_PreferredCulture>> GetPreferredLanguagebyBusinessKey(int BusinessKey);
        Task<List<DO_UserBusinessLocation>> GetUserBusinessLocationByUserID(int UserID);
        Task<DO_ReturnParameter> InsertOrUpdateUserBusinessLocation(DO_UserBusinessLocation obj);
        #endregion


        #endregion

        #region Map User to User Group
        Task<List<DO_UserMaster>> GetActiveUsers();
        List<DO_MapUsertoUserGroup> GetMappedUserGroupByUserID(int UserID);
        Task<DO_ReturnParameter> InsertOrUpdateUserGroupMappedwithUser(DO_MapUsertoUserGroup obj);
        Task<DO_ConfigureMenu> GetMappedUserRoleMenulist(int UserGroup, short UserRole, int BusinessKey);
        #endregion

        #region User Photo
        Task<List<DO_UserMaster>> GetActiveUsersforPhoto();
        Task<DO_UserPhoto> GetUserPhotobyUserID(int UserID);
        Task<DO_ReturnParameter> UploadUserPhoto(DO_UserPhoto obj);
        #endregion

        #endregion

        #region Change Password
        Task<DO_ReturnParameter> ChangeUserPassword(DO_ChangePassword obj);
        #endregion
    }
}
