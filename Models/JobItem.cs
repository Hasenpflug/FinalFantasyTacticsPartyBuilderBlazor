using System;
using System.Collections.Generic;

namespace FinalFantasyTacticsPartyBuilderBlazor.Models
{
    public partial class JobItem
    {
        public int JobItemCategoryId { get; set; }
        public int JobId { get; set; }
        public int ItemCategoryId { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }
        public virtual Job Job { get; set; }
    }
}
