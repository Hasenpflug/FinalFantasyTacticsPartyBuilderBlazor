using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.ViewModels
{
    public class UnitOverviewPanelViewModel
    {
        public int JobID { get; set; }

        public string JobName { get; set; }

        public int MaxHP { get; set; }

        public int MaxMP { get; set; }

        public int Position { get; set; }

        public string Gender { get; set; }

        public char[] HpDigits { get; set; }

        public char[] MpDigits { get; set; }
    }
}
