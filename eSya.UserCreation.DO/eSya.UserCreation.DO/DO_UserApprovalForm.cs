using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.DO
{
    public class DO_UserApprovalForm
    {
        public int BusinessKey { get; set; }
        public int FormID { get; set; }
        public int UserID { get; set; }
        public int ApprovalLevel { get; set; }
        public bool ActiveStatus { get; set; }
        public string TerminalID { get; set; }
    }
}
