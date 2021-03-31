using System;
using System.Collections.Generic;

namespace FinalFantasyTacticsPartyBuilderBlazor.Models
{
    public partial class Job
    {
        public Job()
        {
            Abilities = new HashSet<Ability>();
            Jobitems = new HashSet<JobItem>();
            Jobprerequisites = new HashSet<JobPrerequisites>();
        }

        public int JobId { get; set; }
        public string PspName { get; set; }
        public string PsxName { get; set; }
        public string AbilitySetPsxName { get; set; }
        public string AbilitySetPspName { get; set; }
        public bool IsMaleOnly { get; set; }
        public bool IsFemaleOnly { get; set; }
        public int HPMultiplier { get; set; }
        public int HPGrowthConstant { get; set; }
        public int MPMultiplier { get; set; }
        public int MPGrowthConstant { get; set; }
        public int SpeedMulitplier { get; set; }
        public int SpeedGrowthConstant { get; set; }
        public int PhysicalAttackMultiplier { get; set; }
        public int PhysicalAttackGrowthConstant { get; set; }
        public int MagicalAttackMultiplier { get; set; }
        public int MagicalAttackGrowthConstant { get; set; }
        public int BaseMoveLength { get; set; }
        public int BaseJumpHeight { get; set; }
        public int BaseCombatEvasion { get; set; }

        public virtual ICollection<Ability> Abilities { get; set; }
        public virtual ICollection<JobItem> Jobitems { get; set; }
        public virtual ICollection<JobPrerequisites> Jobprerequisites { get; set; }
    }
}
