using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.ViewModels
{
    public class UnitDetailsViewModel
    {
        public int RawHP { get; set; }

        public int RawMP { get; set; }

        public int RawSpeed { get; set; }

        public int RawPhysicalAttack { get; set; }

        public int RawMagicalAttack { get; set; }

        public int PrimaryAbilityJobID { get; set; }

        public string PrimaryAbilityName { get; set; }

        public int SecondaryAbilityJobID { get; set; }

        public string SecondaryAbilityName { get; set; }

        public int ReactionAbilityID { get; set; }

        public string ReactionAbilityName { get; set; }

        public int SupportAbilityID { get; set; }

        public string SupportAbilityName { get; set; }

        public int MovementAbilityID { get; set; }

        public string MovementAbilityName { get; set; }

        public int WeaponID { get; set; }

        public int ShieldID { get; set; }

        public int HeadID { get; set; }

        public int BodyID { get; set; }

        public int AccessoryID { get; set; }

        public string Move { get; set; }

        public string Jump { get; set; }

        public string Speed { get; set; }

        public ItemOverviewViewModel WeaponRight { get; set; }

        public ItemOverviewViewModel WeaponLeft { get; set; }

        public ItemOverviewViewModel Shield { get; set; }

        public ItemOverviewViewModel Head { get; set; }

        public ItemOverviewViewModel Body { get; set; }

        public ItemOverviewViewModel Accessory { get; set; }

        public UnitResistAndImmunityViewModel Resistances { get; set; }

        public string PhysicalAttackPower { get; set; }

        public string MagicalAttackPower { get; set; }

        public string PhysicalCharacterEvade { get; set; }

        public string PhysicalShieldEvade { get; set; }

        public string PhysicalAccessoryEvade { get; set; }

        public string MagicalCharacterEvade { get; set; }

        public string MagicalShieldEvade { get; set; }

        public string MagicalAccessoryEvade { get; set; }

        public UnitOverviewViewModel Unit { get; set; }
    }
}
