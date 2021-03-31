using System;
using System.Collections.Generic;

namespace FinalFantasyTacticsPartyBuilderBlazor.Models
{
    public partial class Ability
    {
        public int AbilityId { get; set; }
        public int JobId { get; set; }
        public int AbilityType { get; set; }
        public string PspName { get; set; }
        public string PsxName { get; set; }
        public int? Mpcost { get; set; }
        public int? Range { get; set; }
        public int? Radius { get; set; }
        public int? Height { get; set; }
        public int? Speed { get; set; }
        public string ItemEquipIds { get; set; }
        public string Element { get; set; }
        public string Terrain { get; set; }
        public int? MoveBoost { get; set; }
        public int? JumpBoost { get; set; }
        public bool IsMaleOnly { get; set; }
        public bool IsFemaleOnly { get; set; }
        public bool MagicAttackModifier { get; set; }
        public bool PhysicalAttackModifier { get; set; }
        public bool PhysicalDefenceModifier { get; set; }
        public int HpPercentBoost { get; set; }
        public string AmountBoost { get; set; }
        public string AmountDamage { get; set; }
        public int JpNeededToLearnPsp { get; set; }
        public int? JpNeededToLearnPsx { get; set; }
        public bool CanBeReflected { get; set; }
        public bool UsedWithArithmetics { get; set; }
        public string DamageEquation { get; set; }
        public string HealingEquation { get; set; }
        public string EnergizeEquation { get; set; }
        public string SuccessRateEquation { get; set; }
        public string AddStatusEffect { get; set; }
        public string RemoveStatusEffect { get; set; }
        public int? Hprestored { get; set; }
        public string HealthDrainedEquation { get; set; }
        public string MpDrainedEquation { get; set; }
        public int? Mprestored { get; set; }
        public string Trigger { get; set; }
        public string Description { get; set; }
        public string Quote { get; set; }

        public virtual Job Job { get; set; }
    }
}
