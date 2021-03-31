using System;
using System.Collections.Generic;

namespace FinalFantasyTacticsPartyBuilderBlazor.Models
{
    public partial class JobPrerequisites
    {
        public int JobPrerequisitesId { get; set; }
        public int JobId { get; set; }
        public int JobRequiredId { get; set; }
        public int JobLevelRequiredPsp { get; set; }
        public int JobLevelRequiredPsx { get; set; }

        public virtual Job Job { get; set; }
    }
}
