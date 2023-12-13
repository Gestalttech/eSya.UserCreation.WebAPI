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
    public class eSignatureRepository: IeSignatureRepository
    {
        private readonly IStringLocalizer<eSignatureRepository> _localizer;
        public eSignatureRepository(IStringLocalizer<eSignatureRepository> localizer)
        {
            _localizer = localizer;
        }
        #region Upload User eSignature

        public async Task<List<DO_eSignature>> GetActiveUsersforSignature()
        {
            try
            {
                
                using (var db = new eSyaEnterprise())
                {
                    byte[] emptyByte = { };
                    var ds =  db.GtEuusms.Where(x =>x.ActiveStatus)
                        .GroupJoin(db.GtEuuses,
                        lscn => lscn.UserId,
                        r => r.UserId,
                       (lscn, r) => new { lscn, r })
                        .SelectMany(z => z.r.DefaultIfEmpty(),
                                 (a, b) => new DO_eSignature
                                 {
                                     UserID = a.lscn.UserId,
                                     LoginID = a.lscn.LoginId,
                                     LoginDesc =a.lscn.LoginDesc,
                                     EMailId=a.lscn.EMailId,
                                     IsUserAuthenticated = a.lscn.IsUserAuthenticated,
                                     IsUserDeactivated = a.lscn.IsUserDeactivated,
                                     ActiveStatus = a.lscn.ActiveStatus,
                                     ESignature = b != null ? b.ESignature : emptyByte,

                                 }
                        ).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_eSignature> GetUsereSignaturebyUserID(int UserID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    byte[] emptyByte = { };
                    var ds = db.GtEuuses
                        .Where(w => w.UserId == UserID)
                        .Select(r => new DO_eSignature
                        {
                            UserID = r.UserId,
                            ESignature = r.ESignature ?? emptyByte,
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
        public async Task<DO_ReturnParameter> InsertOrUpdateUsereSignature(DO_eSignature obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEuuse _esign = db.GtEuuses.Where(w => w.UserId == obj.UserID).FirstOrDefault();
                        if (_esign == null)
                        {
                            var esig = new GtEuuse
                            {
                                UserId = obj.UserID,
                                ESignature = obj.ESignature,
                                //ActiveStatus = obj.ActiveStatus,
                                ActiveStatus=true,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID,
                            };
                            db.GtEuuses.Add(esig);
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        }
                        else
                        {
                            _esign.ESignature = obj.ESignature;
                            //_esign.ActiveStatus = obj.ActiveStatus;
                            _esign.ModifiedBy = obj.UserID;
                            _esign.ModifiedOn = System.DateTime.Now;
                            _esign.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
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

    }
}
