using eSya.UserCreation.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.IF
{
    public interface ICommonDataRepository
    {
        Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeType(int codeType);

        Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeTypeList(List<int> l_codeType);

        Task<List<DO_BusinessLocation>> GetBusinessKey();

        Task<DO_ConfigureMenu> GetConfigureMenulist();

        Task<List<DO_CountryCodes>> GetISDCodes();

        Task<List<DO_DoctorMaster>> GetDoctorsforCombo();
    }
}
