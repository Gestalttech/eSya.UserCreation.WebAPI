using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.DO
{
    public class DO_UserCreation
    {
        public List<DO_UserMaster> L_UserMaster { get; set; }
        public List<DO_UserBusinessLink> L_UserBusinessLink { get; set; }
        public List<DO_UserMenuLink> L_UserMenuLink { get; set; }
        public List<DO_UserFormAction> L_UserFormAction { get; set; }
    }
    public class DO_UserGroupRole
    {
        public int BusinessKey { get; set; }
        public int UserGroup { get; set; }
        //public int UserType { get; set; }
        public int UserRole { get; set; }
        public int MenuKey { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }

        //public List<DO_UserFormAction> l_formAction { get; set; }
    }
    public class DO_UserMaster
    {
        public int UserID { get; set; }
        public string LoginID { get; set; } = null!;
        public string LoginDesc { get; set; } = null!;
        public byte[]? Photo { get; set; }
        public string? PhotoUrl { get; set; }
        public string EMailId { get; set; } = null!;
        public bool CreatePasswordInNextSignIn { get; set; }
        public int UnsuccessfulAttempt { get; set; }
        public DateTime? LoginAttemptDate { get; set; }
        public bool BlockSignIn { get; set; }
        public DateTime? LastPasswordUpdatedDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public bool IsUserAuthenticated { get; set; }
        public DateTime? UserAuthenticatedDate { get; set; }
        public bool IsUserDeactivated { get; set; }
        public DateTime? UserDeactivatedOn { get; set; }
        public string? DeactivationReason { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int CreatedBy { get; set; }
        public string TerminalID { get; set; }
        //For Image
        public string? userimage { get; set; }

        public List<DO_eSyaParameter> l_userparameter { get; set; }

        //public int UserID { get; set; }
        //public string LoginID { get; set; }
        //public string LoginDesc { get; set; }
        //public string Password { get; set; }
        //public int ISDCode { get; set; }
        //public bool AllowMobileLogin { get; set; }
        //public string MobileNumber { get; set; }
        //public string eMailID { get; set; }
        //public byte[] Photo { get; set; }
        //public string? PhotoURL { get; set; }
        //public byte[] DigitalSignature { get; set; }
        //public DateTime LastPasswordChangeDate { get; set; }
        //public DateTime? LastActivityDate { get; set; }
        //public string? OTPNumber { get; set; }
        //public DateTime OTPGeneratedDate { get; set; }
        //public int PreferredLanguage { get; set; }
        //public string? DeactivationReason { get; set; }
        //public bool ActiveStatus { get; set; }

        //public string? DSimage { get; set; }
    }
    public class DO_eSyaParameter
    {
        public int ParameterID { get; set; }
        public string? ParameterValue { get; set; }
        public bool ParmAction { get; set; }
        public decimal ParmValue { get; set; }
        public decimal ParmPerc { get; set; }
        public string? ParmDesc { get; set; }
        public bool ActiveStatus { get; set; }
    }

    public class DO_PreferredCulture
    {
        public string CultureCode { get; set; }
        public string CultureDescription { get; set; }
       
    }

    public class DO_UserBusinessLocation
    {
        public int UserID { get; set; }
        public int BusinessKey { get; set; }
        public bool AllowMtfy { get; set; }
        public string PreferredLanguage { get; set; } 
        public int Isdcode { get; set; }
        public string MobileNumber { get; set; } 
        public int IsdcodeWan { get; set; }
        public string WhatsappNumber { get; set; } 
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int CreatedBy { get; set; }
        public string TerminalID { get; set; }
    }

    public class DO_UserPhoto
    {
        public int UserID { get; set; }
        public byte[]? Photo { get; set; }
        public string? PhotoUrl { get; set; }
        public string? TerminalID { get; set; }
        public string? userimage { get; set; }
    }

    //not used right now
    public class DO_UserBusinessLink
    {
        public int UserID { get; set; }
        public int BusinessKey { get; set; }
        public string? LocationDescription { get; set; }
        public int UserGroup { get; set; }
        public string? UserGroupDesc { get; set; }
        public int UserType { get; set; }
        public string? UserTypeDesc { get; set; }
        public int IUStatus { get; set; }
        public bool AllowMTFY { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int CreatedBy { get; set; }
        public string TerminalId { get; set; }
    }

    public class DO_UserMenuLink
    {
        public int UserID { get; set; }
        public int BusinessKey { get; set; }
        public int MenuKey { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int CreatedBy { get; set; }
        public string TerminalId { get; set; }

        public List<DO_UserFormAction> l_formAction { get; set; }
    }

    
    public class DO_UserFormAction
    {
        public int UserID { get; set; }
        public int BusinessKey { get; set; }
        public int MenuKey { get; set; }
        public int ActionID { get; set; }
        public string? ActionDesc { get; set; }
        public string Active { get; set; }
        public bool ActiveStatus { get; set; }
        public string? FormId { get; set; }

        public int User_Id { get; set; }
        public string? TerminalId { get; set; }
    }
   

    public class DO_UserRoleMenuLink
    {
        public int UserId { get; set; }
        public int BusinessKey { get; set; }
        public int UserGroup { get; set; }
        public int UserType { get; set; }
        public int UserRole { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTill { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public string TerminalId { get; set; }
        public int CreatedBy { get; set; }
        //for displaying
        public string? UserGroupDesc { get; set; }
        public string? UserTypeDesc { get; set; }
        public string? UserRoleDesc { get; set; }

    }
    public class DO_UserType
    {
        public int UserTypeId { get; set; }
        public string UserTypeDesc { get; set; }
    }
    public class DO_UserRole
    {
        public int UserRoleId { get; set; }
        public string UserRoleDesc { get; set; }
    }
    public class DO_DoctorMaster
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
    //not used
}
