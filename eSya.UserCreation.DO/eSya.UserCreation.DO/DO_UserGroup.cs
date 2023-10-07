using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.UserCreation.DO
{
    public class DO_UserGroup
    {
        public List<string> dataList { get; set; }
        public int uG { get; set; }
        public int uT { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public string FormId { get; set; }
    }
}
