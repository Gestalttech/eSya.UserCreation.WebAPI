using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.DO
{
    public class DO_Authorize
    {
        public int UserID { get; set; }
        public bool IsUserAuthenticated { get; set; }
        public string? RejectionReason { get; set; }
        public int ModifiedBy { get; set; }
        public string TerminalID { get; set; }
    }
}
