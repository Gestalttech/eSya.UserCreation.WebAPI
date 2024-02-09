using eSya.UserCreation.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.IF
{
    public interface IBlockRepository
    {
        #region Un-Block User
        Task<List<DO_UserMaster>> GetBlockedUsers();
        Task<DO_ReturnParameter> UpdateBlockSignIn(DO_BlockUser obj);
        #endregion
    }
}
