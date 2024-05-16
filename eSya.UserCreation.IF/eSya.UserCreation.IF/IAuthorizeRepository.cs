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
        Task<List<DO_UserMaster>> GetUnAuthenticatedUsers();
        Task<DO_ReturnParameter> AuthenticateUser(DO_Authorize obj);
        #endregion
    }
}
