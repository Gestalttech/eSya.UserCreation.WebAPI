using eSya.UserCreation.DL.Entities;
using eSya.UserCreation.DO;
using eSya.UserCreation.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.DL.Repository
{
    public class ApprovalRepository: IApprovalRepository
    {
        private readonly IStringLocalizer<ApprovalRepository> _localizer;
        public ApprovalRepository(IStringLocalizer<ApprovalRepository> localizer)
        {
            _localizer = localizer;
        }
        public async Task<List<DO_UserMaster>> GetApproverUserListbyBusinessKey(int BusinessKey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEuusbls.Join
                      (db.GtEuusms,
                      b => new { b.UserId },
                      u => new { u.UserId },
                      (b, u) => new { b, u })
                      .Join(db.GtEuuspas,
                      bb=>new {bb.u.UserId},
                      p => new {p.UserId},
                      (bb, p) => new {bb,p}).
                      Where(w=>w.bb.b.BusinessKey== BusinessKey && w.bb.b.ActiveStatus
                      && w.bb.u.ActiveStatus && w.p.ParameterId==1 && w.p.ParmAction)
                     .Select(r => new DO_UserMaster
                        {
                            UserID = r.bb.u.UserId,
                            LoginDesc = r.bb.u.LoginDesc,
                    
                        }).ToList();
                    var Distusers = ds.GroupBy(x => new { x.UserID }).Select(g => g.First()).ToList();

                    return Distusers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ConfigureMenu> GetApprovalRequiredFormMenulist(int BusinessKey,int UserID)
        {
            try
            {

                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    DO_ConfigureMenu mn = new DO_ConfigureMenu();

                    mn.l_MainMenu = await db.GtEuubgrs.Join(db.GtEuusgrs,
                        x=>new {x.UserRole,x.UserGroup,x.BusinessKey},
                        y => new {y.UserRole,y.UserGroup,y.BusinessKey},
                        (x, y) => new {x,y})
                       .Join(db.GtEcmnfls,
                           lkey => new { lkey.y.MenuKey },
                           ent => new { ent.MenuKey },
                           (lkey, ent) => new { lkey, ent })
                        .Join(db.GtEcfmpas.Where(h => h.ParameterId == 3 && h.ActiveStatus),
                         pt => new { pt.ent.FormId },
                         p => new { p.FormId },
                         (pt, p) => new { pt, p })
                           .Join(db.GtEcmamns,
                       Bloc => new { Bloc.pt.ent.MainMenuId },
                       seg => new { seg.MainMenuId },
                       (Bloc, seg) => new { Bloc, seg })
                       .Where(w =>w.Bloc.pt.lkey.x.BusinessKey==BusinessKey && w.Bloc.pt.lkey.x.UserId == UserID  && w.Bloc.pt.lkey.x.ActiveStatus && w.Bloc.pt.lkey.y.ActiveStatus)
                                   .Select(m => new DO_MainMenu()
                                   {
                                       MainMenuId = m.Bloc.pt.ent.MainMenuId,
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
                         .Join(db.GtEcfmpas.Where(h => h.ParameterId == 3 && h.ActiveStatus),
                         pt => new { pt.ent.FormId },
                         p => new { p.FormId },
                         (pt, p) => new { pt, p })
                           .Join(db.GtEcsbmns,
                       Bloc => new { Bloc.pt.ent.MainMenuId, Bloc.pt.ent.MenuItemId },
                       seg => new { seg.MainMenuId, seg.MenuItemId },
                       (Bloc, seg) => new { Bloc, seg })
                       .Where(w => w.Bloc.pt.lkey.x.BusinessKey == BusinessKey && w.Bloc.pt.lkey.x.UserId == UserID && w.Bloc.pt.lkey.x.ActiveStatus && w.Bloc.pt.lkey.y.ActiveStatus)
                                   .Select(f => new DO_SubMenu()
                                   {
                                       MainMenuId = f.Bloc.pt.ent.MainMenuId,
                                       MenuItemId = f.Bloc.pt.ent.MenuItemId,
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
                        .Join(db.GtEcfmpas.Where(h=>h.ParameterId==3 && h.ActiveStatus ),
                         pt => new {pt.ent.FormId},
                         p => new {p.FormId},
                         (pt, p) => new {pt,p})

                      .Where(w => w.pt.lkey.x.BusinessKey == BusinessKey && w.pt.lkey.x.UserId == UserID && w.pt.lkey.x.ActiveStatus && w.pt.ent.ActiveStatus)
                                  .Select(f => new DO_FormMenu()
                                  {
                                      MainMenuId = f.pt.ent.MainMenuId,
                                      MenuItemId = f.pt.ent.MenuItemId,
                                      //FormId = f.pt.ent.MenuKey,
                                      FormId = f.pt.ent.FormId,
                                      FormNameClient = f.pt.ent.FormNameClient,
                                      FormIndex = f.pt.ent.FormIndex,
                                      ActiveStatus=true
                                  }).ToListAsync();
                    //here we need to get active status from approved table
                    //foreach (var obj in mn.l_FormMenu)
                    //{
                    //    GtEuusap getlocDesc = db.GtEuusaps.Where(c => c.BusinessKey == BusinessKey && c.FormId == formID && c.UserId == UserID).FirstOrDefault();
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
        public async Task<List<DO_ApprovalLevels>> GetApprovalLevelsbyFormID( int businesskey, int formId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtEcapvds.Where(x => x.BusinessKey == businesskey && x.FormId == formId && x.ActiveStatus)
                        .Join(db.GtEcapcds,
                        x => new { x.ApprovalLevel },
                        y => new { ApprovalLevel = y.ApplicationCode },
                        (x, y) => new { x, y }).Select
                        (f => new DO_ApprovalLevels()
                        {
                            ApprovalLevel=f.x.ApprovalLevel,
                            ApprovalLevelDesc=f.y.CodeDesc,
                            ActiveStatus=false
                        }).ToListAsync();
                        
                        var distinct = ds
                        .GroupBy(x => new { x.ApprovalLevel })
                        .Select(g => g.First())
                        .ToList();
                        //return distinct;

                    if (ds.Count == 0)
                    {
                        var vds = await db.GtEcapvvs.Where(x => x.BusinessKey == businesskey && x.FormId == formId && x.ActiveStatus)
                       .Join(db.GtEcapcds,
                       x => new { x.ApprovalLevel },
                       y => new { ApprovalLevel = y.ApplicationCode },
                       (x, y) => new { x, y }).Select
                       (f => new DO_ApprovalLevels()
                       {
                           ApprovalLevel = f.x.ApprovalLevel,
                           ApprovalLevelDesc = f.y.CodeDesc,
                           ActiveStatus = false
                       }).ToListAsync();

                        var vdistinct = vds
                .GroupBy(x => new { x.ApprovalLevel })
                .Select(g => g.First())
                .ToList();
                        return vdistinct;
                    }
                    else
                    {
                        return distinct;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertOrUpdateUserApprovalForm(List<DO_UserApprovalForm>  obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var _uf in obj)
                        {
                            var _userform = db.GtEuusaps.Where(w => w.BusinessKey == _uf.BusinessKey && w.FormId == _uf.FormID
                        && w.UserId == _uf.UserID && w.ApprovalLevel == _uf.ApprovalLevel).FirstOrDefault();
                            if (_userform != null)
                            {
                                _userform.ActiveStatus = _uf.ActiveStatus;
                                _userform.ModifiedBy = _uf.UserID;
                                _userform.ModifiedOn = System.DateTime.Now;
                                _userform.ModifiedTerminal = _uf.TerminalID;
                            }
                            else
                            {
                                if (_uf.ActiveStatus)
                                {
                                    var _formlink = new GtEuusap
                                    {
                                        BusinessKey = _uf.BusinessKey,
                                        FormId = _uf.FormID,
                                        UserId = _uf.UserID,
                                        ApprovalLevel = _uf.ApprovalLevel,
                                        ActiveStatus = _uf.ActiveStatus,
                                        CreatedBy = _uf.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = _uf.TerminalID
                                    };
                                    db.GtEuusaps.Add(_formlink);
                                }

                            }
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
    }
}
