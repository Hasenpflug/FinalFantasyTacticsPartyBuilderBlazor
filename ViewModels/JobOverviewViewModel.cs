using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.ViewModels
{
    public class JobOverviewViewModel
    {
        public int JobID { get; set; }

        public string FileName { get; set; }

        public string DisplayName { get; set; }

        public int GenderID { get; set; }

        public string Gender { get; set; }

        public int HPMultiplier { get; set; }

        public int HPGrowthConstant { get; set; }

        public int HPGrowthConstantLabel { get; set; }

        public int MPMultiplier { get; set; }

        public int MPGrowthConstant { get; set; }

        public int MPGrowthConstantLabel { get; set; }

        public int SpeedMulitplier { get; set; }

        public int SpeedGrowthConstant { get; set; }

        public int SpeedGrowthConstantLabel { get; set; }

        public int PhysicalAttackMultiplier { get; set; }

        public int PhysicalAttackGrowthConstant { get; set; }

        public int PhysicalAttackGrowthConstantLabel { get; set; }

        public int MagicalAttackMultiplier { get; set; }

        public int MagicalAttackGrowthConstant { get; set; }

        public int MagicalAttackGrowthConstantLabel { get; set; }

        public int BaseMoveLength { get; set; }

        public int BaseJumpHeight { get; set; }

        public int BaseCombatEvasion { get; set; }
    }
}
