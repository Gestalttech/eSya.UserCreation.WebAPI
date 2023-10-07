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
        

        #region  User Group
        Task<DO_ConfigureMenu> GetMenulistbyUserGroup(int UserGroup, int UserType, int UserRole);

        Task<List<DO_UserFormAction>> GetFormActionLinkbyUserGroup(int UserGroup, int UserType, int UserRole, int MenuKey);

        Task<DO_ReturnParameter> InsertIntoUserGroupMenuAction(DO_UserGroupRole obj);
        #endregion

        #region  User Group & Role
        Task<List<DO_UserRoleMenuLink>> GetUserRoleMenuLinkbyUserId(short UserID, int BusinessKey);

        Task<DO_ReturnParameter> InsertIntoUserRoleMenuLink(DO_UserRoleMenuLink obj);

        Task<DO_ReturnParameter> UpdateUserRoleMenuLink(DO_UserRoleMenuLink obj);

        Task<List<DO_UserType>> GetUserTypesbyUserGroup(int usergroup);

        Task<List<DO_UserRole>> GetUserRolesbyUserType(int usergroup, int usertype);

        #endregion

        #region User Creation
        Task<List<DO_UserMaster>> GetUserMaster();

        Task<DO_UserMaster> GetUserDetails(int UserID);

        Task<List<DO_UserBusinessLink>> GetUserBusinessLocation(short UserID, int CodeTypeUG, int CodeTypeUT);

        Task<DO_ReturnParameter> InsertIntoUserMaster(DO_UserMaster obj);

        Task<DO_ReturnParameter> UpdateUserMaster(DO_UserMaster obj);

        Task<DO_ReturnParameter> InsertIntoUserBL(DO_UserBusinessLink obj);

        Task<DO_ReturnParameter> UpdateUserBL(DO_UserBusinessLink obj);

        Task<List<DO_UserFormAction>> GetUserFormActionLink(short UserID, int BusinessKey, int MenuKey);

        Task<DO_ConfigureMenu> GetMenulist(int UserGroup, int UserType, short UserID, int BusinessKey);

        Task<DO_ReturnParameter> InsertIntoUserMenuAction(DO_UserMenuLink obj);

        List<int> GetMenuKeysforUser(short UserID, int BusinessKey);

        Task<List<DO_UserMaster>> GetUserMasterForUserAuthentication();

        Task<DO_ReturnParameter> UpdateUserMasteronAuthentication(DO_UserMaster obj);

        Task<List<DO_UserMaster>> GetUserMasterForUserDeactivation();

        Task<DO_ReturnParameter> UpdateUserForDeativation(DO_UserMaster obj);

        #endregion User Creation
    }
}
