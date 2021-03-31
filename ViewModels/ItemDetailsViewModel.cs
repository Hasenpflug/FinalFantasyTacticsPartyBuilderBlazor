using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.ViewModels
{
    public class ItemDetailsViewModel
    {
        public int ItemID { get; set; }

        public string Name { get; set; }

        public string AttackPower { get; set; }

        public string HPBonus { get; set; }

        public string MPBonus { get; set; }

        public string HitPercentage { get; set; }

        public string PhysicalEvade { get; set; }

        public string MagicalEvade { get; set; }

        public string Description { get; set; }

        public Dictionary<string, string> SpellEffect { get; set; }

        public Dictionary<string, string> HitStatusEffect { get; set; }

        public Dictionary<string, string> RemoveStatusEffect { get; set; }

        public Dictionary<string, string> ImmuneStatusEffect { get; set; }

        public Dictionary<string, string> EquipStatusEffect { get; set; }

        public string PhysicalAttackBoost { get; set; }

        public string MagicAttackBoost { get; set; }

        public string SpeedBoost { get; set; }

        public string MoveBoost { get; set; }

        public string JumpBoost { get; set; }

        public Dictionary<string, string> Element { get; set; }

        public Dictionary<string, string> ElementAbsorbed { get; set; }

        public Dictionary<string, string> ElementBoosted { get; set; }

        public Dictionary<string, string> ElementHalved { get; set; }

        public Dictionary<string, string> ElementNegated { get; set; }

        public Dictionary<string, string> ElementWeakness { get; set; }

        public string AcquiredViaMode { get; set; }

        public string AcquiredViaLocation { get; set; }

        public string AcquiredViaPoach { get; set; }

        public string AcquiredViaSteal { get; set; }

        public string AcquiredViaTreasureHunt { get; set; }

        public string AcquiredViaCatch { get; set; }

        public string AcquiredViaInitialEquip { get; set; }

        public string ImagePath { get; set; }

        public int Cost { get; set; }

        public string ItemCategoryName { get; set; }

        public bool CanDualWield { get; set; }

        public bool CanDoubleHand { get; set; }
    }
}
