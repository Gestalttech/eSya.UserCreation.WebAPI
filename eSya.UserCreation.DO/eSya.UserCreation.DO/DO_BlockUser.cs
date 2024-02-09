using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.DO
{
    public class DO_BlockUser
    {
        public int UserID { get; set; }
        public bool BlockSignIn { get; set; }
        public int ModifiedBy { get; set; }
        public string TerminalID { get; set; }
    }
}
