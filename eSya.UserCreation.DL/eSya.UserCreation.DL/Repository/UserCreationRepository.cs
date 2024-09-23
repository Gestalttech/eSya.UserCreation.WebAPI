using eSya.UserCreation.DL.Entities;
using eSya.UserCreation.DO;
using eSya.UserCreation.IF;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.DL.Repository
{
    public class UserCreationRepository: IUserCreationRepository
    {
        private readonly IStringLocalizer<UserCreationRepository> _localizer;
        public UserCreationRepository(IStringLocalizer<UserCreationRepository> localizer)
        {
            _localizer = localizer;
        }


        //#region  UserGroup & Type
        //public async Task<DO_ConfigureMenu> GetMenulistbyUserGroup(int UserGroup, int BusinessKey, int UserRole)
        //{
        //    try
        //    {
        //        using (eSyaEnterprise db = new eSyaEnterprise())
        //        {
        //            DO_ConfigureMenu mn = new DO_ConfigureMenu();
        //            mn.l_MainMenu = await db.GtEcmamns.Where(x => x.ActiveStatus == true)
        //                            .Select(m => new DO_MainMenu()
        //                            {
        //                                MainMenuId = m.MainMenuId,
        //                                MainMenu = m.MainMenu,
        //                                MenuIndex = m.MenuIndex,
        //                                ActiveStatus = m.ActiveStatus
        //                            }).ToListAsync();

        //            mn.l_SubMenu = await db.GtEcsbmns.Where(x => x.ActiveStatus == true)
        //                            .Select(s => new DO_SubMenu()
        //                            {
        //                                MainMenuId = s.MainMenuId,
        //                                MenuItemId = s.MenuItemId,
        //                                MenuItemName = s.MenuItemName,
        //                                MenuIndex = s.MenuIndex,
        //                                ParentID = s.ParentId,
        //                                ActiveStatus = s.ActiveStatus
        //                            }).ToListAsync();

        //            mn.l_FormMenu = await db.GtEcmnfls.Where(x => x.ActiveStatus == true)
        //                            .Select(f => new DO_FormMenu()
        //                            {
        //                                MainMenuId = f.MainMenuId,
        //                                MenuItemId = f.MenuItemId,
        //                                FormNameClient = f.FormNameClient,
        //                                FormIndex = f.FormIndex,
        //                                //ActiveStatus = f.ActiveStatus,
        //                                //FormId = f.FormId,
        //                                //MenuKey = f.MenuKey
        //                                FormId = f.MenuKey
        //                            }).ToListAsync();
        //            foreach (var obj in mn.l_FormMenu)
        //            {
        //                GtEuusgr getlocDesc = db.GtEuusgrs.Where(c => c.UserGroup == UserGroup && c.BusinessKey == BusinessKey && c.UserRole == UserRole && c.MenuKey == obj.FormId).FirstOrDefault();
        //                if (getlocDesc != null)
        //                {
        //                    obj.ActiveStatus = getlocDesc.ActiveStatus;
        //                }
        //                else
        //                {
        //                    obj.ActiveStatus = false;
        //                }
        //            }
        //            return mn;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<List<DO_UserFormAction>> GetFormActionLinkbyUserGroup(int UserGroup, int UserType, int UserRole, int MenuKey)
        //{
        //    try
        //    {
        //        using (var db = new eSyaEnterprise())
        //        {
        //            GtEcmnfl geformID = db.GtEcmnfls.Where(x => x.MenuKey == MenuKey).FirstOrDefault();
        //            int formID = 0;
        //            if (geformID != null)
        //                formID = geformID.FormId;

        //            var result = await db.GtEcfmacs
        //            .Join(db.GtEcfmals.Where(w => w.FormId == formID),
        //            a => a.ActionId,
        //            f => f.ActionId,
        //            (a, f) => new { a, f })
        //            //(a, f) => new { a, f = f.FirstOrDefault() })
        //            .Select(r => new DO_UserFormAction
        //            {
        //                ActionID = r.a.ActionId,
        //                ActionDesc = r.a.ActionDesc,
        //                ActiveStatus = r.f != null ? r.f.ActiveStatus : false,
        //            }).ToListAsync();
        //            if (result.Count > 0)
        //            {
        //                result = result.GroupBy(x => x.ActionID).Select(g => g.FirstOrDefault()).ToList();
        //            }
        //            foreach (var obj in result)
        //            {
        //                GtEuusrl actions = db.GtEuusrls.Where(x => x.UserRole == UserRole && x.ActionId == obj.ActionID).FirstOrDefault();
        //                if (actions != null)
        //                {
        //                    obj.ActiveStatus = actions.ActiveStatus;
        //                }
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<DO_ReturnParameter> InsertIntoUserGroupMenuAction(DO_UserGroupRole obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                GtEuusgr ug = await db.GtEuusgrs.Where(w => w.UserGroup == obj.UserGroup && w.UserRole == obj.UserRole && w.MenuKey == obj.MenuKey && w.BusinessKey == obj.BusinessKey).FirstOrDefaultAsync();

        //                if (ug != null)
        //                {
        //                    ug.ActiveStatus = obj.ActiveStatus;
        //                    ug.ModifiedBy = obj.UserId;
        //                    ug.ModifiedOn = System.DateTime.Now;
        //                    ug.ModifiedTerminal = obj.TerminalId;
        //                    await db.SaveChangesAsync();
        //                }
        //                else
        //                {
        //                    ug = new GtEuusgr();
        //                    ug.BusinessKey = obj.BusinessKey;
        //                    ug.UserGroup = obj.UserGroup;
        //                    ug.UserRole = obj.UserRole;
        //                    ug.MenuKey = obj.MenuKey;
        //                    ug.ActiveStatus = obj.ActiveStatus;
        //                    ug.FormId = obj.FormId;
        //                    ug.CreatedBy = obj.UserId;
        //                    ug.CreatedOn = DateTime.Now;
        //                    ug.CreatedTerminal = obj.TerminalId;
        //                    db.GtEuusgrs.Add(ug);
        //                    await db.SaveChangesAsync();
        //                }
        //                var fa = await db.GtEuusrls.Where(w => w.UserRole == obj.UserRole).ToListAsync();

        //                foreach (GtEuusrl f in fa)
        //                {
        //                    f.ActiveStatus = false;
        //                    f.ModifiedBy = obj.UserId;
        //                    f.ModifiedOn = System.DateTime.Now;
        //                    f.ModifiedTerminal = obj.TerminalId;
        //                }
        //                await db.SaveChangesAsync();

        //                if (obj.l_formAction != null)
        //                {
        //                    foreach (DO_UserFormAction i in obj.l_formAction)
        //                    {
        //                        var obj_FA = await db.GtEuusrls.Where(w => w.UserRole == obj.UserRole && w.ActionId == i.ActionID).FirstOrDefaultAsync();
        //                        if (obj_FA != null)
        //                        {
        //                            if (i.Active.Substring(0, 1).ToString() == "Y")
        //                                obj_FA.ActiveStatus = true;
        //                            else
        //                                obj_FA.ActiveStatus = false;
        //                            obj_FA.ModifiedBy = obj.UserId;
        //                            obj_FA.ModifiedOn = DateTime.Now;
        //                            obj_FA.ModifiedTerminal = System.Environment.MachineName;
        //                        }
        //                        else
        //                        {
        //                            obj_FA = new GtEuusrl();
        //                            obj_FA.UserRole = obj.UserRole;
        //                            obj_FA.ActionId = i.ActionID;
        //                            if (i.Active.Substring(0, 1).ToString() == "Y")
        //                                obj_FA.ActiveStatus = true;
        //                            else
        //                                obj_FA.ActiveStatus = false;

        //                            obj_FA.FormId = obj.FormId;
        //                            obj_FA.CreatedBy = obj.UserId;
        //                            obj_FA.CreatedOn = DateTime.Now;
        //                            obj_FA.CreatedTerminal = System.Environment.MachineName;
        //                            db.GtEuusrls.Add(obj_FA);
        //                        }
        //                    }
        //                    await db.SaveChangesAsync();
        //                }

        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}
        //#endregion

        //#region  User Group & Role
        //public async Task<List<DO_UserType>> GetUserTypesbyUserGroup(int usergroup)
        //{
        //    try
        //    {
        //        using (var db = new eSyaEnterprise())
        //        {
        //            var _utypes = await db.GtEuusacs.Where(x => x.UserGroup == usergroup && x.ActiveStatus == true)
        //                .Join(db.GtEcapcds,
        //                 x => x.UserType,
        //                 y => y.ApplicationCode,
        //                (x, y) => new DO_UserType
        //                {
        //                    UserTypeId = x.UserType,
        //                    UserTypeDesc = y.CodeDesc

        //                }).ToListAsync();
        //            var _uniqueutypes = _utypes.GroupBy(e => e.UserTypeId).Select(g => g.First());
        //            return _uniqueutypes.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<List<DO_UserRole>> GetUserRolesbyUserType(int usergroup, int usertype)
        //{
        //    try
        //    {
        //        using (var db = new eSyaEnterprise())
        //        {
        //            var _utypes = await db.GtEuusacs.Where(x => x.UserGroup == usergroup && x.UserType == usertype && x.ActiveStatus == true)
        //                .Join(db.GtEcapcds,
        //                 x => x.UserRole,
        //                 y => y.ApplicationCode,
        //                (x, y) => new DO_UserRole
        //                {
        //                    UserRoleId = x.UserRole,
        //                    UserRoleDesc = y.CodeDesc

        //                }).ToListAsync();
        //            var _uniqueutypes = _utypes.GroupBy(e => e.UserRoleId).Select(g => g.First());
        //            return _uniqueutypes.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<List<DO_UserRoleMenuLink>> GetUserRoleMenuLinkbyUserId(short UserID, int BusinessKey)

        //{
        //    try
        //    {
        //        using (var db = new eSyaEnterprise())
        //        {
        //            var ds = await db.GtEuusrls.Where(k => k.UserId == UserID && k.BusinessKey == BusinessKey).Join(db.GtEcapcds,
        //                 x => x.UserGroup,
        //                 y => y.ApplicationCode,
        //                 (x, y) => new { x, y }).Join(db.GtEcapcds,
        //                 a => a.x.UserType,
        //                 p => p.ApplicationCode, (a, p) => new { a, p }).Join(db.GtEcapcds,
        //                 b => b.a.x.UserRole,
        //                 c => c.ApplicationCode, (b, c) => new { b, c }).Select(r => new DO_UserRoleMenuLink
        //                 {
        //                     BusinessKey = r.b.a.x.BusinessKey,
        //                     UserId = r.b.a.x.UserId,
        //                     UserGroup = r.b.a.x.UserGroup,
        //                     UserType = r.b.a.x.UserType,
        //                     UserRole = r.b.a.x.UserRole,
        //                     EffectiveFrom = r.b.a.x.EffectiveFrom,
        //                     EffectiveTill = r.b.a.x.EffectiveTill,
        //                     ActiveStatus = r.b.a.x.ActiveStatus,
        //                     UserGroupDesc = r.b.a.y.CodeDesc,
        //                     UserTypeDesc = r.b.p.CodeDesc,
        //                     UserRoleDesc = r.c.CodeDesc
        //                 }).ToListAsync();

        //            return ds;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<DO_ReturnParameter> InsertIntoUserRoleMenuLink(DO_UserRoleMenuLink obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var _isExist = db.GtEuusrls.Where(x => x.UserId == obj.UserId && x.BusinessKey == obj.BusinessKey && x.UserGroup == obj.UserGroup && x.UserRole == obj.UserRole && x.UserType == obj.UserType && x.EffectiveFrom.Date == obj.EffectiveFrom.Date).Count();
        //                if (_isExist > 0)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0125", Message = string.Format(_localizer[name: "W0125"]) };
        //                }
        //                var role_link = new GtEuusrl
        //                {
        //                    UserId = obj.UserId,
        //                    BusinessKey = obj.BusinessKey,
        //                    UserGroup = obj.UserGroup,
        //                    UserType = obj.UserType,
        //                    UserRole = obj.UserRole,
        //                    EffectiveFrom = obj.EffectiveFrom,
        //                    EffectiveTill = obj.EffectiveTill,
        //                    ActiveStatus = obj.ActiveStatus,
        //                    FormId = obj.FormId,
        //                    CreatedBy = obj.CreatedBy,
        //                    CreatedOn = System.DateTime.Now,
        //                    CreatedTerminal = obj.TerminalId,
        //                };
        //                db.GtEuusrls.Add(role_link);

        //                await db.SaveChangesAsync();
        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        //public async Task<DO_ReturnParameter> UpdateUserRoleMenuLink(DO_UserRoleMenuLink obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var _rolelink = db.GtEuusrls.Where(x => x.UserId == obj.UserId && x.BusinessKey == obj.BusinessKey && x.UserGroup == obj.UserGroup && x.UserRole == obj.UserRole && x.UserType == obj.UserType && x.EffectiveFrom.Date == obj.EffectiveFrom.Date).FirstOrDefault();
        //                if (_rolelink != null)
        //                {
        //                    _rolelink.EffectiveTill = obj.EffectiveTill;
        //                    _rolelink.ActiveStatus = obj.ActiveStatus;
        //                    _rolelink.ModifiedBy = obj.CreatedBy;
        //                    _rolelink.ModifiedOn = System.DateTime.Now;
        //                    _rolelink.ModifiedTerminal = obj.TerminalId;
        //                    await db.SaveChangesAsync();
        //                    dbContext.Commit();

        //                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
        //                }
        //                else
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0126", Message = string.Format(_localizer[name: "W0126"]) };
        //                }

        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}
        //#endregion

        //#region User Creation
        //public async Task<List<DO_UserMaster>> GetUserMaster()
        //{
        //    try
        //    {
        //        using (var db = new eSyaEnterprise())
        //        {
        //            var ds = db.GtEuusms
        //                .Select(r => new DO_UserMaster
        //                {
        //                    UserID = r.UserId,
        //                    LoginID = r.LoginId,
        //                    LoginDesc = r.LoginDesc,
        //                    ISDCode = r.Isdcode ?? 0,
        //                    MobileNumber = r.MobileNumber,
        //                    LastActivityDate = (DateTime)r.LastActivityDate,
        //                    ActiveStatus = r.ActiveStatus,
        //                }).OrderBy(o => o.LoginDesc).ToListAsync();

        //            return await ds;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<DO_UserMaster> GetUserDetails(int UserID)
        //{
        //    try
        //    {
        //        using (var db = new eSyaEnterprise())
        //        {
        //            var ds = db.GtEuusms
        //                .Where(w => w.UserId == UserID)
        //                .Select(r => new DO_UserMaster
        //                {
        //                    LoginID = r.LoginId,
        //                    LoginDesc = r.LoginDesc,
        //                    ISDCode = r.Isdcode ?? 0,
        //                    MobileNumber = r.MobileNumber,
        //                    AllowMobileLogin = (bool)r.AllowMobileLogin,
        //                    IsApprover = r.IsApprover,
        //                    eMailID = r.EMailId,
        //                    Photo = r.Photo,
        //                    Password = eSyaCryptGeneration.Decrypt(r.Password),
        //                    DigitalSignature = r.DigitalSignature,
        //                    IsDoctor = r.IsDoctor,
        //                    DoctorId = r.DoctorId,
        //                    ActiveStatus = r.ActiveStatus,
        //                }).FirstOrDefaultAsync();

        //            return await ds;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<DO_ReturnParameter> InsertIntoUserMaster(DO_UserMaster obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var _isLoginIdExist = db.GtEuusms.Where(w => w.LoginId == obj.LoginID).Count();
        //                if (_isLoginIdExist > 0)
        //                {

        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0111", Message = string.Format(_localizer[name: "W0111"]) };
        //                }

        //                var _isMobileNoExist = db.GtEuusms.Where(w => w.MobileNumber == obj.MobileNumber).Count();
        //                if (_isMobileNoExist > 0)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0118", Message = string.Format(_localizer[name: "W0118"]) };
        //                }

        //                var _isEmaiExist = db.GtEuusms.Where(w => w.EMailId == obj.eMailID).Count();
        //                if (_isEmaiExist > 0)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0127", Message = string.Format(_localizer[name: "W0127"]) };
        //                }

        //                int _userId = 0;

        //                int maxUserId = db.GtEuusms.Select(c => c.UserId).DefaultIfEmpty().Max();
        //                _userId = maxUserId + 1;

        //                var ap_cd = new GtEuusm
        //                {
        //                    UserId = _userId,
        //                    LoginId = obj.LoginID,
        //                    LoginDesc = obj.LoginDesc,
        //                    Password = eSyaCryptGeneration.Encrypt(obj.Password),
        //                    Isdcode = obj.ISDCode,
        //                    MobileNumber = obj.MobileNumber,
        //                    AllowMobileLogin = obj.AllowMobileLogin,
        //                    IsApprover = obj.IsApprover,
        //                    EMailId = obj.eMailID,
        //                    Photo = obj.Photo,
        //                    PhotoUrl = null,
        //                    DigitalSignature = obj.DigitalSignature,
        //                    LastPasswordChangeDate = null,
        //                    LastActivityDate = null,
        //                    Otpnumber = null,
        //                    OtpgeneratedDate = null,
        //                    IsUserAuthenticated = false,
        //                    UserAuthenticatedDate = null,
        //                    IsUserDeactivated = false,
        //                    UserDeactivatedOn = null,
        //                    IsDoctor = obj.IsDoctor,
        //                    DoctorId = obj.DoctorId,
        //                    ActiveStatus = false,
        //                    FormId = obj.FormId,
        //                    CreatedBy = obj.CreatedBy,
        //                    CreatedOn = System.DateTime.Now,
        //                    CreatedTerminal = obj.TerminalId,
        //                };
        //                db.GtEuusms.Add(ap_cd);

        //                await db.SaveChangesAsync();
        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]), ID = _userId };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        //public async Task<DO_ReturnParameter> UpdateUserMaster(DO_UserMaster obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                GtEuusm ap_cd = db.GtEuusms.Where(w => w.UserId == obj.UserID).FirstOrDefault();
        //                if (ap_cd == null)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
        //                }

        //                var _isMobileNoExist = db.GtEuusms.Where(w => w.UserId != obj.UserID && w.MobileNumber == obj.MobileNumber).Count();
        //                if (_isMobileNoExist > 0)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0128", Message = string.Format(_localizer[name: "W0128"]) };
        //                }

        //                var _isEmaiExist = db.GtEuusms.Where(w => w.UserId != obj.UserID && w.EMailId == obj.eMailID).Count();
        //                if (_isEmaiExist > 0)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0129", Message = string.Format(_localizer[name: "W0129"]) };
        //                }

        //                ap_cd.LoginDesc = obj.LoginDesc;
        //                ap_cd.Isdcode = obj.ISDCode;
        //                ap_cd.MobileNumber = obj.MobileNumber;
        //                ap_cd.AllowMobileLogin = obj.AllowMobileLogin;
        //                ap_cd.IsApprover = obj.IsApprover;
        //                ap_cd.EMailId = obj.eMailID;
        //                ap_cd.Photo = obj.Photo;
        //                ap_cd.PhotoUrl = null;
        //                ap_cd.DigitalSignature = obj.DigitalSignature;
        //                ap_cd.IsDoctor = obj.IsDoctor;
        //                ap_cd.DoctorId = obj.DoctorId;
        //                ap_cd.ModifiedBy = obj.UserID;
        //                ap_cd.ModifiedOn = System.DateTime.Now;
        //                ap_cd.ModifiedTerminal = obj.TerminalId;

        //                await db.SaveChangesAsync();

        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]), ID = obj.UserID };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        //public async Task<List<DO_UserBusinessLink>> GetUserBusinessLocation(short UserID, int CodeTypeUG, int CodeTypeUT)
        //{
        //    try
        //    {
        //        using (var db = new eSyaEnterprise())
        //        {
        //            var result = await db.GtEcbslns
        //                .Join(db.GtEcbsens,
        //                    lkey => new { lkey.BusinessId },
        //                    ent => new { ent.BusinessId },
        //                    (lkey, ent) => new { lkey, ent })
        //                .Where(x => x.ent.ActiveStatus && x.lkey.ActiveStatus)
        //                .Select(c => new DO_UserBusinessLink
        //                {
        //                    BusinessKey = c.lkey.BusinessKey,
        //                    LocationDescription = c.lkey.LocationDescription,

        //                }).ToListAsync();
        //            foreach (var obj in result)
        //            {
        //                if (UserID != 0)
        //                {
        //                    GtEuusbl isBusinessSegment = await db.GtEuusbls
        //                        .Where(c => c.UserId == UserID && c.BusinessKey == obj.BusinessKey).FirstOrDefaultAsync();
        //                    if (isBusinessSegment != null)
        //                    {
        //                        obj.UserGroup = isBusinessSegment.UserGroup.Value;
        //                        obj.IUStatus = 1;
        //                        GtEcapcd UserGroupDescription = await db.GtEcapcds
        //                        .Where(c => c.CodeType == CodeTypeUG && c.ApplicationCode == obj.UserGroup).FirstOrDefaultAsync();

        //                        if (UserGroupDescription != null)
        //                            obj.UserGroupDesc = UserGroupDescription.CodeDesc;

        //                        obj.UserType = isBusinessSegment.UserType.Value;

        //                        GtEcapcd UserTypeDescription = await db.GtEcapcds
        //                        .Where(c => c.CodeType == CodeTypeUT && c.ApplicationCode == obj.UserType).FirstOrDefaultAsync();

        //                        if (UserTypeDescription != null)
        //                            obj.UserTypeDesc = UserTypeDescription.CodeDesc;

        //                        obj.AllowMTFY = isBusinessSegment.AllowMtfy;
        //                        obj.ActiveStatus = isBusinessSegment.ActiveStatus;
        //                    }
        //                    else
        //                    {
        //                        //obj.UserGroup = 0;
        //                        //obj.UserGroupDesc = null;
        //                        //obj.UserType = 0;
        //                        //obj.UserTypeDesc = null;
        //                        obj.IUStatus = 0;
        //                        obj.AllowMTFY = false;
        //                        obj.ActiveStatus = false;
        //                    }
        //                }
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<DO_ReturnParameter> InsertIntoUserBL(DO_UserBusinessLink obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var ap_cd = new GtEuusbl
        //                {
        //                    UserId = obj.UserID,
        //                    BusinessKey = obj.BusinessKey,
        //                    UserGroup = obj.UserGroup,
        //                    UserType = obj.UserType,
        //                    AllowMtfy = obj.AllowMTFY,
        //                    ActiveStatus = obj.ActiveStatus,
        //                    FormId = obj.FormId,
        //                    CreatedBy = obj.CreatedBy,
        //                    CreatedOn = System.DateTime.Now,
        //                    CreatedTerminal = obj.TerminalId,
        //                };
        //                db.GtEuusbls.Add(ap_cd);
        //                await db.SaveChangesAsync();

        //                //Insert Default Record in User Menu Link
        //                var MenuKey = await db.GtEuusgrs.Where(x => x.UserGroup == obj.UserGroup && x.UserType == obj.UserType && x.ActiveStatus == true).ToListAsync();
        //                foreach (var mkey in MenuKey)
        //                {
        //                    GtEuusml userMenuLink = new GtEuusml();
        //                    userMenuLink.UserId = obj.UserID;
        //                    userMenuLink.BusinessKey = obj.BusinessKey;
        //                    userMenuLink.MenuKey = mkey.MenuKey;
        //                    userMenuLink.ActiveStatus = obj.ActiveStatus;
        //                    userMenuLink.FormId = obj.FormId;
        //                    userMenuLink.CreatedBy = obj.CreatedBy;
        //                    userMenuLink.CreatedOn = System.DateTime.Now;
        //                    userMenuLink.CreatedTerminal = obj.TerminalId;
        //                    db.GtEuusmls.Add(userMenuLink);
        //                    await db.SaveChangesAsync();
        //                }

        //                //Insert Default Record in User Menu Action Link
        //                var MenuActionLink = await db.GtEcmnfls.Join(db.GtEcfmals, u => u.FormId, uir => uir.FormId,
        //                        (u, uir) => new { u, uir }).
        //                        Join(db.GtEuusgrs, r => r.u.MenuKey, ro => ro.MenuKey, (r, ro) => new { r, ro })
        //                        .Where(m => m.ro.UserGroup == obj.UserGroup && m.ro.UserType == obj.UserType)
        //                        .Select(m => new DO_UserFormAction
        //                        {
        //                            MenuKey = m.ro.MenuKey,
        //                            ActionID = m.r.uir.ActionId
        //                        }).ToListAsync();

        //                foreach (var makey in MenuActionLink)
        //                {
        //                    GtEuusfa userMenuActionLink = new GtEuusfa();
        //                    userMenuActionLink.UserId = obj.UserID;
        //                    userMenuActionLink.BusinessKey = obj.BusinessKey;
        //                    userMenuActionLink.MenuKey = makey.MenuKey;
        //                    userMenuActionLink.ActionId = makey.ActionID;
        //                    userMenuActionLink.ActiveStatus = obj.ActiveStatus;
        //                    userMenuActionLink.FormId = obj.FormId;
        //                    userMenuActionLink.CreatedBy = obj.CreatedBy;
        //                    userMenuActionLink.CreatedOn = System.DateTime.Now;
        //                    userMenuActionLink.CreatedTerminal = obj.TerminalId;
        //                    db.GtEuusfas.Add(userMenuActionLink);
        //                    await db.SaveChangesAsync();
        //                }

        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        //public async Task<DO_ReturnParameter> UpdateUserBL(DO_UserBusinessLink obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                GtEuusbl ap_cd = await db.GtEuusbls.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey).FirstOrDefaultAsync();
        //                if (ap_cd == null)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0126", Message = string.Format(_localizer[name: "W0126"]) };
        //                }

        //                if (ap_cd.UserGroup != obj.UserGroup || ap_cd.UserType != obj.UserType)
        //                {
        //                    var u_fal = await db.GtEuusfas.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey).ToListAsync();
        //                    if (u_fal != null)
        //                    {
        //                        db.GtEuusfas.RemoveRange(u_fal);
        //                        await db.SaveChangesAsync();
        //                    }

        //                    var um_lnk = await db.GtEuusmls.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey).ToListAsync();
        //                    if (um_lnk != null)
        //                    {
        //                        db.GtEuusmls.RemoveRange(um_lnk);
        //                        await db.SaveChangesAsync();
        //                    }

        //                    //Insert Default Record in User Menu Link

        //                    var MenuKey = await db.GtEuusgrs.Where(x => x.UserGroup == obj.UserGroup && x.UserType == obj.UserType && x.ActiveStatus == true).ToListAsync();
        //                    foreach (var mkey in MenuKey)
        //                    {
        //                        GtEuusml userMenuLink = new GtEuusml();
        //                        userMenuLink.UserId = obj.UserID;
        //                        userMenuLink.BusinessKey = obj.BusinessKey;
        //                        userMenuLink.MenuKey = mkey.MenuKey;
        //                        userMenuLink.ActiveStatus = obj.ActiveStatus;
        //                        userMenuLink.FormId = obj.FormId;
        //                        userMenuLink.CreatedBy = obj.CreatedBy;
        //                        userMenuLink.CreatedOn = System.DateTime.Now;
        //                        userMenuLink.CreatedTerminal = obj.TerminalId;
        //                        db.GtEuusmls.Add(userMenuLink);
        //                        await db.SaveChangesAsync();
        //                    }

        //                    //Insert Default Record in User Menu Action Link

        //                    var MenuActionLink = await db.GtEcmnfls.Join(db.GtEcfmals, u => u.FormId, uir => uir.FormId,
        //                            (u, uir) => new { u, uir }).
        //                            Join(db.GtEuusgrs, r => r.u.MenuKey, ro => ro.MenuKey, (r, ro) => new { r, ro })
        //                            .Where(m => m.ro.UserGroup == obj.UserGroup && m.ro.UserType == obj.UserType)
        //                            .Select(m => new DO_UserFormAction
        //                            {
        //                                MenuKey = m.ro.MenuKey,
        //                                ActionID = m.r.uir.ActionId
        //                            }).ToListAsync();

        //                    foreach (var makey in MenuActionLink)
        //                    {
        //                        GtEuusfa userMenuActionLink = new GtEuusfa();
        //                        userMenuActionLink.UserId = obj.UserID;
        //                        userMenuActionLink.BusinessKey = obj.BusinessKey;
        //                        userMenuActionLink.MenuKey = makey.MenuKey;
        //                        userMenuActionLink.ActionId = makey.ActionID;
        //                        userMenuActionLink.ActiveStatus = obj.ActiveStatus;
        //                        userMenuActionLink.FormId = obj.FormId;
        //                        userMenuActionLink.CreatedBy = obj.CreatedBy;
        //                        userMenuActionLink.CreatedOn = System.DateTime.Now;
        //                        userMenuActionLink.CreatedTerminal = obj.TerminalId;
        //                        db.GtEuusfas.Add(userMenuActionLink);
        //                        await db.SaveChangesAsync();
        //                    }
        //                }

        //                ap_cd.UserGroup = obj.UserGroup;
        //                ap_cd.UserType = obj.UserType;
        //                ap_cd.AllowMtfy = obj.AllowMTFY;
        //                ap_cd.ActiveStatus = obj.ActiveStatus;
        //                ap_cd.ModifiedBy = obj.UserID;
        //                ap_cd.ModifiedOn = System.DateTime.Now;
        //                ap_cd.ModifiedTerminal = obj.TerminalId;

        //                await db.SaveChangesAsync();

        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        //public async Task<DO_ConfigureMenu> GetMenulist(int UserGroup, int UserType, short UserID, int BusinessKey)
        //{
        //    try
        //    {
        //        //UserGroup = 10001;
        //        //UserType = 20004;
        //        using (eSyaEnterprise db = new eSyaEnterprise())
        //        {
        //            DO_ConfigureMenu mn = new DO_ConfigureMenu();

        //            mn.l_MainMenu = await db.GtEuusgrs.Join(db.GtEcmnfls,
        //                    lkey => new { lkey.MenuKey },
        //                    ent => new { ent.MenuKey },
        //                    (lkey, ent) => new { lkey, ent })
        //                    .Join(db.GtEcmamns,
        //                Bloc => new { Bloc.ent.MainMenuId },
        //                seg => new { seg.MainMenuId },
        //                (Bloc, seg) => new { Bloc, seg })
        //                .Where(x => x.Bloc.lkey.UserGroup == UserGroup && x.Bloc.lkey.UserType == UserType && x.Bloc.lkey.ActiveStatus == true)
        //                            .Select(m => new DO_MainMenu()
        //                            {
        //                                MainMenuId = m.Bloc.ent.MainMenuId,
        //                                MainMenu = m.seg.MainMenu,
        //                                MenuIndex = m.seg.MenuIndex
        //                            }).Distinct().ToListAsync();

        //            //mn.l_MainMenu = await db.GtEcmamn.Where(w => w.ActiveStatus == true)
        //            //                .Select(m => new DO_MainMenu()
        //            //                {
        //            //                    MainMenuId = m.MainMenuId,
        //            //                    MainMenu = m.MainMenu,
        //            //                    MenuIndex = m.MenuIndex
        //            //                }).ToListAsync();

        //            mn.l_SubMenu = await db.GtEuusgrs.Join(db.GtEcmnfls,
        //                    lkey => new { lkey.MenuKey },
        //                    ent => new { ent.MenuKey },
        //                    (lkey, ent) => new { lkey, ent })
        //                    .Join(db.GtEcsbmns,
        //                Bloc => new { Bloc.ent.MainMenuId, Bloc.ent.MenuItemId },
        //                seg => new { seg.MainMenuId, seg.MenuItemId },
        //                (Bloc, seg) => new { Bloc, seg })
        //                .Where(x => x.Bloc.lkey.UserGroup == UserGroup && x.Bloc.lkey.UserType == UserType && x.Bloc.lkey.ActiveStatus == true)
        //                            .Select(f => new DO_SubMenu()
        //                            {
        //                                MainMenuId = f.Bloc.ent.MainMenuId,
        //                                MenuItemId = f.Bloc.ent.MenuItemId,
        //                                MenuItemName = f.seg.MenuItemName,
        //                                MenuIndex = f.seg.MenuIndex,
        //                                ParentID = f.seg.ParentId
        //                            }).Distinct().ToListAsync();

        //            //mn.l_SubMenu = await db.GtEcsbmn.Where(w => w.ActiveStatus == true)
        //            //                .Select(s => new DO_SubMenu()
        //            //                {
        //            //                    MainMenuId = s.MainMenuId,
        //            //                    MenuItemId = s.MenuItemId,
        //            //                    MenuItemName = s.MenuItemName,
        //            //                    MenuIndex = s.MenuIndex,
        //            //                    ParentID = s.ParentId
        //            //                }).ToListAsync();

        //            mn.l_FormMenu = await db.GtEuusgrs.Join(db.GtEcmnfls,
        //                    lkey => new { lkey.MenuKey },
        //                    ent => new { ent.MenuKey },
        //                    (lkey, ent) => new { lkey, ent })
        //                .Where(x => x.lkey.UserGroup == UserGroup && x.lkey.UserType == UserType)
        //                            .Select(f => new DO_FormMenu()
        //                            {
        //                                MainMenuId = f.ent.MainMenuId,
        //                                MenuItemId = f.ent.MenuItemId,
        //                                FormId = f.ent.MenuKey,
        //                                FormNameClient = f.ent.FormNameClient,
        //                                FormIndex = f.ent.FormIndex
        //                            }).ToListAsync();

        //            foreach (var obj in mn.l_FormMenu)
        //            {
        //                GtEuusml getlocDesc = db.GtEuusmls.Where(c => c.UserId == UserID && c.BusinessKey == BusinessKey && c.MenuKey == obj.FormId).FirstOrDefault();
        //                if (getlocDesc != null)
        //                {
        //                    obj.ActiveStatus = getlocDesc.ActiveStatus;
        //                }
        //                else
        //                {
        //                    obj.ActiveStatus = false;
        //                }
        //            }
        //            return mn;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<List<DO_UserFormAction>> GetUserFormActionLink(short UserID, int BusinessKey, int MenuKey)
        //{
        //    try
        //    {
        //        using (var db = new eSyaEnterprise())
        //        {
        //            //var result = db.GtEuusfa
        //            //    .Join(db.GtEcfmac,
        //            //        lkey => new { lkey.ActionId },
        //            //        ent => new { ent.ActionId },
        //            //        (lkey, ent) => new { lkey, ent })
        //            //    .Where(x =>
        //            //          x.lkey.UserId == UserID && x.lkey.BusinessKey == BusinessKey && x.lkey.MenuKey == MenuKey)
        //            //    .Select(c => new DO_UserFormAction
        //            //    {
        //            //        ActionID = c.lkey.ActionId,
        //            //        ActionDesc = c.ent.ActionDesc,
        //            //        ActiveStatus = c.lkey.ActiveStatus
        //            //    }).ToListAsync();

        //            GtEcmnfl geformID = db.GtEcmnfls.Where(x => x.MenuKey == MenuKey).FirstOrDefault();
        //            int formID = 0;
        //            if (geformID != null)
        //                formID = geformID.FormId;

        //            var result = await db.GtEcfmacs
        //            .Join(db.GtEcfmals.Where(w => w.FormId == formID),
        //            a => a.ActionId,
        //            f => f.ActionId,
        //            (a, f) => new { a, f })
        //            //(a, f) => new { a, f = f.FirstOrDefault() })
        //            .Select(r => new DO_UserFormAction
        //            {
        //                ActionID = r.a.ActionId,
        //                ActionDesc = r.a.ActionDesc,
        //                ActiveStatus = r.f != null ? r.f.ActiveStatus : false,
        //            }).ToListAsync();

        //            foreach (var obj in result)
        //            {
        //                GtEuusfa getUserAction = db.GtEuusfas.Where(x => x.UserId == UserID && x.BusinessKey == BusinessKey && x.MenuKey == MenuKey && x.ActionId == obj.ActionID).FirstOrDefault();
        //                if (getUserAction != null)
        //                {
        //                    obj.ActiveStatus = getUserAction.ActiveStatus;
        //                }
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<DO_ReturnParameter> InsertIntoUserMenuAction(DO_UserMenuLink obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                GtEuusml uml = await db.GtEuusmls.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey && w.MenuKey == obj.MenuKey).FirstOrDefaultAsync();

        //                if (uml != null)
        //                {
        //                    uml.ActiveStatus = obj.ActiveStatus;
        //                    uml.ModifiedBy = obj.CreatedBy;
        //                    uml.ModifiedOn = System.DateTime.Now;
        //                    uml.ModifiedTerminal = obj.TerminalId;
        //                    await db.SaveChangesAsync();
        //                }
        //                else
        //                {
        //                    uml = new GtEuusml();
        //                    uml.UserId = obj.UserID;
        //                    uml.BusinessKey = obj.BusinessKey;
        //                    uml.MenuKey = obj.MenuKey;
        //                    uml.ActiveStatus = obj.ActiveStatus;
        //                    uml.FormId = obj.FormId;
        //                    uml.CreatedBy = obj.UserID;
        //                    uml.CreatedOn = DateTime.Now;
        //                    uml.CreatedTerminal = System.Environment.MachineName;
        //                    db.GtEuusmls.Add(uml);
        //                    await db.SaveChangesAsync();
        //                }

        //                //if (obj.ActiveStatus == true)
        //                //{
        //                var fa = await db.GtEuusfas.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey && w.MenuKey == obj.MenuKey).ToListAsync();

        //                foreach (GtEuusfa f in fa)
        //                {
        //                    f.ActiveStatus = false;
        //                    f.ModifiedBy = obj.UserID;
        //                    f.ModifiedOn = System.DateTime.Now;
        //                    f.ModifiedTerminal = obj.TerminalId;
        //                }
        //                await db.SaveChangesAsync();

        //                if (obj.l_formAction != null)
        //                {
        //                    foreach (DO_UserFormAction i in obj.l_formAction)
        //                    {
        //                        var obj_FA = await db.GtEuusfas.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey && w.MenuKey == obj.MenuKey && w.ActionId == i.ActionID).FirstOrDefaultAsync();
        //                        if (obj_FA != null)
        //                        {
        //                            if (i.Active.Substring(0, 1).ToString() == "Y")
        //                                obj_FA.ActiveStatus = true;
        //                            else
        //                                obj_FA.ActiveStatus = false;
        //                            obj_FA.ModifiedBy = obj.UserID;
        //                            obj_FA.ModifiedOn = DateTime.Now;
        //                            obj_FA.ModifiedTerminal = System.Environment.MachineName;
        //                        }
        //                        else
        //                        {
        //                            obj_FA = new GtEuusfa();
        //                            obj_FA.UserId = obj.UserID;
        //                            obj_FA.BusinessKey = obj.BusinessKey;
        //                            obj_FA.MenuKey = obj.MenuKey;
        //                            obj_FA.ActionId = i.ActionID;
        //                            if (i.Active.Substring(0, 1).ToString() == "Y")
        //                                obj_FA.ActiveStatus = true;
        //                            else
        //                                obj_FA.ActiveStatus = false;
        //                            obj_FA.FormId = obj.FormId;
        //                            obj_FA.CreatedBy = obj.UserID;
        //                            obj_FA.CreatedOn = DateTime.Now;
        //                            obj_FA.CreatedTerminal = System.Environment.MachineName;
        //                            db.GtEuusfas.Add(obj_FA);
        //                        }
        //                    }
        //                    await db.SaveChangesAsync();
        //                }
        //                //}
        //                //else
        //                //{
        //                //    var fa = await db.GtEuusfa.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey && w.MenuKey == obj.MenuKey).ToListAsync();

        //                //    foreach (GtEuusfa f in fa)
        //                //    {
        //                //        f.ActiveStatus = false;
        //                //        f.ModifiedBy = obj.UserID;
        //                //        f.ModifiedOn = System.DateTime.Now;
        //                //        f.ModifiedTerminal = obj.TerminalId;
        //                //    }
        //                //    await db.SaveChangesAsync();
        //                //}
        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        //public List<int> GetMenuKeysforUser(short UserID, int BusinessKey)
        //{
        //    try
        //    {
        //        List<int> menukeys = new List<int>();
        //        using (var db = new eSyaEnterprise())
        //        {
        //            IEnumerable<GtEuusml> UserMenu = db.GtEuusmls.Where(u => u.UserId == UserID && u.BusinessKey == BusinessKey && u.ActiveStatus == true);
        //            int key;
        //            foreach (GtEuusml obj in UserMenu)
        //            {
        //                key = new int();
        //                key = obj.MenuKey;
        //                menukeys.Add(key);
        //            }
        //        }
        //        return menukeys;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<List<DO_UserMaster>> GetUserMasterForUserAuthentication()
        //{
        //    try
        //    {
        //        using (var db = new eSyaEnterprise())
        //        {
        //            var ds = db.GtEuusms.Where(x => x.IsUserAuthenticated == false && x.ActiveStatus == false)
        //                .Select(r => new DO_UserMaster
        //                {
        //                    UserID = r.UserId,
        //                    LoginID = r.LoginId,
        //                    LoginDesc = r.LoginDesc,
        //                    ISDCode = r.Isdcode ?? 0,
        //                    MobileNumber = r.MobileNumber,
        //                    LastActivityDate = (DateTime)r.LastActivityDate,
        //                    ActiveStatus = r.ActiveStatus,
        //                }).OrderBy(o => o.LoginDesc).ToListAsync();

        //            return await ds;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<DO_ReturnParameter> UpdateUserMasteronAuthentication(DO_UserMaster obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                GtEuusm ap_cd = db.GtEuusms.Where(w => w.UserId == obj.UserID).FirstOrDefault();
        //                if (ap_cd == null)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
        //                }

        //                var _isMobileNoExist = db.GtEuusms.Where(w => w.UserId != obj.UserID && w.MobileNumber == obj.MobileNumber).Count();
        //                if (_isMobileNoExist > 0)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0128", Message = string.Format(_localizer[name: "W0128"]) };
        //                }

        //                var _isEmaiExist = db.GtEuusms.Where(w => w.UserId != obj.UserID && w.EMailId == obj.eMailID).Count();
        //                if (_isEmaiExist > 0)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0129", Message = string.Format(_localizer[name: "W0129"]) };
        //                }

        //                ap_cd.LoginDesc = obj.LoginDesc;
        //                ap_cd.Isdcode = obj.ISDCode;
        //                ap_cd.MobileNumber = obj.MobileNumber;
        //                ap_cd.AllowMobileLogin = obj.AllowMobileLogin;
        //                ap_cd.EMailId = obj.eMailID;
        //                ap_cd.Photo = obj.Photo;
        //                ap_cd.PhotoUrl = null;
        //                ap_cd.DigitalSignature = obj.DigitalSignature;
        //                ap_cd.IsUserAuthenticated = true;
        //                ap_cd.UserAuthenticatedDate = System.DateTime.Now;
        //                ap_cd.ActiveStatus = true;
        //                ap_cd.ModifiedBy = obj.UserID;
        //                ap_cd.ModifiedOn = System.DateTime.Now;
        //                ap_cd.ModifiedTerminal = obj.TerminalId;

        //                await db.SaveChangesAsync();

        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, Message = string.Format(_localizer[name: "S0006"]), ID = obj.UserID };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        //public async Task<List<DO_UserMaster>> GetUserMasterForUserDeactivation()
        //{
        //    try
        //    {
        //        using (var db = new eSyaEnterprise())
        //        {
        //            var ds = db.GtEuusms.Where(x => x.IsUserDeactivated == false && x.ActiveStatus == true)
        //                .Select(r => new DO_UserMaster
        //                {
        //                    UserID = r.UserId,
        //                    LoginID = r.LoginId,
        //                    LoginDesc = r.LoginDesc,
        //                    ISDCode = r.Isdcode ?? 0,
        //                    MobileNumber = r.MobileNumber,
        //                    LastActivityDate = (DateTime)r.LastActivityDate,
        //                    ActiveStatus = r.ActiveStatus,
        //                }).OrderBy(o => o.LoginDesc).ToListAsync();

        //            return await ds;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<DO_ReturnParameter> UpdateUserForDeativation(DO_UserMaster obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                GtEuusm ap_cd = db.GtEuusms.Where(w => w.UserId == obj.UserID).FirstOrDefault();
        //                if (ap_cd == null)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
        //                }

        //                ap_cd.DeactivationReason = obj.DeactivationReason;
        //                ap_cd.IsUserDeactivated = true;
        //                ap_cd.UserDeactivatedOn = System.DateTime.Now;
        //                ap_cd.ActiveStatus = false;
        //                ap_cd.ModifiedBy = obj.UserID;
        //                ap_cd.ModifiedOn = System.DateTime.Now;
        //                ap_cd.ModifiedTerminal = obj.TerminalId;

        //                await db.SaveChangesAsync();

        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, StatusCode = "S0007", Message = string.Format(_localizer[name: "S0007"]), ID = obj.UserID };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        //#endregion User Creation

        #region User Creation New Process

        #region User Group
        public async Task<List<DO_ApplicationCodes>> GetActiveUserRolesByCodeType(int codeType)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds=db.GtEuusrls.Join
                        (db.GtEcapcds,
                        x => new {x.UserRole},
                        y=>  new { UserRole=y.ApplicationCode},
                        (x,y)=> new {x,y}).Where
                        (w=>w.x.ActiveStatus && w.y.ActiveStatus && w.y.CodeType==codeType)
                   .Select(r => new DO_ApplicationCodes
                   {
                       ApplicationCode = r.x.UserRole,
                       CodeDesc = r.y.CodeDesc
                   }).OrderBy(x=>x.CodeDesc).ToList();
                    var Distroles = ds.GroupBy(x => new { x.ApplicationCode }).Select(g => g.First()).ToList();

                    return Distroles.ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_UserRoleActionLink>> GetActionsByUserGroup(int userRole)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEuusrls.Join
                        (db.GtEcfmacs,
                        x => new { x.ActionId },
                        y => new { y.ActionId },
                        (x, y) => new { x, y })
                        .Where(w => w.x.ActiveStatus && w.y.ActiveStatus && w.x.UserRole == userRole)
                   .Select(r => new DO_UserRoleActionLink
                   {
                       ActionId = r.x.ActionId,
                       UserRole=r.x.UserRole,
                       ActionDesc = r.y.ActionDesc,
                       ActiveStatus=r.x.ActiveStatus
                   }).ToList();
                    var Distactions = ds.GroupBy(x => new { x.ActionId,x.UserRole }).Select(g => g.First()).ToList();

                    return Distactions.ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ConfigureMenu> GetUserRoleMenulist(int UserGroup,  short UserRole, int BusinessKey)
        {
            try
            {
               
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    DO_ConfigureMenu mn = new DO_ConfigureMenu();

                    mn.l_MainMenu = await db.GtEcbsmns.Join(db.GtEcmnfls,
                            lkey => new { lkey.MenuKey },
                            ent => new { ent.MenuKey },
                            (lkey, ent) => new { lkey, ent })
                            .Join(db.GtEcmamns,
                        Bloc => new { Bloc.ent.MainMenuId },
                        seg => new { seg.MainMenuId },
                        (Bloc, seg) => new { Bloc, seg })
                        .Where(x => x.Bloc.lkey.BusinessKey == BusinessKey && x.Bloc.lkey.ActiveStatus == true)
                                    .Select(m => new DO_MainMenu()
                                    {
                                        MainMenuId = m.Bloc.ent.MainMenuId,
                                        MainMenu = m.seg.MainMenu,
                                        MenuIndex = m.seg.MenuIndex
                                    }).Distinct().ToListAsync();

                   
                    mn.l_SubMenu = await db.GtEcbsmns.Join(db.GtEcmnfls,
                            lkey => new { lkey.MenuKey },
                            ent => new { ent.MenuKey },
                            (lkey, ent) => new { lkey, ent })
                            .Join(db.GtEcsbmns,
                        Bloc => new { Bloc.ent.MainMenuId, Bloc.ent.MenuItemId },
                        seg => new { seg.MainMenuId, seg.MenuItemId },
                        (Bloc, seg) => new { Bloc, seg })
                        .Where(x => x.Bloc.lkey.BusinessKey == BusinessKey && x.Bloc.lkey.ActiveStatus == true)
                                    .Select(f => new DO_SubMenu()
                                    {
                                        MainMenuId = f.Bloc.ent.MainMenuId,
                                        MenuItemId = f.Bloc.ent.MenuItemId,
                                        MenuItemName = f.seg.MenuItemName,
                                        MenuIndex = f.seg.MenuIndex,
                                        ParentID = f.seg.ParentId
                                    }).Distinct().ToListAsync();

                    mn.l_FormMenu = await db.GtEcbsmns.Join(db.GtEcmnfls,
                           lkey => new { lkey.MenuKey },
                           ent => new { ent.MenuKey },
                           (lkey, ent) => new { lkey, ent })
                       .Where(x => x.lkey.BusinessKey == BusinessKey && x.lkey.ActiveStatus == true)
                        .GroupJoin(db.GtEuusgrs.Where(x => x.BusinessKey == BusinessKey && x.UserGroup == UserGroup
                        && x.UserRole == UserRole),
                        m => new { m.ent.MenuKey },
                        fm => new { fm.MenuKey },
                        (m, fm) => new { m, fm })
                        .SelectMany(z => z.fm.DefaultIfEmpty(),
                        (a, b) => new DO_FormMenu()
                        {
                            MainMenuId = a.m.ent.MainMenuId,
                            MenuItemId = a.m.ent.MenuItemId,
                            FormId = a.m.ent.MenuKey,
                            FormNameClient = a.m.ent.FormNameClient,
                            FormIndex = a.m.ent.FormIndex,
                            ActiveStatus = b == null ? false : b.ActiveStatus
                        }).ToListAsync();
                                  

                    //mn.l_FormMenu = await db.GtEcbsmns.Join(db.GtEcmnfls,
                    //        lkey => new { lkey.MenuKey },
                    //        ent => new { ent.MenuKey },
                    //        (lkey, ent) => new { lkey, ent })
                    //    .Where(x => x.lkey.BusinessKey == BusinessKey && x.lkey.ActiveStatus == true)
                    //                .Select(f => new DO_FormMenu()
                    //                {
                    //                    MainMenuId = f.ent.MainMenuId,
                    //                    MenuItemId = f.ent.MenuItemId,
                    //                    FormId = f.ent.MenuKey,
                    //                    FormNameClient = f.ent.FormNameClient,
                    //                    FormIndex = f.ent.FormIndex
                    //                }).ToListAsync();

                    //foreach (var obj in mn.l_FormMenu)
                    //{
                    //    GtEuusgr getlocDesc = db.GtEuusgrs.Where(c => c.BusinessKey == BusinessKey && c.UserGroup == UserGroup && c.UserRole== UserRole && c.MenuKey == obj.FormId).FirstOrDefault();
                    //    if (getlocDesc != null)
                    //    {
                    //        obj.ActiveStatus = getlocDesc.ActiveStatus;
                    //    }
                    //    else
                    //    {
                    //        obj.ActiveStatus = false;
                    //    }
                    //}
                    return mn;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertOrUpdateUserRoleMenuLink(List<DO_UserGroupRole> obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var _link in obj)
                        {
                            var _linkExist = db.GtEuusgrs.Where(w => w.MenuKey == _link.MenuKey && w.BusinessKey == _link.BusinessKey
                            && w.UserGroup==_link.UserGroup && w.UserRole==_link.UserRole).FirstOrDefault();
                            if (_linkExist != null)
                            {
                                if (_linkExist.ActiveStatus != _link.ActiveStatus)
                                {
                                    _linkExist.ActiveStatus = _link.ActiveStatus;
                                    _linkExist.ModifiedBy = _link.UserID;
                                    _linkExist.ModifiedOn = System.DateTime.Now;
                                    _linkExist.ModifiedTerminal = _link.TerminalID;
                                }

                            }
                            else
                            {
                                if (_link.ActiveStatus)
                                {
                                    var _rolelink = new GtEuusgr
                                    {
                                        BusinessKey = _link.BusinessKey,
                                        UserGroup=_link.UserGroup,
                                        UserRole=_link.UserRole,
                                        MenuKey = _link.MenuKey,
                                        ActiveStatus = _link.ActiveStatus,
                                        FormId=_link.FormID,
                                        CreatedBy = _link.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = _link.TerminalID
                                    };
                                    db.GtEuusgrs.Add(_rolelink);
                                }

                            }
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };

                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }
            }
        }
        #endregion

        #region Link Action to User Role 

        public async Task<List<DO_ApplicationCodes>> GetUserRoleByCodeType(int codeType)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var ds = db.GtEcapcds
                   .Where(w => w.CodeType == codeType && w.ActiveStatus == true)
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
        public async Task<List<DO_UserRoleActionLink>> GetUserRoleActionLink(int userRole)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var ds = await db.GtEcfmacs.Where(x => x.ActiveStatus == true)
                   .GroupJoin(db.GtEuusrls.Where(w => w.UserRole == userRole),
                     d => d.ActionId,
                     l => l.ActionId,
                    (act, rol) => new { act, rol })
                   .SelectMany(z => z.rol.DefaultIfEmpty(),
                    (a, b) => new DO_UserRoleActionLink
                    {
                        ActionId = a.act.ActionId,
                        ActionDesc = a.act.ActionDesc,
                        UserRole = b == null ? 0 : b.UserRole,
                        ActiveStatus = b == null ? false : b.ActiveStatus
                    }).ToListAsync();

                    return ds;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertOrUpdateUserRoleActionLink(List<DO_UserRoleActionLink> obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var _role in obj)
                        {
                            var roleExist = db.GtEuusrls.Where(w => w.UserRole == _role.UserRole && w.ActionId == _role.ActionId).FirstOrDefault();
                            if (roleExist != null)
                            {
                                db.GtEuusrls.Remove(roleExist);
                                await db.SaveChangesAsync();
                            }
                        }
                        foreach (var _role in obj)
                        {
                            if (_role.ActiveStatus == true)
                            {
                                var userrolelink = new GtEuusrl
                                {
                                    UserRole = _role.UserRole,
                                    ActionId = _role.ActionId,
                                    ActiveStatus = _role.ActiveStatus,
                                    FormId = _role.FormID,
                                    CreatedBy = _role.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = _role.TerminalID
                                };
                                db.GtEuusrls.Add(userrolelink);
                                await db.SaveChangesAsync();
                            }
                        }


                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }
            }
        }
        #endregion

        #region User Creation

        #region Tab-1
        public async Task<List<DO_UserMaster>> GetUserMaster()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEuusms
                        .Select(r => new DO_UserMaster
                        {
                            UserID = r.UserId,
                            LoginID = r.LoginId,
                            LoginDesc = r.LoginDesc,
                            EMailId = r.EMailId,
                            BlockSignIn=r.BlockSignIn,
                            IsUserAuthenticated=r.IsUserAuthenticated,
                            IsUserDeactivated=r.IsUserDeactivated,
                            ActiveStatus = r.ActiveStatus,
                            //ISDCode = r.Isdcode ?? 0,
                        }).OrderBy(o => o.LoginDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_UserMaster> GetUserDetails(int UserID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEuusms
                        .Where(w => w.UserId == UserID)
                        .Select(r => new DO_UserMaster
                        {
                            UserID=r.UserId,
                            LoginID = r.LoginId,
                            LoginDesc = r.LoginDesc,
                            Photo = r.Photo,
                            PhotoUrl=r.PhotoUrl,
                            EMailId=r.EMailId,
                            CreatePasswordInNextSignIn=r.CreatePasswordInNextSignIn,
                            UnsuccessfulAttempt=r.UnsuccessfulAttempt,
                            LoginAttemptDate=r.LoginAttemptDate,
                            BlockSignIn=r.BlockSignIn,
                            LastPasswordUpdatedDate=r.LastPasswordUpdatedDate,
                            LastActivityDate=r.LastActivityDate,
                            IsUserAuthenticated=r.IsUserAuthenticated,
                            UserAuthenticatedDate=r.UserAuthenticatedDate,
                            IsUserDeactivated=r.IsUserDeactivated,
                            UserDeactivatedOn=r.UserDeactivatedOn,
                            DeactivationReason=r.DeactivationReason,
                            ActiveStatus = r.ActiveStatus,
                        }).FirstOrDefaultAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_eSyaParameter>> GetUserParameters(int UserID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEuuspas
                        .Where(w => w.UserId == UserID)
                        .Select(p => new DO_eSyaParameter
                        {
                            ParameterID = p.ParameterId,
                            ParmAction = p.ParmAction,
                            ParmPerc = p.ParmPerc,
                            ParmDesc = p.ParmDesc,
                            ParmValue = p.ParmValue,
                            ActiveStatus = p.ActiveStatus,
                        }).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertIntoUserMaster(DO_UserMaster obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var _isLoginIdExist = db.GtEuusms.Where(w => w.LoginId == obj.LoginID).Count();
                        if (_isLoginIdExist > 0)
                        {

                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0111", Message = string.Format(_localizer[name: "W0111"]) };
                        }

                        var _isEmaiExist = db.GtEuusms.Where(w => w.EMailId == obj.EMailId).Count();
                        if (_isEmaiExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0127", Message = string.Format(_localizer[name: "W0127"]) };
                        }

                        int _userId = 0;

                        int maxUserId = db.GtEuusms.Select(c => c.UserId).DefaultIfEmpty().Max();
                        _userId = maxUserId + 1;

                        var ap_cd = new GtEuusm
                        {
                            UserId = _userId,
                            LoginId = obj.LoginID,
                            LoginDesc = obj.LoginDesc,
                            Photo = obj.Photo,
                            PhotoUrl = null,
                            EMailId = obj.EMailId,
                            UserCreatedOn=System.DateTime.Now,
                            FirstUseByUser=null,
                            CreatePasswordInNextSignIn = true,
                            UnsuccessfulAttempt=0,
                            LoginAttemptDate=null,
                            BlockSignIn=false,
                            LastPasswordUpdatedDate = null,
                            LastActivityDate = null,
                            IsUserAuthenticated = false,
                            UserAuthenticatedDate = null,
                            IsUserDeactivated = false,
                            UserDeactivatedOn = null,
                            DeactivationReason=null,
                            ActiveStatus = true,
                            FormId = obj.FormID,
                            CreatedBy = obj.CreatedBy,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID,
                        };
                        db.GtEuusms.Add(ap_cd);
                        foreach (DO_eSyaParameter up in obj.l_userparameter)
                        {
                            var _uparams = new GtEuuspa
                            {
                                UserId = _userId,
                                ParameterId = up.ParameterID,
                                ParmPerc = up.ParmPerc,
                                ParmAction = up.ParmAction,
                                ParmDesc = string.IsNullOrEmpty(up.ParmDesc) ? "-": up.ParmDesc,
                                ParmValue = up.ParmValue,
                                ActiveStatus = up.ActiveStatus,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID,
                            };
                            db.GtEuuspas.Add(_uparams);

                        }
                        await db.SaveChangesAsync();
                        CreateOTPforUserLogin(_userId);
                        dbContext.Commit();
                      
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]), ID = _userId };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> UpdateUserMaster(DO_UserMaster obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEuusm _user = db.GtEuusms.Where(w => w.UserId == obj.UserID).FirstOrDefault();
                        if (_user == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
                        }
                        var _isEmaiExist = db.GtEuusms.Where(w => w.UserId != obj.UserID && w.EMailId == obj.EMailId).Count();
                        if (_isEmaiExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0129", Message = string.Format(_localizer[name: "W0129"]) };
                        }

                        _user.LoginDesc = obj.LoginDesc;
                        _user.EMailId = obj.EMailId;
                        _user.Photo = obj.Photo;
                        _user.PhotoUrl = null;
                        _user.ModifiedBy = obj.UserID;
                        _user.ModifiedOn = System.DateTime.Now;
                        _user.ModifiedTerminal = obj.TerminalID;

                        foreach (DO_eSyaParameter up in obj.l_userparameter)
                        {
                            var cPar = db.GtEuuspas.Where(x => x.UserId == obj.UserID && x.ParameterId == up.ParameterID).FirstOrDefault();
                            if (cPar != null)
                            {
                                cPar.ParmAction = up.ParmAction;
                                cPar.ParmDesc = string.IsNullOrEmpty(up.ParmDesc) ? "-" : up.ParmDesc;
                                cPar.ParmPerc = up.ParmPerc;
                                cPar.ParmValue = up.ParmValue;
                                cPar.ActiveStatus = obj.ActiveStatus;
                                cPar.ModifiedBy = obj.UserID;
                                cPar.ModifiedOn = System.DateTime.Now;
                                cPar.ModifiedTerminal = obj.TerminalID;
                            }
                            else
                            {
                                var _uparams = new GtEuuspa
                                {
                                    UserId = obj.UserID,
                                    ParameterId = up.ParameterID,
                                    ParmPerc = up.ParmPerc,
                                    ParmAction = up.ParmAction,
                                    ParmDesc = string.IsNullOrEmpty(up.ParmDesc) ? "-" : up.ParmDesc,
                                    ParmValue = up.ParmValue,
                                    ActiveStatus = up.ActiveStatus,
                                    FormId = obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = obj.TerminalID,

                                };
                                db.GtEuuspas.Add(_uparams);
                            }

                        }
                        await db.SaveChangesAsync();
                        //CreateOTPforUserLogin(obj.UserID);
                        dbContext.Commit();
                       
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]), ID = obj.UserID };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region Tab-2
        public int GetStateCodebyBusinessKey(int BusinessKey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    int ISDCode = 0;
                    var ds = db.GtEcbslns.Where(w => w.BusinessKey == BusinessKey && w.ActiveStatus).FirstOrDefault();
                    if (ds != null)
                    {
                        ISDCode = ds.Isdcode;
                    }
                   
                    return  ISDCode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_PreferredCulture>> GetPreferredLanguagebyBusinessKey(int BusinessKey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var ds = await db.GtEcblpls.Where(x =>x.BusinessKey==BusinessKey && x.ActiveStatus == true)
                   .Join(db.GtEbeculs.Where(w => w.ActiveStatus),
                     d => d.PreferredLanguage.ToUpper().Replace(" ", ""),
                     l => l.CultureCode.ToUpper().Replace(" ", ""),
                    (d, l) => new { d, l })
                   .Select(x=> new DO_PreferredCulture
                   {
                        CultureCode = x.d.PreferredLanguage,
                        CultureDescription = x.l.CultureDesc
                        
                    }).Distinct().ToListAsync();

                    return ds;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_UserBusinessLocation>> GetUserBusinessLocationByUserID(int UserID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEuusbls
                        .Where(w => w.UserId == UserID)
                        .Select(u => new DO_UserBusinessLocation
                        {
                            UserID = u.UserId,
                            BusinessKey = u.BusinessKey,
                            AllowMtfy = u.AllowMtfy,
                            PreferredLanguage=u.PreferredLanguage,
                            Isdcode = u.Isdcode,
                            MobileNumber = u.MobileNumber,
                            IsdcodeWan=u.IsdcodeWan,
                            WhatsappNumber=u.WhatsappNumber,
                            ESyaAuthentication=u.ESyaAuthentication,
                            ActiveStatus = u.ActiveStatus,
                        }).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertOrUpdateUserBusinessLocation(DO_UserBusinessLocation obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        
                            var _locExist = db.GtEuusbls.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey).FirstOrDefault();
                            if (_locExist != null)
                            {
                            _locExist.AllowMtfy = obj.AllowMtfy;
                            _locExist.PreferredLanguage = obj.PreferredLanguage;
                            _locExist.Isdcode = obj.Isdcode;
                            _locExist.MobileNumber = obj.MobileNumber;
                            _locExist.IsdcodeWan = obj.IsdcodeWan;
                            _locExist.WhatsappNumber = obj.WhatsappNumber;
                            _locExist.ESyaAuthentication = obj.ESyaAuthentication;
                            _locExist.ActiveStatus = obj.ActiveStatus;
                            _locExist.ModifiedBy = obj.UserID;
                            _locExist.ModifiedOn = System.DateTime.Now;
                            _locExist.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };

                        }
                            else
                            {
                                
                                    var userloc = new GtEuusbl
                                    {
                                        UserId=obj.UserID,
                                        BusinessKey = obj.BusinessKey,
                                        AllowMtfy = obj.AllowMtfy,
                                        PreferredLanguage = obj.PreferredLanguage,
                                        Isdcode = obj.Isdcode,
                                        MobileNumber=obj.MobileNumber,
                                        IsdcodeWan=obj.IsdcodeWan,
                                        WhatsappNumber=obj.WhatsappNumber,
                                        ESyaAuthentication= obj.ESyaAuthentication,
                                        ActiveStatus = obj.ActiveStatus,
                                        FormId = obj.FormID,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID
                                    };
                                    db.GtEuusbls.Add(userloc);
                            }
                        
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }
            }
        }
        #endregion

        #endregion

        #region Map User to User Group
        public async Task<List<DO_UserMaster>> GetActiveUsers()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var ds = db.GtEuusms
                   .Where(w => w.ActiveStatus == true)
                   .Select(r => new DO_UserMaster
                   {
                        UserID= r.UserId,
                       LoginDesc = r.LoginDesc
                   }).OrderBy(o => o.LoginDesc).ToListAsync();
                    return await ds;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public  List<DO_MapUsertoUserGroup> GetMappedUserGroupByUserID(int UserID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var defaultDate = DateTime.Now.Date;
                    var result = db.GtEuusbls.Where(x => x.UserId == UserID && x.ActiveStatus)
                        .Join(db.GtEuusgrs.Where(x => x.ActiveStatus),
                        bus => new { bus.BusinessKey },
                        bgl => new { bgl.BusinessKey },
                        (bus, bgl) => new { bus, bgl })
                        .Join(db.GtEcbslns.Where(x => x.ActiveStatus),
                        mu => new { mu.bgl.BusinessKey, },
                        b => new { b.BusinessKey },
                        (mu, b) => new { mu, b })
                        .Join(db.GtEcapcds.Where(x => x.ActiveStatus),
                        mg => new { mg.mu.bgl.UserGroup, },
                        g => new { UserGroup = g.ApplicationCode },
                        (mg, g) => new { mg, g })
                        .Join(db.GtEcapcds.Where(x => x.ActiveStatus),
                        mr => new { mr.mg.mu.bgl.UserRole },
                        r => new { UserRole = r.ApplicationCode },
                        (mr, r) => new { mr, r })
                        .GroupJoin(db.GtEuubgrs.Where(w => w.UserId == UserID).OrderByDescending(o => o.ActiveStatus),
                        mus => new { mus.mr.mg.mu.bgl.UserGroup, mus.mr.mg.mu.bgl.BusinessKey, mus.mr.mg.mu.bgl.UserRole },
                        mr => new { mr.UserGroup, mr.BusinessKey, mr.UserRole },
                        (mus, mr) => new { mus, mr })
                        .SelectMany(z => z.mr.DefaultIfEmpty(),
                               (a, d) => new DO_MapUsertoUserGroup
                               {
                                   BusinessKey = a.mus.mr.mg.mu.bus.BusinessKey,
                                   UserGroup = a.mus.mr.mg.mu.bgl.UserGroup,
                                   UserRole = a.mus.mr.mg.mu.bgl.UserRole,
                                   LocationDesc = a.mus.mr.mg.b.BusinessName + "-" + a.mus.mr.mg.b.LocationDescription,
                                   UserGroupDesc = a.mus.mr.g.CodeDesc,
                                   UserRoleDesc = a.mus.r.CodeDesc,
                                   UserID = d != null ? d.UserId : UserID,
                                   EffectiveFrom = d != null ? d.EffectiveFrom : defaultDate,
                                   EffectiveTill = d != null ? d.EffectiveTill : null,
                                   ActiveStatus = d != null ? d.ActiveStatus : false,
                               }
                        ).ToList();
                  
                    var uniqueCollection = result
                    .GroupBy(x => new { x.BusinessKey, x.UserGroup, x.UserRole })
                    .Select(group => group.First())
                    .ToList();
                    return uniqueCollection;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertOrUpdateUserGroupMappedwithUser(DO_MapUsertoUserGroup obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        var _usermapped = db.GtEuubgrs.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey && w.UserGroup == obj.UserGroup
                        && w.UserRole == obj.UserRole && w.EffectiveFrom == obj.EffectiveFrom).FirstOrDefault();
                        if (_usermapped != null)
                        {
                            _usermapped.EffectiveTill = obj.EffectiveTill;
                            _usermapped.ActiveStatus = obj.ActiveStatus;
                            _usermapped.ModifiedBy = obj.UserID;
                            _usermapped.ModifiedOn = System.DateTime.Now;
                            _usermapped.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };

                        }
                        else
                        {

                            var usermap = new GtEuubgr
                            {
                                UserId = obj.UserID,
                                BusinessKey = obj.BusinessKey,
                                UserGroup = obj.UserGroup,
                                UserRole = obj.UserRole,
                                EffectiveFrom = obj.EffectiveFrom,
                                EffectiveTill = obj.EffectiveTill,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtEuubgrs.Add(usermap);
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }
            }
        }

        public async Task<DO_ConfigureMenu> GetMappedUserRoleMenulist(int UserGroup, short UserRole, int BusinessKey)
        {
            try
            {

                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    DO_ConfigureMenu mn = new DO_ConfigureMenu();

                    mn.l_MainMenu = await db.GtEuusgrs.Join(db.GtEcmnfls,
                            lkey => new { lkey.MenuKey },
                            ent => new { ent.MenuKey },
                            (lkey, ent) => new { lkey, ent })
                            .Join(db.GtEcmamns,
                        Bloc => new { Bloc.ent.MainMenuId },
                        seg => new { seg.MainMenuId },
                        (Bloc, seg) => new { Bloc, seg })
                        .Where(x => x.Bloc.lkey.BusinessKey == BusinessKey && x.Bloc.lkey.UserRole == UserRole && x.Bloc.lkey.UserGroup == UserGroup && x.Bloc.lkey.ActiveStatus == true)
                                    .Select(m => new DO_MainMenu()
                                    {
                                        MainMenuId = m.Bloc.ent.MainMenuId,
                                        MainMenu = m.seg.MainMenu,
                                        MenuIndex = m.seg.MenuIndex
                                    }).Distinct().ToListAsync();


                    mn.l_SubMenu = await db.GtEuusgrs.Join(db.GtEcmnfls,
                            lkey => new { lkey.MenuKey },
                            ent => new { ent.MenuKey },
                            (lkey, ent) => new { lkey, ent })
                            .Join(db.GtEcsbmns,
                        Bloc => new { Bloc.ent.MainMenuId, Bloc.ent.MenuItemId },
                        seg => new { seg.MainMenuId, seg.MenuItemId },
                        (Bloc, seg) => new { Bloc, seg })
                        .Where(x => x.Bloc.lkey.BusinessKey == BusinessKey && x.Bloc.lkey.UserRole == UserRole && x.Bloc.lkey.UserGroup == UserGroup && x.Bloc.lkey.ActiveStatus == true)
                                    .Select(f => new DO_SubMenu()
                                    {
                                        MainMenuId = f.Bloc.ent.MainMenuId,
                                        MenuItemId = f.Bloc.ent.MenuItemId,
                                        MenuItemName = f.seg.MenuItemName,
                                        MenuIndex = f.seg.MenuIndex,
                                        ParentID = f.seg.ParentId
                                    }).Distinct().ToListAsync();

                    mn.l_FormMenu = await db.GtEuusgrs.Join(db.GtEcmnfls,
                            lkey => new { lkey.MenuKey },
                            ent => new { ent.MenuKey },
                            (lkey, ent) => new { lkey, ent })
                        .Where(x => x.lkey.BusinessKey == BusinessKey && x.lkey.UserRole == UserRole && x.lkey.UserGroup == UserGroup && x.lkey.ActiveStatus == true)
                                    .Select(f => new DO_FormMenu()
                                    {
                                        MainMenuId = f.ent.MainMenuId,
                                        MenuItemId = f.ent.MenuItemId,
                                        FormId = f.ent.MenuKey,
                                        FormNameClient = f.ent.FormNameClient,
                                        FormIndex = f.ent.FormIndex
                                    }).ToListAsync();

                    foreach (var obj in mn.l_FormMenu)
                    {
                        GtEuusgr getlocDesc = db.GtEuusgrs.Where(c => c.BusinessKey == BusinessKey && c.UserGroup == UserGroup && c.UserRole == UserRole && c.MenuKey == obj.FormId).FirstOrDefault();
                        if (getlocDesc != null)
                        {
                            obj.ActiveStatus = getlocDesc.ActiveStatus;
                        }
                        else
                        {
                            obj.ActiveStatus = false;
                        }
                    }
                    return mn;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region User Photo
        public async Task<List<DO_UserMaster>> GetActiveUsersforPhoto()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEuusms.Where(x=>x.ActiveStatus)
                        .Select(r => new DO_UserMaster
                        {
                            UserID = r.UserId,
                            LoginID = r.LoginId,
                            LoginDesc = r.LoginDesc,
                            EMailId = r.EMailId,
                            Photo=r.Photo,
                            PhotoUrl=r.PhotoUrl,
                            IsUserAuthenticated = r.IsUserAuthenticated,
                            IsUserDeactivated = r.IsUserDeactivated,
                            ActiveStatus = r.ActiveStatus,
                        }).OrderBy(o => o.LoginDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_UserPhoto> GetUserPhotobyUserID(int UserID)

        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEuusms.Where(x=>x.ActiveStatus)
                        .Where(w => w.UserId == UserID)
                        .Select(u => new DO_UserPhoto
                        {
                            UserID = u.UserId,
                             Photo= u.Photo,
                            PhotoUrl = u.PhotoUrl
                            
                        }).FirstOrDefaultAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> UploadUserPhoto(DO_UserPhoto obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEuusm _photo = db.GtEuusms.Where(w => w.UserId == obj.UserID).FirstOrDefault();
                        if (_photo == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
                        }
                        _photo.Photo = obj.Photo;
                        _photo.PhotoUrl = null;
                        _photo.ModifiedBy = obj.UserID;
                        _photo.ModifiedOn = System.DateTime.Now;
                        _photo.ModifiedTerminal = obj.TerminalID;
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0008", Message = string.Format(_localizer[name: "S0008"])};
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #endregion

        #region Change Password
        public async Task<DO_ReturnParameter> ChangeUserPassword(DO_ChangePassword obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try {
                        
                        var user = await db.GtEuusms
                            .Join(db.GtEuuspws,
                                u => new { u.UserId },
                                up => new { up.UserId },
                                (u, up) => new { u, up }).Where(x => x.u.UserId == obj.userID && x.u.ActiveStatus && x.up.UserId == obj.userID)
                                .Select(m => new DO_UserPassword
                                {
                                    UserID = m.u.UserId,
                                    EPasswd = m.up.EPasswd
                                }).FirstOrDefaultAsync();


                        if (user != null)
                        {
                            string existingpassword = string.Empty;
                            string olduserpassword = string.Empty;
                            if (user.EPasswd.Length != 0)
                            {
                                existingpassword = eSyaCryptGeneration.Decrypt(Encoding.UTF8.GetString(user.EPasswd));

                                //existingpassword = eSyaCryptGeneration.Decrypt(Convert.ToBase64String(user.EPasswd));

                               
                                Byte[] oldpasswordbitmapData = Encoding.UTF8.GetBytes(eSyaCryptGeneration.Encrypt(obj.oldpassword));
                                olduserpassword = eSyaCryptGeneration.Decrypt(Encoding.UTF8.GetString(oldpasswordbitmapData));

                                if (existingpassword != olduserpassword)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0206", Message = string.Format(_localizer[name: "W0206"]) };
                                }
                               
                                else
                                {
                                    var usermaster = db.GtEuusms.Where(x => x.UserId == obj.userID).FirstOrDefault();
                                    var passwordmaster = db.GtEuuspws.Where(x => x.UserId == obj.userID).FirstOrDefault();

                                    var pass_history = db.GtEuusphs.Where(x => x.UserId == obj.userID).Select
                                        (x => new DO_ChangePassword
                                        {
                                            newPassword = eSyaCryptGeneration.Decrypt(Encoding.UTF8.GetString(x.EPasswd))
                                        }).ToList();
                                    if (pass_history != null)
                                    {
                                        foreach (var p in pass_history)
                                        {
                                            if (p.newPassword == obj.newPassword)
                                            {
                                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0209", Message = string.Format(_localizer[name: "W0209"]) };
                                            }
                                        }
                                    }

                                    if (usermaster != null && passwordmaster != null)
                                    {
                                        usermaster.LastPasswordUpdatedDate = DateTime.Now;
                                        usermaster.LastActivityDate = System.DateTime.Now;
                                        await db.SaveChangesAsync();
                                    }


                                   
                                    passwordmaster.EPasswd = Encoding.UTF8.GetBytes(eSyaCryptGeneration.Encrypt(obj.newPassword));
                                    passwordmaster.LastPasswdDate = DateTime.Now;
                                    await db.SaveChangesAsync();
                                    var serialno = db.GtEuusphs.Select(x => x.SerialNumber).DefaultIfEmpty().Max() + 1;
                                    var passhistory = new GtEuusph
                                    {
                                        UserId = obj.userID,
                                        SerialNumber = serialno,
                                        EPasswd = Encoding.UTF8.GetBytes(eSyaCryptGeneration.Encrypt(obj.newPassword)),
                                        LastPasswdChangedDate = DateTime.Now,
                                        ActiveStatus = true,
                                        FormId = obj.FormID,
                                        CreatedBy = obj.CreatedBy,
                                        CreatedOn = DateTime.Now,
                                        CreatedTerminal = obj.TerminalID
                                    };
                                    db.GtEuusphs.Add(passhistory);
                                    await db.SaveChangesAsync();
                                    dbContext.Commit();
                                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0010", Message = string.Format(_localizer[name: "S0010"]) };

                                }
                            }
                            else
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0207", Message = string.Format(_localizer[name: "W0207"]) };

                            }



                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0208", Message = string.Format(_localizer[name: "W0208"]) };


                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }

            }
        }
        #endregion

        #region Send OTP 

        public void CreateOTPforUserLogin(int userId)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {

                    try
                    {
                        var userOtp = db.GtEuuotps.Where(x => x.UserId == userId).FirstOrDefault();
                        Random rnd = new Random();
                        var OTP = rnd.Next(100000, 999999).ToString();

                        if (userOtp == null)
                        {
                            var lotp = new GtEuuotp()
                            {
                                UserId = userId,
                                Otpnumber = OTP,
                                OtpgeneratedDate = System.DateTime.Now,
                                Otpsource = "User Creation OTP",
                                UsageStatus = false,
                                ActiveStatus = true,
                                FormId = "0",
                                CreatedBy = userId,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = "GTPL"
                            };
                            db.GtEuuotps.Add(lotp);
                            db.SaveChanges();

                        }
                        else
                        {
                            userOtp.Otpsource = "User Creation OTP";
                            userOtp.Otpnumber = OTP;
                            userOtp.UsageStatus = false;
                            userOtp.ActiveStatus = true;
                            userOtp.OtpgeneratedDate = System.DateTime.Now;
                            userOtp.ModifiedBy = userId;
                            userOtp.ModifiedOn = System.DateTime.Now;
                            userOtp.ModifiedTerminal = "GTPL";
                        }
                        db.SaveChanges();
                        dbContext.Commit();
                    }

                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }

                }
            }
        }

        #endregion

        #region It is shifted to Gateway
        public void CreateUserPassword(int _userId)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        byte[] Epass = Encoding.UTF8.GetBytes(eSyaCryptGeneration.Encrypt("Gtpl@123"));
                        string password = string.Empty;
                        password = eSyaCryptGeneration.Decrypt(Encoding.UTF8.GetString(Epass));


                        //olduserpassword = eSyaCryptGeneration.Decrypt(Encoding.UTF8.GetString(oldpasswordbitmapData));

                        var passExist = db.GtEuuspws.Where(x => x.UserId == _userId).FirstOrDefault();
                        if (passExist != null)
                        {
                            passExist.EPasswd = Epass;
                            passExist.LastPasswdDate = DateTime.Now;
                            passExist.ModifiedBy = 0;
                            passExist.ModifiedOn = DateTime.Now;
                            passExist.ModifiedTerminal = "GTPL";
                        }
                        db.SaveChanges();
                        var _pas = new GtEuuspw()
                        {
                            UserId = _userId,
                            EPasswd = Epass,
                            LastPasswdDate = DateTime.Now,
                            ActiveStatus = true,
                            FormId = "0",
                            CreatedBy = _userId,
                            CreatedOn = DateTime.Now,
                            CreatedTerminal = "GTPL"

                        };
                        db.GtEuuspws.Add(_pas);
                        db.SaveChanges();
                        var serialno = db.GtEuusphs.Select(x => x.SerialNumber).DefaultIfEmpty().Max() + 1;
                        var passhistory = new GtEuusph
                        {
                            UserId = _userId,
                            SerialNumber = serialno,
                            EPasswd = Encoding.UTF8.GetBytes(eSyaCryptGeneration.Encrypt(password)),
                            LastPasswdChangedDate = DateTime.Now,
                            ActiveStatus = true,
                            FormId = "0",
                            CreatedBy = _userId,
                            CreatedOn = DateTime.Now,
                            CreatedTerminal = "GTPL"
                        };
                        db.GtEuusphs.Add(passhistory);
                        db.SaveChanges();
                        dbContext.Commit();

                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                    }
                }

            }
        }
        #endregion
    }
}

