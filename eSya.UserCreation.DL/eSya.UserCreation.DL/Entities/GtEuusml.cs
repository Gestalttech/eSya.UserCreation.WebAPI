﻿using System;
using System.Collections.Generic;

namespace eSya.UserCreation.DL.Entities
{
    public partial class GtEuusml
    {
        public int UserId { get; set; }
        public int BusinessKey { get; set; }
        public int MenuKey { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }

        public virtual GtEuusbl GtEuusbl { get; set; } = null!;
    }
}
