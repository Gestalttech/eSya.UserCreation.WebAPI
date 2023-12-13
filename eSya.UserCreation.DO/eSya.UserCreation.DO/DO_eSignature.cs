using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.DO
{
    public class DO_eSignature
    {
        public int UserID { get; set; }
        public byte[] ESignature { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public string TerminalID { get; set; }
        public string? LoginID { get; set; } = null!;
        public string? LoginDesc { get; set; } = null!;
        public string? EMailId { get; set; } = null!;
        public bool IsUserAuthenticated { get; set; }
        public bool IsUserDeactivated { get; set; }
        public string? usersignature { get; set; }
        public int CreatedBy { get; set; }

    }
}
