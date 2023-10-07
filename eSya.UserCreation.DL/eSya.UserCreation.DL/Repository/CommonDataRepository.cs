using eSya.UserCreation.DL.Entities;
using eSya.UserCreation.DO;
using eSya.UserCreation.IF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.DL.Repository
{
    public class CommonDataRepository: ICommonDataRepository
    {
        public async Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeType(int codeType)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcapcds
                        .Where(w => w.CodeType == codeType && w.ActiveStatus)
                        .Select(r => new DO_ApplicationCodes
                        {
                            ApplicationCode = r.ApplicationCode,
                            CodeDesc = r.CodeDesc
                        }).OrderBy(o => o.CodeDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeTypeList(List<int> l_codeType)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcapcds
                        .Where(w => w.ActiveStatus
                        && l_codeType.Contains(w.CodeType))
                        .Select(r => new DO_ApplicationCodes
                        {
                            CodeType = r.CodeType,
                            ApplicationCode = r.ApplicationCode,
                            CodeDesc = r.CodeDesc
                        }).OrderBy(o => o.CodeDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_BusinessLocation>> GetBusinessKey()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEcbslns
                        .Where(w => w.ActiveStatus)
                        .Select(r => new DO_BusinessLocation
                        {
                            BusinessKey = r.BusinessKey,
                            LocationDescription = r.LocationDescription
                        }).ToListAsync();

                    return await bk;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ConfigureMenu> GetConfigureMenulist()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    DO_ConfigureMenu mn = new DO_ConfigureMenu();
                    mn.l_MainMenu = await db.GtEcmamns.Where(x => x.ActiveStatus == true)
                                    .Select(m => new DO_MainMenu()
                                    {
                                        MainMenuId = m.MainMenuId,
                                        MainMenu = m.MainMenu,
                                        MenuIndex = m.MenuIndex,
                                        ActiveStatus = m.ActiveStatus
                                    }).ToListAsync();

                    mn.l_SubMenu = await db.GtEcsbmns.Where(x => x.ActiveStatus == true)
                                    .Select(s => new DO_SubMenu()
                                    {
                                        MainMenuId = s.MainMenuId,
                                        MenuItemId = s.MenuItemId,
                                        MenuItemName = s.MenuItemName,
                                        MenuIndex = s.MenuIndex,
                                        ParentID = s.ParentId,
                                        ActiveStatus = s.ActiveStatus
                                    }).ToListAsync();

                    mn.l_FormMenu = await db.GtEcmnfls.Where(x => x.ActiveStatus == true)
                                    .Select(f => new DO_FormMenu()
                                    {
                                        MainMenuId = f.MainMenuId,
                                        MenuItemId = f.MenuItemId,
                                        FormId = f.FormId,
                                        FormNameClient = f.FormNameClient,
                                        FormIndex = f.FormIndex,
                                        ActiveStatus = f.ActiveStatus,
                                        MenuKey = f.MenuKey
                                    }).ToListAsync();

                    return mn;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_CountryCodes>> GetISDCodes()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEccncds
                        .Where(w => w.ActiveStatus)
                        .Select(r => new DO_CountryCodes
                        {
                            Isdcode = r.Isdcode,
                            CountryName = r.CountryName
                        }).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_DoctorMaster>> GetDoctorsforCombo()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEsdocds.Where(x => x.ActiveStatus)
                        .Select(r => new DO_DoctorMaster
                        {
                            DoctorId = r.DoctorId,
                            DoctorName = r.DoctorName
                        }).OrderBy(o => o.DoctorName).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
