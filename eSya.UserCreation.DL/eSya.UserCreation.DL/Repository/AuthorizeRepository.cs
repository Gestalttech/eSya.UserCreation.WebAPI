using eSya.UserCreation.DL.Entities;
using eSya.UserCreation.DO;
using eSya.UserCreation.IF;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.DL.Repository
{
    public class AuthorizeRepository:IAuthorizeRepository
    {
        private readonly IStringLocalizer<AuthorizeRepository> _localizer;
        public AuthorizeRepository(IStringLocalizer<AuthorizeRepository> localizer)
        {
            _localizer = localizer;
        }

        #region Authenticate User
        public async Task<List<DO_UserMaster>> GetUnAuthenticatedUsers(string authenticate)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    if(authenticate == "Authentic")
                    {
                        var ds = db.GtEuusms.
                            Join(db.GtEuubgrs,
                            u => new {u.UserId},
                            g => new {g.UserId},
                            (u, g) => new {u,g})
                           .Join(db.GtEcapcds.Where(x => x.ActiveStatus),
                            mg => new { mg.g.UserGroup, },
                            g => new { UserGroup = g.ApplicationCode },
                            (mg, g) => new { mg, g })
                            .Join(db.GtEcapcds.Where(x => x.ActiveStatus),
                             cg => new { cg.mg.g.UserRole, },
                             c => new { UserRole = c.ApplicationCode },
                             (cg, c) => new { cg, c })

                            .Where(w => w.cg.mg.u.IsUserAuthenticated && w.cg.mg.u.ActiveStatus && w.cg.mg.u.RejectionReason == null
                             &&w.cg.mg.g.ActiveStatus)
                    .Select(r => new DO_UserMaster
                    {
                        UserID = r.cg.mg.u.UserId,
                        LoginID = r.cg.mg.u.LoginId,
                        LoginDesc = r.cg.mg.u.LoginDesc,
                        EMailId = r.cg.mg.u.EMailId,
                        UnsuccessfulAttempt = r.cg.mg.u.UnsuccessfulAttempt,
                        LastPasswordUpdatedDate = r.cg.mg.u.LastPasswordUpdatedDate,
                        LoginAttemptDate = (DateTime)r.cg.mg.u.LoginAttemptDate,
                        LastActivityDate = (DateTime)r.cg.mg.u.LastActivityDate,
                        ActiveStatus = r.cg.mg.u.ActiveStatus,
                        IsUserAuthenticated = r.cg.mg.u.IsUserAuthenticated,
                        UserAuthenticatedDate = (DateTime)r.cg.mg.u.UserAuthenticatedDate,
                        UserGroup = r.cg.mg.g.UserGroup,
                        UserRole = r.cg.mg.g.UserRole,
                        AuthenticStatus= "Authentic",
                        UserGroupDesc=r.cg.g.CodeDesc,
                        UserRoleDesc=r.c.CodeDesc

                    }).OrderBy(o => o.LoginDesc).ToListAsync();
                        return await ds;
                    }
                    else if (authenticate == "Rejected")
                    {
                        var ds = db.GtEuusms.
                            Join(db.GtEuubgrs,
                            u => new { u.UserId },
                            g => new { g.UserId },
                            (u, g) => new { u, g })
                           .Join(db.GtEcapcds.Where(x => x.ActiveStatus),
                            mg => new { mg.g.UserGroup, },
                            g => new { UserGroup = g.ApplicationCode },
                            (mg, g) => new { mg, g })
                            .Join(db.GtEcapcds.Where(x => x.ActiveStatus),
                             cg => new { cg.mg.g.UserRole, },
                             c => new { UserRole = c.ApplicationCode },
                             (cg, c) => new { cg, c })

                            .Where(w => !w.cg.mg.u.IsUserAuthenticated && w.cg.mg.u.ActiveStatus && w.cg.mg.u.RejectionReason != null
                             && w.cg.mg.g.ActiveStatus)
                    .Select(r => new DO_UserMaster
                    {
                        UserID = r.cg.mg.u.UserId,
                        LoginID = r.cg.mg.u.LoginId,
                        LoginDesc = r.cg.mg.u.LoginDesc,
                        EMailId = r.cg.mg.u.EMailId,
                        UnsuccessfulAttempt = r.cg.mg.u.UnsuccessfulAttempt,
                        LastPasswordUpdatedDate = r.cg.mg.u.LastPasswordUpdatedDate,
                        LoginAttemptDate = (DateTime)r.cg.mg.u.LoginAttemptDate,
                        LastActivityDate = (DateTime)r.cg.mg.u.LastActivityDate,
                        ActiveStatus = r.cg.mg.u.ActiveStatus,
                        IsUserAuthenticated = r.cg.mg.u.IsUserAuthenticated,
                        UserAuthenticatedDate = (DateTime)r.cg.mg.u.UserAuthenticatedDate,
                        UserGroup = r.cg.mg.g.UserGroup,
                        UserRole = r.cg.mg.g.UserRole,
                        AuthenticStatus = "Rejected",
                        UserGroupDesc = r.cg.g.CodeDesc,
                        UserRoleDesc = r.c.CodeDesc

                    }).OrderBy(o => o.LoginDesc).ToListAsync();
                        return await ds;
                    }
                    else
                    {
                       
                        var ds = db.GtEuusms.Join(db.GtEuubgrs,
                                                   u => new { u.UserId },
                                                   g => new { g.UserId },
                                                   (u, g) => new { u, g })
                                                  .Join(db.GtEcapcds.Where(x => x.ActiveStatus),
                                                   mg => new { mg.g.UserGroup, },
                                                   g => new { UserGroup = g.ApplicationCode },
                                                   (mg, g) => new { mg, g })
                                                   .Join(db.GtEcapcds.Where(x => x.ActiveStatus),
                                                    cg => new { cg.mg.g.UserRole, },
                                                    c => new { UserRole = c.ApplicationCode },
                                                    (cg, c) => new { cg, c })

                                                   .Where(w => !w.cg.mg.u.IsUserAuthenticated && w.cg.mg.u.ActiveStatus && w.cg.mg.u.RejectionReason == null
                                                    && w.cg.mg.g.ActiveStatus)
                                           .Select(r => new DO_UserMaster
                                           {
                                               UserID = r.cg.mg.u.UserId,
                                               LoginID = r.cg.mg.u.LoginId,
                                               LoginDesc = r.cg.mg.u.LoginDesc,
                                               EMailId = r.cg.mg.u.EMailId,
                                               UnsuccessfulAttempt = r.cg.mg.u.UnsuccessfulAttempt,
                                               LastPasswordUpdatedDate = r.cg.mg.u.LastPasswordUpdatedDate,
                                               LoginAttemptDate = (DateTime)r.cg.mg.u.LoginAttemptDate,
                                               LastActivityDate = (DateTime)r.cg.mg.u.LastActivityDate,
                                               ActiveStatus = r.cg.mg.u.ActiveStatus,
                                               IsUserAuthenticated = r.cg.mg.u.IsUserAuthenticated,
                                               UserAuthenticatedDate = (DateTime)r.cg.mg.u.UserAuthenticatedDate,
                                               UserGroup = r.cg.mg.g.UserGroup,
                                               UserRole = r.cg.mg.g.UserRole,
                                               AuthenticStatus = "Un Authentic",
                                               UserGroupDesc = r.cg.g.CodeDesc,
                                               UserRoleDesc = r.c.CodeDesc

                                           }).OrderBy(o => o.LoginDesc).ToListAsync();
                        return await ds;

                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> AuthenticateUser(DO_Authorize obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEuusm b_user = db.GtEuusms.Where(w => w.UserId == obj.UserID).FirstOrDefault();
                        if (b_user == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
                        }
                        b_user.RejectionReason = null;
                        b_user.IsUserAuthenticated = obj.IsUserAuthenticated;
                        b_user.UserAuthenticatedDate= System.DateTime.Now;
                        b_user.ModifiedBy = obj.ModifiedBy;
                        b_user.ModifiedOn = System.DateTime.Now;
                        b_user.ModifiedTerminal = obj.TerminalID;
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, Message = string.Format(_localizer[name: "S0011"]) };
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
        public async Task<DO_ReturnParameter> RejectUser(DO_Authorize obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEuusm b_user = db.GtEuusms.Where(w => w.UserId == obj.UserID).FirstOrDefault();
                        if (b_user == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
                        }
                        b_user.RejectionReason = obj.RejectionReason;
                        b_user.IsUserAuthenticated = obj.IsUserAuthenticated;
                        b_user.UserAuthenticatedDate = System.DateTime.Now;
                        b_user.ModifiedBy = obj.ModifiedBy;
                        b_user.ModifiedOn = System.DateTime.Now;
                        b_user.ModifiedTerminal = obj.TerminalID;
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, Message = string.Format(_localizer[name: "S0013"]) };
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
        public async Task<DO_ConfigureMenu> GetUserLinkedFormMenulist(int UserID,int UserGroup,int UserRole)
        {
            try
            {

                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    DO_ConfigureMenu mn = new DO_ConfigureMenu();

                    mn.l_MainMenu = await db.GtEuubgrs.Join(db.GtEuusgrs,
                        x => new { x.UserRole, x.UserGroup, x.BusinessKey },
                        y => new { y.UserRole, y.UserGroup, y.BusinessKey },
                        (x, y) => new { x, y })
                       .Join(db.GtEcmnfls,
                           lkey => new { lkey.y.MenuKey },
                           ent => new { ent.MenuKey },
                           (lkey, ent) => new { lkey, ent })
                           .Join(db.GtEcmamns,
                       Bloc => new { Bloc.ent.MainMenuId },
                       seg => new { seg.MainMenuId },
                       (Bloc, seg) => new { Bloc, seg })
                       .Where(w => w.Bloc.lkey.x.UserId == UserID && w.Bloc.lkey.x.UserGroup == UserGroup && w.Bloc.lkey.x.UserRole == UserRole && w.Bloc.lkey.x.ActiveStatus && w.Bloc.lkey.y.ActiveStatus)
                                   .Select(m => new DO_MainMenu()
                                   {
                                       MainMenuId = m.Bloc.ent.MainMenuId,
                                       MainMenu = m.seg.MainMenu,
                                       MenuIndex = m.seg.MenuIndex
                                   }).Distinct().ToListAsync();


                    mn.l_SubMenu = await db.GtEuubgrs.Join(db.GtEuusgrs,
                        x => new { x.UserRole, x.UserGroup, x.BusinessKey },
                        y => new { y.UserRole, y.UserGroup, y.BusinessKey },
                        (x, y) => new { x, y })
                        .Join(db.GtEcmnfls,
                           lkey => new { lkey.y.MenuKey },
                           ent => new { ent.MenuKey },
                           (lkey, ent) => new { lkey, ent })
                           .Join(db.GtEcsbmns,
                       Bloc => new { Bloc.ent.MainMenuId, Bloc.ent.MenuItemId },
                       seg => new { seg.MainMenuId, seg.MenuItemId },
                       (Bloc, seg) => new { Bloc, seg })
                       .Where(w => w.Bloc.lkey.x.UserId == UserID && w.Bloc.lkey.x.UserGroup == UserGroup && w.Bloc.lkey.x.UserRole == UserRole && w.Bloc.lkey.x.ActiveStatus && w.Bloc.lkey.y.ActiveStatus)
                                   .Select(f => new DO_SubMenu()
                                   {
                                       MainMenuId = f.Bloc.ent.MainMenuId,
                                       MenuItemId = f.Bloc.ent.MenuItemId,
                                       MenuItemName = f.seg.MenuItemName,
                                       MenuIndex = f.seg.MenuIndex,
                                       ParentID = f.seg.ParentId
                                   }).Distinct().ToListAsync();

                    mn.l_FormMenu = await db.GtEuubgrs.Join(db.GtEuusgrs,
                        x => new { x.UserRole, x.UserGroup, x.BusinessKey },
                        y => new { y.UserRole, y.UserGroup, y.BusinessKey },
                        (x, y) => new { x, y })
                        .Join(db.GtEcmnfls,
                          lkey => new { lkey.y.MenuKey },
                          ent => new { ent.MenuKey },
                          (lkey, ent) => new { lkey, ent })
                      .Where(w => w.lkey.x.UserId == UserID && w.lkey.x.UserGroup == UserGroup && w.lkey.x.UserRole == UserRole && w.lkey.x.ActiveStatus && w.ent.ActiveStatus)
                                  .Select(f => new DO_FormMenu()
                                  {
                                      MainMenuId = f.ent.MainMenuId,
                                      MenuItemId = f.ent.MenuItemId,
                                      FormId = f.ent.MenuKey,
                                      //FormId = f.ent.FormId,
                                      FormNameClient = f.ent.FormNameClient,
                                      FormIndex = f.ent.FormIndex,
                                      ActiveStatus=true
                                  }).ToListAsync();
                   
                    return mn;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_UserRoleActionLink>> GetActionListByUserRole(int userID, int UserGroup, int UserRole,int formID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

             
                        var ds =await db.GtEuubgrs.Where(x => x.UserId == userID && x.UserGroup == UserGroup && x.UserRole == UserRole && x.ActiveStatus).Join
                            (db.GtEuusgrs.Where(x => x.MenuKey == formID && x.ActiveStatus),
                            u => new { u.BusinessKey, u.UserGroup, u.UserRole },
                            e => new { e.BusinessKey, e.UserGroup, e.UserRole },
                            (u, e) => new { u, e }).Join
                            (db.GtEuusrls.Where(x=>x.ActiveStatus),
                            r => new { r.e.UserRole },
                            s => new { s.UserRole },
                            (r, s) => new { r, s }).
                            Join(db.GtEcfmacs.Where(x=>x.ActiveStatus),
                            a=>new {a.s.ActionId},
                            aa=>new {aa.ActionId},
                            (a, aa) => new {a,aa})
                            .Select(m => new DO_UserRoleActionLink
                            {
                                ActionId =m.a.s.ActionId,
                                ActionDesc =m.aa.ActionDesc,
                                ActiveStatus =m.a.s.ActiveStatus
                            })
                            .ToListAsync();
                    return ds;
                    }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
