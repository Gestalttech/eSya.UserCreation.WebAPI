﻿using System;
using System.Collections.Generic;

namespace eSya.UserCreation.DL.Entities
{
    public partial class GtEuusph
    {
        public int UserId { get; set; }
        public int SerialNumber { get; set; }
        public byte[] EPasswd { get; set; } = null!;
        public DateTime LastPasswdChangedDate { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}
