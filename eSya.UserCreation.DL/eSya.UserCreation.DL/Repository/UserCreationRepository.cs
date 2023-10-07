using eSya.UserCreation.DL.Entities;
using eSya.UserCreation.DO;
using eSya.UserCreation.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
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


        #region  UserGroup & Type
        public async Task<DO_ConfigureMenu> GetMenulistbyUserGroup(int UserGroup, int UserType, int UserRole)
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
                                        FormNameClient = f.FormNameClient,
                                        FormIndex = f.FormIndex,
                                        //ActiveStatus = f.ActiveStatus,
                                        //FormId = f.FormId,
                                        //MenuKey = f.MenuKey
                                        FormId = f.MenuKey
                                    }).ToListAsync();
                    foreach (var obj in mn.l_FormMenu)
                    {
                        GtEuusgr getlocDesc = db.GtEuusgrs.Where(c => c.UserGroup == UserGroup && c.UserType == UserType && c.UserRole == UserRole && c.MenuKey == obj.FormId).FirstOrDefault();
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

        public async Task<List<DO_UserFormAction>> GetFormActionLinkbyUserGroup(int UserGroup, int UserType, int UserRole, int MenuKey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    GtEcmnfl geformID = db.GtEcmnfls.Where(x => x.MenuKey == MenuKey).FirstOrDefault();
                    int formID = 0;
                    if (geformID != null)
                        formID = geformID.FormId;

                    var result = await db.GtEcfmacs
                    .Join(db.GtEcfmals.Where(w => w.FormId == formID),
                    a => a.ActionId,
                    f => f.ActionId,
                    (a, f) => new { a, f })
                    //(a, f) => new { a, f = f.FirstOrDefault() })
                    .Select(r => new DO_UserFormAction
                    {
                        ActionID = r.a.ActionId,
                        ActionDesc = r.a.ActionDesc,
                        ActiveStatus = r.f != null ? r.f.ActiveStatus : false,
                    }).ToListAsync();
                    if (result.Count > 0)
                    {
                        result = result.GroupBy(x => x.ActionID).Select(g => g.FirstOrDefault()).ToList();
                    }
                    foreach (var obj in result)
                    {
                        GtEuusac actions = db.GtEuusacs.Where(x => x.UserGroup == UserGroup && x.UserType == UserType && x.UserRole == UserRole && x.MenuKey == MenuKey && x.ActionId == obj.ActionID).FirstOrDefault();
                        if (actions != null)
                        {
                            obj.ActiveStatus = actions.ActiveStatus;
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoUserGroupMenuAction(DO_UserGroupRole obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEuusgr ug = await db.GtEuusgrs.Where(w => w.UserGroup == obj.UserGroup && w.UserType == obj.UserType && w.UserRole == obj.UserRole && w.MenuKey == obj.MenuKey).FirstOrDefaultAsync();

                        if (ug != null)
                        {
                            ug.ActiveStatus = obj.ActiveStatus;
                            ug.ModifiedBy = obj.UserId;
                            ug.ModifiedOn = System.DateTime.Now;
                            ug.ModifiedTerminal = obj.TerminalId;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            ug = new GtEuusgr();
                            ug.UserGroup = obj.UserGroup;
                            ug.UserType = obj.UserType;
                            ug.UserRole = obj.UserRole;
                            ug.MenuKey = obj.MenuKey;
                            ug.ActiveStatus = obj.ActiveStatus;
                            ug.FormId = obj.FormId;
                            ug.CreatedBy = obj.UserId;
                            ug.CreatedOn = DateTime.Now;
                            ug.CreatedTerminal = System.Environment.MachineName;
                            db.GtEuusgrs.Add(ug);
                            await db.SaveChangesAsync();
                        }
                        var fa = await db.GtEuusacs.Where(w => w.UserGroup == obj.UserGroup && w.UserType == obj.UserType && w.UserRole == obj.UserRole && w.MenuKey == obj.MenuKey).ToListAsync();

                        foreach (GtEuusac f in fa)
                        {
                            f.ActiveStatus = false;
                            f.ModifiedBy = obj.UserId;
                            f.ModifiedOn = System.DateTime.Now;
                            f.ModifiedTerminal = obj.TerminalId;
                        }
                        await db.SaveChangesAsync();

                        if (obj.l_formAction != null)
                        {
                            foreach (DO_UserFormAction i in obj.l_formAction)
                            {
                                var obj_FA = await db.GtEuusacs.Where(w => w.UserGroup == obj.UserGroup && w.UserType == obj.UserType && w.UserRole == obj.UserRole && w.MenuKey == obj.MenuKey && w.ActionId == i.ActionID).FirstOrDefaultAsync();
                                if (obj_FA != null)
                                {
                                    if (i.Active.Substring(0, 1).ToString() == "Y")
                                        obj_FA.ActiveStatus = true;
                                    else
                                        obj_FA.ActiveStatus = false;
                                    obj_FA.ModifiedBy = obj.UserId;
                                    obj_FA.ModifiedOn = DateTime.Now;
                                    obj_FA.ModifiedTerminal = System.Environment.MachineName;
                                }
                                else
                                {
                                    obj_FA = new GtEuusac();
                                    obj_FA.UserGroup = obj.UserGroup;
                                    obj_FA.UserType = obj.UserType;
                                    obj_FA.UserRole = obj.UserRole;
                                    obj_FA.MenuKey = obj.MenuKey;
                                    obj_FA.ActionId = i.ActionID;
                                    if (i.Active.Substring(0, 1).ToString() == "Y")
                                        obj_FA.ActiveStatus = true;
                                    else
                                        obj_FA.ActiveStatus = false;

                                    obj_FA.FormId = obj.FormId;
                                    obj_FA.CreatedBy = obj.UserId;
                                    obj_FA.CreatedOn = DateTime.Now;
                                    obj_FA.CreatedTerminal = System.Environment.MachineName;
                                    db.GtEuusacs.Add(obj_FA);
                                }
                            }
                            await db.SaveChangesAsync();
                        }

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
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region  User Group & Role
        public async Task<List<DO_UserType>> GetUserTypesbyUserGroup(int usergroup)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var _utypes = await db.GtEuusacs.Where(x => x.UserGroup == usergroup && x.ActiveStatus == true)
                        .Join(db.GtEcapcds,
                         x => x.UserType,
                         y => y.ApplicationCode,
                        (x, y) => new DO_UserType
                        {
                            UserTypeId = x.UserType,
                            UserTypeDesc = y.CodeDesc

                        }).ToListAsync();
                    var _uniqueutypes = _utypes.GroupBy(e => e.UserTypeId).Select(g => g.First());
                    return _uniqueutypes.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_UserRole>> GetUserRolesbyUserType(int usergroup, int usertype)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var _utypes = await db.GtEuusacs.Where(x => x.UserGroup == usergroup && x.UserType == usertype && x.ActiveStatus == true)
                        .Join(db.GtEcapcds,
                         x => x.UserRole,
                         y => y.ApplicationCode,
                        (x, y) => new DO_UserRole
                        {
                            UserRoleId = x.UserRole,
                            UserRoleDesc = y.CodeDesc

                        }).ToListAsync();
                    var _uniqueutypes = _utypes.GroupBy(e => e.UserRoleId).Select(g => g.First());
                    return _uniqueutypes.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_UserRoleMenuLink>> GetUserRoleMenuLinkbyUserId(short UserID, int BusinessKey)

        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtEuusrls.Where(k => k.UserId == UserID && k.BusinessKey == BusinessKey).Join(db.GtEcapcds,
                         x => x.UserGroup,
                         y => y.ApplicationCode,
                         (x, y) => new { x, y }).Join(db.GtEcapcds,
                         a => a.x.UserType,
                         p => p.ApplicationCode, (a, p) => new { a, p }).Join(db.GtEcapcds,
                         b => b.a.x.UserRole,
                         c => c.ApplicationCode, (b, c) => new { b, c }).Select(r => new DO_UserRoleMenuLink
                         {
                             BusinessKey = r.b.a.x.BusinessKey,
                             UserId = r.b.a.x.UserId,
                             UserGroup = r.b.a.x.UserGroup,
                             UserType = r.b.a.x.UserType,
                             UserRole = r.b.a.x.UserRole,
                             EffectiveFrom = r.b.a.x.EffectiveFrom,
                             EffectiveTill = r.b.a.x.EffectiveTill,
                             ActiveStatus = r.b.a.x.ActiveStatus,
                             UserGroupDesc = r.b.a.y.CodeDesc,
                             UserTypeDesc = r.b.p.CodeDesc,
                             UserRoleDesc = r.c.CodeDesc
                         }).ToListAsync();

                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoUserRoleMenuLink(DO_UserRoleMenuLink obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var _isExist = db.GtEuusrls.Where(x => x.UserId == obj.UserId && x.BusinessKey == obj.BusinessKey && x.UserGroup == obj.UserGroup && x.UserRole == obj.UserRole && x.UserType == obj.UserType && x.EffectiveFrom.Date == obj.EffectiveFrom.Date).Count();
                        if (_isExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0125", Message = string.Format(_localizer[name: "W0125"]) };
                        }
                        var role_link = new GtEuusrl
                        {
                            UserId = obj.UserId,
                            BusinessKey = obj.BusinessKey,
                            UserGroup = obj.UserGroup,
                            UserType = obj.UserType,
                            UserRole = obj.UserRole,
                            EffectiveFrom = obj.EffectiveFrom,
                            EffectiveTill = obj.EffectiveTill,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormId,
                            CreatedBy = obj.CreatedBy,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalId,
                        };
                        db.GtEuusrls.Add(role_link);

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
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> UpdateUserRoleMenuLink(DO_UserRoleMenuLink obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var _rolelink = db.GtEuusrls.Where(x => x.UserId == obj.UserId && x.BusinessKey == obj.BusinessKey && x.UserGroup == obj.UserGroup && x.UserRole == obj.UserRole && x.UserType == obj.UserType && x.EffectiveFrom.Date == obj.EffectiveFrom.Date).FirstOrDefault();
                        if (_rolelink != null)
                        {
                            _rolelink.EffectiveTill = obj.EffectiveTill;
                            _rolelink.ActiveStatus = obj.ActiveStatus;
                            _rolelink.ModifiedBy = obj.CreatedBy;
                            _rolelink.ModifiedOn = System.DateTime.Now;
                            _rolelink.ModifiedTerminal = obj.TerminalId;
                            await db.SaveChangesAsync();
                            dbContext.Commit();

                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0126", Message = string.Format(_localizer[name: "W0126"]) };
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
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region User Creation
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
                            ISDCode = r.Isdcode ?? 0,
                            MobileNumber = r.MobileNumber,
                            LastActivityDate = (DateTime)r.LastActivityDate,
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
                            LoginID = r.LoginId,
                            LoginDesc = r.LoginDesc,
                            ISDCode = r.Isdcode ?? 0,
                            MobileNumber = r.MobileNumber,
                            AllowMobileLogin = (bool)r.AllowMobileLogin,
                            IsApprover = r.IsApprover,
                            eMailID = r.EMailId,
                            Photo = r.Photo,
                            Password = eSyaCryptGeneration.Decrypt(r.Password),
                            DigitalSignature = r.DigitalSignature,
                            IsDoctor = r.IsDoctor,
                            DoctorId = r.DoctorId,
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

                        var _isMobileNoExist = db.GtEuusms.Where(w => w.MobileNumber == obj.MobileNumber).Count();
                        if (_isMobileNoExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0118", Message = string.Format(_localizer[name: "W0118"]) };
                        }

                        var _isEmaiExist = db.GtEuusms.Where(w => w.EMailId == obj.eMailID).Count();
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
                            Password = eSyaCryptGeneration.Encrypt(obj.Password),
                            Isdcode = obj.ISDCode,
                            MobileNumber = obj.MobileNumber,
                            AllowMobileLogin = obj.AllowMobileLogin,
                            IsApprover = obj.IsApprover,
                            EMailId = obj.eMailID,
                            Photo = obj.Photo,
                            PhotoUrl = null,
                            DigitalSignature = obj.DigitalSignature,
                            LastPasswordChangeDate = null,
                            LastActivityDate = null,
                            Otpnumber = null,
                            OtpgeneratedDate = null,
                            IsUserAuthenticated = false,
                            UserAuthenticatedDate = null,
                            IsUserDeactivated = false,
                            UserDeactivatedOn = null,
                            IsDoctor = obj.IsDoctor,
                            DoctorId = obj.DoctorId,
                            ActiveStatus = false,
                            FormId = obj.FormId,
                            CreatedBy = obj.CreatedBy,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalId,
                        };
                        db.GtEuusms.Add(ap_cd);

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true,StatusCode= "S0001", Message = string.Format(_localizer[name: "S0001"]), ID = _userId };
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
                        GtEuusm ap_cd = db.GtEuusms.Where(w => w.UserId == obj.UserID).FirstOrDefault();
                        if (ap_cd == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
                        }

                        var _isMobileNoExist = db.GtEuusms.Where(w => w.UserId != obj.UserID && w.MobileNumber == obj.MobileNumber).Count();
                        if (_isMobileNoExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0128", Message = string.Format(_localizer[name: "W0128"]) };
                        }

                        var _isEmaiExist = db.GtEuusms.Where(w => w.UserId != obj.UserID && w.EMailId == obj.eMailID).Count();
                        if (_isEmaiExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0129", Message = string.Format(_localizer[name: "W0129"]) };
                        }

                        ap_cd.LoginDesc = obj.LoginDesc;
                        ap_cd.Isdcode = obj.ISDCode;
                        ap_cd.MobileNumber = obj.MobileNumber;
                        ap_cd.AllowMobileLogin = obj.AllowMobileLogin;
                        ap_cd.IsApprover = obj.IsApprover;
                        ap_cd.EMailId = obj.eMailID;
                        ap_cd.Photo = obj.Photo;
                        ap_cd.PhotoUrl = null;
                        ap_cd.DigitalSignature = obj.DigitalSignature;
                        ap_cd.IsDoctor = obj.IsDoctor;
                        ap_cd.DoctorId = obj.DoctorId;
                        ap_cd.ModifiedBy = obj.UserID;
                        ap_cd.ModifiedOn = System.DateTime.Now;
                        ap_cd.ModifiedTerminal = obj.TerminalId;

                        await db.SaveChangesAsync();

                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true,StatusCode= "S0002", Message = string.Format(_localizer[name: "S0002"]), ID = obj.UserID };
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

        public async Task<List<DO_UserBusinessLink>> GetUserBusinessLocation(short UserID, int CodeTypeUG, int CodeTypeUT)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var result = await db.GtEcbslns
                        .Join(db.GtEcbsens,
                            lkey => new { lkey.BusinessId },
                            ent => new { ent.BusinessId },
                            (lkey, ent) => new { lkey, ent })
                        .Where(x =>x.ent.ActiveStatus && x.lkey.ActiveStatus)
                        .Select(c => new DO_UserBusinessLink
                        {
                            BusinessKey = c.lkey.BusinessKey,
                            LocationDescription = c.lkey.LocationDescription,

                        }).ToListAsync();
                    foreach (var obj in result)
                    {
                        if (UserID != 0)
                        {
                            GtEuusbl isBusinessSegment = await db.GtEuusbls
                                .Where(c => c.UserId == UserID && c.BusinessKey == obj.BusinessKey).FirstOrDefaultAsync();
                            if (isBusinessSegment != null)
                            {
                                obj.UserGroup = isBusinessSegment.UserGroup.Value;
                                obj.IUStatus = 1;
                                GtEcapcd UserGroupDescription = await db.GtEcapcds
                                .Where(c => c.CodeType == CodeTypeUG && c.ApplicationCode == obj.UserGroup).FirstOrDefaultAsync();

                                if (UserGroupDescription != null)
                                    obj.UserGroupDesc = UserGroupDescription.CodeDesc;

                                obj.UserType = isBusinessSegment.UserType.Value;

                                GtEcapcd UserTypeDescription = await db.GtEcapcds
                                .Where(c => c.CodeType == CodeTypeUT && c.ApplicationCode == obj.UserType).FirstOrDefaultAsync();

                                if (UserTypeDescription != null)
                                    obj.UserTypeDesc = UserTypeDescription.CodeDesc;

                                obj.AllowMTFY = isBusinessSegment.AllowMtfy;
                                obj.ActiveStatus = isBusinessSegment.ActiveStatus;
                            }
                            else
                            {
                                //obj.UserGroup = 0;
                                //obj.UserGroupDesc = null;
                                //obj.UserType = 0;
                                //obj.UserTypeDesc = null;
                                obj.IUStatus = 0;
                                obj.AllowMTFY = false;
                                obj.ActiveStatus = false;
                            }
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoUserBL(DO_UserBusinessLink obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var ap_cd = new GtEuusbl
                        {
                            UserId = obj.UserID,
                            BusinessKey = obj.BusinessKey,
                            UserGroup = obj.UserGroup,
                            UserType = obj.UserType,
                            AllowMtfy = obj.AllowMTFY,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormId,
                            CreatedBy = obj.CreatedBy,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalId,
                        };
                        db.GtEuusbls.Add(ap_cd);
                        await db.SaveChangesAsync();

                        //Insert Default Record in User Menu Link
                        var MenuKey = await db.GtEuusgrs.Where(x => x.UserGroup == obj.UserGroup && x.UserType == obj.UserType && x.ActiveStatus == true).ToListAsync();
                        foreach (var mkey in MenuKey)
                        {
                            GtEuusml userMenuLink = new GtEuusml();
                            userMenuLink.UserId = obj.UserID;
                            userMenuLink.BusinessKey = obj.BusinessKey;
                            userMenuLink.MenuKey = mkey.MenuKey;
                            userMenuLink.ActiveStatus = obj.ActiveStatus;
                            userMenuLink.FormId = obj.FormId;
                            userMenuLink.CreatedBy = obj.CreatedBy;
                            userMenuLink.CreatedOn = System.DateTime.Now;
                            userMenuLink.CreatedTerminal = obj.TerminalId;
                            db.GtEuusmls.Add(userMenuLink);
                            await db.SaveChangesAsync();
                        }

                        //Insert Default Record in User Menu Action Link
                        var MenuActionLink = await db.GtEcmnfls.Join(db.GtEcfmals, u => u.FormId, uir => uir.FormId,
                                (u, uir) => new { u, uir }).
                                Join(db.GtEuusgrs, r => r.u.MenuKey, ro => ro.MenuKey, (r, ro) => new { r, ro })
                                .Where(m => m.ro.UserGroup == obj.UserGroup && m.ro.UserType == obj.UserType)
                                .Select(m => new DO_UserFormAction
                                {
                                    MenuKey = m.ro.MenuKey,
                                    ActionID = m.r.uir.ActionId
                                }).ToListAsync();

                        foreach (var makey in MenuActionLink)
                        {
                            GtEuusfa userMenuActionLink = new GtEuusfa();
                            userMenuActionLink.UserId = obj.UserID;
                            userMenuActionLink.BusinessKey = obj.BusinessKey;
                            userMenuActionLink.MenuKey = makey.MenuKey;
                            userMenuActionLink.ActionId = makey.ActionID;
                            userMenuActionLink.ActiveStatus = obj.ActiveStatus;
                            userMenuActionLink.FormId = obj.FormId;
                            userMenuActionLink.CreatedBy = obj.CreatedBy;
                            userMenuActionLink.CreatedOn = System.DateTime.Now;
                            userMenuActionLink.CreatedTerminal = obj.TerminalId;
                            db.GtEuusfas.Add(userMenuActionLink);
                            await db.SaveChangesAsync();
                        }

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
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> UpdateUserBL(DO_UserBusinessLink obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEuusbl ap_cd = await db.GtEuusbls.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey).FirstOrDefaultAsync();
                        if (ap_cd == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0126", Message = string.Format(_localizer[name: "W0126"]) };
                        }

                        if (ap_cd.UserGroup != obj.UserGroup || ap_cd.UserType != obj.UserType)
                        {
                            var u_fal = await db.GtEuusfas.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey).ToListAsync();
                            if (u_fal != null)
                            {
                                db.GtEuusfas.RemoveRange(u_fal);
                                await db.SaveChangesAsync();
                            }

                            var um_lnk = await db.GtEuusmls.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey).ToListAsync();
                            if (um_lnk != null)
                            {
                                db.GtEuusmls.RemoveRange(um_lnk);
                                await db.SaveChangesAsync();
                            }

                            //Insert Default Record in User Menu Link

                            var MenuKey = await db.GtEuusgrs.Where(x => x.UserGroup == obj.UserGroup && x.UserType == obj.UserType && x.ActiveStatus == true).ToListAsync();
                            foreach (var mkey in MenuKey)
                            {
                                GtEuusml userMenuLink = new GtEuusml();
                                userMenuLink.UserId = obj.UserID;
                                userMenuLink.BusinessKey = obj.BusinessKey;
                                userMenuLink.MenuKey = mkey.MenuKey;
                                userMenuLink.ActiveStatus = obj.ActiveStatus;
                                userMenuLink.FormId = obj.FormId;
                                userMenuLink.CreatedBy = obj.CreatedBy;
                                userMenuLink.CreatedOn = System.DateTime.Now;
                                userMenuLink.CreatedTerminal = obj.TerminalId;
                                db.GtEuusmls.Add(userMenuLink);
                                await db.SaveChangesAsync();
                            }

                            //Insert Default Record in User Menu Action Link

                            var MenuActionLink = await db.GtEcmnfls.Join(db.GtEcfmals, u => u.FormId, uir => uir.FormId,
                                    (u, uir) => new { u, uir }).
                                    Join(db.GtEuusgrs, r => r.u.MenuKey, ro => ro.MenuKey, (r, ro) => new { r, ro })
                                    .Where(m => m.ro.UserGroup == obj.UserGroup && m.ro.UserType == obj.UserType)
                                    .Select(m => new DO_UserFormAction
                                    {
                                        MenuKey = m.ro.MenuKey,
                                        ActionID = m.r.uir.ActionId
                                    }).ToListAsync();

                            foreach (var makey in MenuActionLink)
                            {
                                GtEuusfa userMenuActionLink = new GtEuusfa();
                                userMenuActionLink.UserId = obj.UserID;
                                userMenuActionLink.BusinessKey = obj.BusinessKey;
                                userMenuActionLink.MenuKey = makey.MenuKey;
                                userMenuActionLink.ActionId = makey.ActionID;
                                userMenuActionLink.ActiveStatus = obj.ActiveStatus;
                                userMenuActionLink.FormId = obj.FormId;
                                userMenuActionLink.CreatedBy = obj.CreatedBy;
                                userMenuActionLink.CreatedOn = System.DateTime.Now;
                                userMenuActionLink.CreatedTerminal = obj.TerminalId;
                                db.GtEuusfas.Add(userMenuActionLink);
                                await db.SaveChangesAsync();
                            }
                        }

                        ap_cd.UserGroup = obj.UserGroup;
                        ap_cd.UserType = obj.UserType;
                        ap_cd.AllowMtfy = obj.AllowMTFY;
                        ap_cd.ActiveStatus = obj.ActiveStatus;
                        ap_cd.ModifiedBy = obj.UserID;
                        ap_cd.ModifiedOn = System.DateTime.Now;
                        ap_cd.ModifiedTerminal = obj.TerminalId;

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
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ConfigureMenu> GetMenulist(int UserGroup, int UserType, short UserID, int BusinessKey)
        {
            try
            {
                //UserGroup = 10001;
                //UserType = 20004;
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
                        .Where(x => x.Bloc.lkey.UserGroup == UserGroup && x.Bloc.lkey.UserType == UserType && x.Bloc.lkey.ActiveStatus == true)
                                    .Select(m => new DO_MainMenu()
                                    {
                                        MainMenuId = m.Bloc.ent.MainMenuId,
                                        MainMenu = m.seg.MainMenu,
                                        MenuIndex = m.seg.MenuIndex
                                    }).Distinct().ToListAsync();

                    //mn.l_MainMenu = await db.GtEcmamn.Where(w => w.ActiveStatus == true)
                    //                .Select(m => new DO_MainMenu()
                    //                {
                    //                    MainMenuId = m.MainMenuId,
                    //                    MainMenu = m.MainMenu,
                    //                    MenuIndex = m.MenuIndex
                    //                }).ToListAsync();

                    mn.l_SubMenu = await db.GtEuusgrs.Join(db.GtEcmnfls,
                            lkey => new { lkey.MenuKey },
                            ent => new { ent.MenuKey },
                            (lkey, ent) => new { lkey, ent })
                            .Join(db.GtEcsbmns,
                        Bloc => new { Bloc.ent.MainMenuId, Bloc.ent.MenuItemId },
                        seg => new { seg.MainMenuId, seg.MenuItemId },
                        (Bloc, seg) => new { Bloc, seg })
                        .Where(x => x.Bloc.lkey.UserGroup == UserGroup && x.Bloc.lkey.UserType == UserType && x.Bloc.lkey.ActiveStatus == true)
                                    .Select(f => new DO_SubMenu()
                                    {
                                        MainMenuId = f.Bloc.ent.MainMenuId,
                                        MenuItemId = f.Bloc.ent.MenuItemId,
                                        MenuItemName = f.seg.MenuItemName,
                                        MenuIndex = f.seg.MenuIndex,
                                        ParentID = f.seg.ParentId
                                    }).Distinct().ToListAsync();

                    //mn.l_SubMenu = await db.GtEcsbmn.Where(w => w.ActiveStatus == true)
                    //                .Select(s => new DO_SubMenu()
                    //                {
                    //                    MainMenuId = s.MainMenuId,
                    //                    MenuItemId = s.MenuItemId,
                    //                    MenuItemName = s.MenuItemName,
                    //                    MenuIndex = s.MenuIndex,
                    //                    ParentID = s.ParentId
                    //                }).ToListAsync();

                    mn.l_FormMenu = await db.GtEuusgrs.Join(db.GtEcmnfls,
                            lkey => new { lkey.MenuKey },
                            ent => new { ent.MenuKey },
                            (lkey, ent) => new { lkey, ent })
                        .Where(x => x.lkey.UserGroup == UserGroup && x.lkey.UserType == UserType)
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
                        GtEuusml getlocDesc = db.GtEuusmls.Where(c => c.UserId == UserID && c.BusinessKey == BusinessKey && c.MenuKey == obj.FormId).FirstOrDefault();
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

        public async Task<List<DO_UserFormAction>> GetUserFormActionLink(short UserID, int BusinessKey, int MenuKey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    //var result = db.GtEuusfa
                    //    .Join(db.GtEcfmac,
                    //        lkey => new { lkey.ActionId },
                    //        ent => new { ent.ActionId },
                    //        (lkey, ent) => new { lkey, ent })
                    //    .Where(x =>
                    //          x.lkey.UserId == UserID && x.lkey.BusinessKey == BusinessKey && x.lkey.MenuKey == MenuKey)
                    //    .Select(c => new DO_UserFormAction
                    //    {
                    //        ActionID = c.lkey.ActionId,
                    //        ActionDesc = c.ent.ActionDesc,
                    //        ActiveStatus = c.lkey.ActiveStatus
                    //    }).ToListAsync();

                    GtEcmnfl geformID = db.GtEcmnfls.Where(x => x.MenuKey == MenuKey).FirstOrDefault();
                    int formID = 0;
                    if (geformID != null)
                        formID = geformID.FormId;

                    var result = await db.GtEcfmacs
                    .Join(db.GtEcfmals.Where(w => w.FormId == formID),
                    a => a.ActionId,
                    f => f.ActionId,
                    (a, f) => new { a, f })
                    //(a, f) => new { a, f = f.FirstOrDefault() })
                    .Select(r => new DO_UserFormAction
                    {
                        ActionID = r.a.ActionId,
                        ActionDesc = r.a.ActionDesc,
                        ActiveStatus = r.f != null ? r.f.ActiveStatus : false,
                    }).ToListAsync();

                    foreach (var obj in result)
                    {
                        GtEuusfa getUserAction = db.GtEuusfas.Where(x => x.UserId == UserID && x.BusinessKey == BusinessKey && x.MenuKey == MenuKey && x.ActionId == obj.ActionID).FirstOrDefault();
                        if (getUserAction != null)
                        {
                            obj.ActiveStatus = getUserAction.ActiveStatus;
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoUserMenuAction(DO_UserMenuLink obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEuusml uml = await db.GtEuusmls.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey && w.MenuKey == obj.MenuKey).FirstOrDefaultAsync();

                        if (uml != null)
                        {
                            uml.ActiveStatus = obj.ActiveStatus;
                            uml.ModifiedBy = obj.CreatedBy;
                            uml.ModifiedOn = System.DateTime.Now;
                            uml.ModifiedTerminal = obj.TerminalId;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            uml = new GtEuusml();
                            uml.UserId = obj.UserID;
                            uml.BusinessKey = obj.BusinessKey;
                            uml.MenuKey = obj.MenuKey;
                            uml.ActiveStatus = obj.ActiveStatus;
                            uml.FormId = obj.FormId;
                            uml.CreatedBy = obj.UserID;
                            uml.CreatedOn = DateTime.Now;
                            uml.CreatedTerminal = System.Environment.MachineName;
                            db.GtEuusmls.Add(uml);
                            await db.SaveChangesAsync();
                        }

                        //if (obj.ActiveStatus == true)
                        //{
                        var fa = await db.GtEuusfas.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey && w.MenuKey == obj.MenuKey).ToListAsync();

                        foreach (GtEuusfa f in fa)
                        {
                            f.ActiveStatus = false;
                            f.ModifiedBy = obj.UserID;
                            f.ModifiedOn = System.DateTime.Now;
                            f.ModifiedTerminal = obj.TerminalId;
                        }
                        await db.SaveChangesAsync();

                        if (obj.l_formAction != null)
                        {
                            foreach (DO_UserFormAction i in obj.l_formAction)
                            {
                                var obj_FA = await db.GtEuusfas.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey && w.MenuKey == obj.MenuKey && w.ActionId == i.ActionID).FirstOrDefaultAsync();
                                if (obj_FA != null)
                                {
                                    if (i.Active.Substring(0, 1).ToString() == "Y")
                                        obj_FA.ActiveStatus = true;
                                    else
                                        obj_FA.ActiveStatus = false;
                                    obj_FA.ModifiedBy = obj.UserID;
                                    obj_FA.ModifiedOn = DateTime.Now;
                                    obj_FA.ModifiedTerminal = System.Environment.MachineName;
                                }
                                else
                                {
                                    obj_FA = new GtEuusfa();
                                    obj_FA.UserId = obj.UserID;
                                    obj_FA.BusinessKey = obj.BusinessKey;
                                    obj_FA.MenuKey = obj.MenuKey;
                                    obj_FA.ActionId = i.ActionID;
                                    if (i.Active.Substring(0, 1).ToString() == "Y")
                                        obj_FA.ActiveStatus = true;
                                    else
                                        obj_FA.ActiveStatus = false;
                                    obj_FA.FormId = obj.FormId;
                                    obj_FA.CreatedBy = obj.UserID;
                                    obj_FA.CreatedOn = DateTime.Now;
                                    obj_FA.CreatedTerminal = System.Environment.MachineName;
                                    db.GtEuusfas.Add(obj_FA);
                                }
                            }
                            await db.SaveChangesAsync();
                        }
                        //}
                        //else
                        //{
                        //    var fa = await db.GtEuusfa.Where(w => w.UserId == obj.UserID && w.BusinessKey == obj.BusinessKey && w.MenuKey == obj.MenuKey).ToListAsync();

                        //    foreach (GtEuusfa f in fa)
                        //    {
                        //        f.ActiveStatus = false;
                        //        f.ModifiedBy = obj.UserID;
                        //        f.ModifiedOn = System.DateTime.Now;
                        //        f.ModifiedTerminal = obj.TerminalId;
                        //    }
                        //    await db.SaveChangesAsync();
                        //}
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
                        throw ex;
                    }
                }
            }
        }

        public List<int> GetMenuKeysforUser(short UserID, int BusinessKey)
        {
            try
            {
                List<int> menukeys = new List<int>();
                using (var db = new eSyaEnterprise())
                {
                    IEnumerable<GtEuusml> UserMenu = db.GtEuusmls.Where(u => u.UserId == UserID && u.BusinessKey == BusinessKey && u.ActiveStatus == true);
                    int key;
                    foreach (GtEuusml obj in UserMenu)
                    {
                        key = new int();
                        key = obj.MenuKey;
                        menukeys.Add(key);
                    }
                }
                return menukeys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_UserMaster>> GetUserMasterForUserAuthentication()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEuusms.Where(x => x.IsUserAuthenticated == false && x.ActiveStatus == false)
                        .Select(r => new DO_UserMaster
                        {
                            UserID = r.UserId,
                            LoginID = r.LoginId,
                            LoginDesc = r.LoginDesc,
                            ISDCode = r.Isdcode ?? 0,
                            MobileNumber = r.MobileNumber,
                            LastActivityDate = (DateTime)r.LastActivityDate,
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

        public async Task<DO_ReturnParameter> UpdateUserMasteronAuthentication(DO_UserMaster obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEuusm ap_cd = db.GtEuusms.Where(w => w.UserId == obj.UserID).FirstOrDefault();
                        if (ap_cd == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
                        }

                        var _isMobileNoExist = db.GtEuusms.Where(w => w.UserId != obj.UserID && w.MobileNumber == obj.MobileNumber).Count();
                        if (_isMobileNoExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0128", Message = string.Format(_localizer[name: "W0128"]) };
                        }

                        var _isEmaiExist = db.GtEuusms.Where(w => w.UserId != obj.UserID && w.EMailId == obj.eMailID).Count();
                        if (_isEmaiExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0129", Message = string.Format(_localizer[name: "W0129"]) };
                        }

                        ap_cd.LoginDesc = obj.LoginDesc;
                        ap_cd.Isdcode = obj.ISDCode;
                        ap_cd.MobileNumber = obj.MobileNumber;
                        ap_cd.AllowMobileLogin = obj.AllowMobileLogin;
                        ap_cd.EMailId = obj.eMailID;
                        ap_cd.Photo = obj.Photo;
                        ap_cd.PhotoUrl = null;
                        ap_cd.DigitalSignature = obj.DigitalSignature;
                        ap_cd.IsUserAuthenticated = true;
                        ap_cd.UserAuthenticatedDate = System.DateTime.Now;
                        ap_cd.ActiveStatus = true;
                        ap_cd.ModifiedBy = obj.UserID;
                        ap_cd.ModifiedOn = System.DateTime.Now;
                        ap_cd.ModifiedTerminal = obj.TerminalId;

                        await db.SaveChangesAsync();

                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, Message = string.Format(_localizer[name: "S0006"]), ID = obj.UserID };
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

        public async Task<List<DO_UserMaster>> GetUserMasterForUserDeactivation()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEuusms.Where(x => x.IsUserDeactivated == false && x.ActiveStatus == true)
                        .Select(r => new DO_UserMaster
                        {
                            UserID = r.UserId,
                            LoginID = r.LoginId,
                            LoginDesc = r.LoginDesc,
                            ISDCode = r.Isdcode ?? 0,
                            MobileNumber = r.MobileNumber,
                            LastActivityDate = (DateTime)r.LastActivityDate,
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

        public async Task<DO_ReturnParameter> UpdateUserForDeativation(DO_UserMaster obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEuusm ap_cd = db.GtEuusms.Where(w => w.UserId == obj.UserID).FirstOrDefault();
                        if (ap_cd == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
                        }

                        ap_cd.DeactivationReason = obj.DeactivationReason;
                        ap_cd.IsUserDeactivated = true;
                        ap_cd.UserDeactivatedOn = System.DateTime.Now;
                        ap_cd.ActiveStatus = false;
                        ap_cd.ModifiedBy = obj.UserID;
                        ap_cd.ModifiedOn = System.DateTime.Now;
                        ap_cd.ModifiedTerminal = obj.TerminalId;

                        await db.SaveChangesAsync();

                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true,StatusCode= "S0007", Message = string.Format(_localizer[name: "S0007"]), ID = obj.UserID };
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

        #endregion User Creation

    }
}

