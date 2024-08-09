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
    public class BlockRepository: IBlockRepository
    {
        private readonly IStringLocalizer<BlockRepository> _localizer;
        public BlockRepository(IStringLocalizer<BlockRepository> localizer)
        {
            _localizer = localizer;
        }

        #region Un-Block User
        public async Task<List<DO_UserMaster>> GetBlockedUsers()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEuusms.Where(x => x.BlockSignIn == true && x.ActiveStatus == true)
                        .Select(r => new DO_UserMaster
                        {
                            UserID = r.UserId,
                            LoginID = r.LoginId,
                            LoginDesc = r.LoginDesc,
                            EMailId = r.EMailId,
                            UnsuccessfulAttempt = r.UnsuccessfulAttempt,
                            LastPasswordUpdatedDate=r.LastPasswordUpdatedDate,
                            LoginAttemptDate= (DateTime)r.LoginAttemptDate,
                            LastActivityDate = (DateTime)r.LastActivityDate,
                            ActiveStatus = r.ActiveStatus,
                            BlockSignIn=r.BlockSignIn,
                        }).OrderBy(o => o.LoginDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> UpdateBlockSignIn(DO_BlockUser obj)
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
                       
                        b_user.BlockSignIn = obj.BlockSignIn;
                        b_user.UnsuccessfulAttempt = 0;
                        b_user.LastActivityDate = DateTime.Now;
                        b_user.LoginAttemptDate = DateTime.Now;
                        b_user.ModifiedBy = obj.ModifiedBy;
                        b_user.ModifiedOn = System.DateTime.Now;
                        b_user.ModifiedTerminal = obj.TerminalID;
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, Message = string.Format(_localizer[name: "S0009"]) };
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
    }
}
