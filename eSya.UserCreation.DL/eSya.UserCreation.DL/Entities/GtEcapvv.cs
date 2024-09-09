using System;
using System.Collections.Generic;

namespace eSya.UserCreation.DL.Entities
{
    public partial class GtEcapvv
    {
        public int BusinessKey { get; set; }
        public int FormId { get; set; }
        public int ApprovalLevel { get; set; }
        public decimal ValueFrom { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public decimal ValueTo { get; set; }
        public DateTime? EffectiveTill { get; set; }
        public bool ActiveStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}
