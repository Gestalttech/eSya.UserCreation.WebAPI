using eSya.UserCreation.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.IF
{
    public interface IDeActivateRepository
    {
        #region Authenticate User
        Task<List<DO_UserMaster>> GetActivatedUsers();
        Task<DO_ReturnParameter> DeActivateUser(DO_DeActivated obj);
        #endregion
    }
}
