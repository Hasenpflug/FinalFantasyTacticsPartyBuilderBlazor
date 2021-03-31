using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.ViewModels
{
    public class UnitDismissViewModel
    {
        public int JobID { get; set; }

        public int Position { get; set; }

        public string UnitName { get; set; }

        public string JobName { get; set; }

        public string JobPortraitPath { get; set; }

        public int Gender { get; set; }

        public string GenderName { get; set; }

        public string Quote { get; set; }
    }
}
