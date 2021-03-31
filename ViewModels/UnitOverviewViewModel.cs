using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.ViewModels
{
    public class UnitOverviewViewModel
    {
        public int UnitID { get; set; }

        public string UnitName { get; set; }

        public int Position { get; set; }

        public int Gender { get; set; }

        public string GenderName { get; set; }

        public int JobID { get; set; }

        public string JobName { get; set; }

        public string JobPortraitPath { get; set; }

        public int MaxHP { get; set; }

        public int MaxMP { get; set; }

        public int Level { get; set; }

        public int Experience { get; set; }

        public int Brave { get; set; }

        public int Faith { get; set; }

        public UnitOverviewHpMpViewModel AttributeDigits { get; set; }
    }
}
