using eSya.UserCreation.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.IF
{
    public interface IApprovalRepository
    {
        Task<List<DO_UserMaster>> GetApproverUserListbyBusinessKey(int BusinessKey);
        Task<DO_ConfigureMenu> GetApprovalRequiredFormMenulist(int BusinessKey, int UserID);
        Task<List<DO_ApprovalLevels>> GetApprovalLevelsbyFormID(int businesskey, int formId);
        Task<DO_ReturnParameter> InsertOrUpdateUserApprovalForm(List<DO_UserApprovalForm> obj);
    }
}
