﻿using System;
using System.Collections.Generic;

namespace eSya.UserCreation.DL.Entities
{
    public partial class GtEcfmac
    {
        public GtEcfmac()
        {
            GtEcfmals = new HashSet<GtEcfmal>();
        }

        public int ActionId { get; set; }
        public string ActionDesc { get; set; } = null!;
        public bool ActiveStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }

        public virtual ICollection<GtEcfmal> GtEcfmals { get; set; }
    }
}
