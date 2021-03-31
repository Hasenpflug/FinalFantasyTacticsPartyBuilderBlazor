using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.ViewModels
{
    public enum ResistLevel
    {
        Weak = -1,
        Normal = 0,
        Halved,
        Negated,
        Absorbed
    }

    public class UnitResistAndImmunityViewModel
    {
        public static Dictionary<ResistLevel, string> resistLevelDescriptions = new Dictionary<ResistLevel, string>
        {
            { ResistLevel.Weak, "Double" }, { ResistLevel.Normal, "Normal" }, { ResistLevel.Negated, "Negate"}, { ResistLevel.Halved, "Half" }, { ResistLevel.Absorbed, "Absorbed" }
        };

        public string FireResist { get; set; }

        public string IceResist { get; set; }

        public string LightningResist { get; set; }

        public string WaterResist { get; set; }

        public string WindResist { get; set; }

        public string EarthResist { get; set; }

        public string HolyResist { get; set; }

        public string DarkResist { get; set; }

        public string PhysicalResist { get; set; }

        public string MagicalResist { get; set; }

        public bool IsBerserkImmune { get; set; }

        public bool IsSleepImmune { get; set; }

        public bool IsSilenceImmune { get; set; }

        public bool IsPoisonImmune { get; set; }

        public bool IsDoomImmune { get; set; }

        public bool IsTraitorImmune { get; set; }

        public bool IsKOImmune { get; set; }

        public bool IsStoneImmune { get; set; }

        public bool IsConfusedImmune { get; set; }

        public bool IsToadImmune { get; set; }

        public bool IsSlowImmune { get; set; }

        public bool IsStopImmune { get; set; }

        public bool IsDontMoveImmune { get; set; }

        public bool IsDontActImmune { get; set; }

        public bool IsBlindImmune { get; set; }

        public bool IsVampireImmune { get; set; }

        public bool IsUndeadImmune { get; set; }

        public bool IsCharmImmune { get; set; }
    }
}
