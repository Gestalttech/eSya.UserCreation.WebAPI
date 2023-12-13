using eSya.UserCreation.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.IF
{
    public interface IeSignatureRepository
    {
        #region Upload User eSignature
        Task<List<DO_eSignature>> GetActiveUsersforSignature();
        Task<DO_eSignature> GetUsereSignaturebyUserID(int UserID);
        Task<DO_ReturnParameter> InsertOrUpdateUsereSignature(DO_eSignature obj);
        #endregion
    }
}
