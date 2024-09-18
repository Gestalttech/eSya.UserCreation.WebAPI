using eSya.UserCreation.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.IF
{
    public interface IAuthorizeRepository
    {
        #region Authenticate User
        Task<List<DO_UserMaster>> GetUnAuthenticatedUsers(string authenticate);
        Task<DO_ReturnParameter> AuthenticateUser(DO_Authorize obj);
        Task<DO_ReturnParameter> RejectUser(DO_Authorize obj);
        Task<DO_ConfigureMenu> GetUserLinkedFormMenulist(int UserID, int UserGroup, int UserRole);
        Task<List<DO_UserRoleActionLink>> GetActionListByUserRole(int userID, int UserGroup, int UserRole, int formID);
        #endregion
    }
}
