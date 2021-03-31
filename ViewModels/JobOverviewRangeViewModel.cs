using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.ViewModels
{
    public class JobOverviewRangeViewModel
    {
        public int HPGrowthConstantMin { get; set; }

        public int HPGrowthConstantMax { get; set; }

        public float HPGrowthConstantMultiplier { get; set; }

        public int MPGrowthConstantMin { get; set; }

        public int MPGrowthConstantMax { get; set; }

        public float MPGrowthConstantMultiplier { get; set; }

        public int SpeedGrowthConstantMax { get; set; }

        public int SpeedGrowthConstantMin { get; set; }

        public float SpeedGrowthConstantMultiplier { get; set; }

        public int PhysicalAttackGrowthConstantMax { get; set; }

        public int PhysicalAttackGrowthConstantMin { get; set; }

        public float PhysicalAttackGrowthConstantMultiplier { get; set; }

        public int MagicalAttackGrowthConstantMax { get; set; }

        public int MagicalAttackGrowthConstantMin { get; set; }

        public float MagicalAttackGrowthConstantMultiplier { get; set; }
    }
}
