using FinalFantasyTacticsPartyBuilderBlazor.Models;
using FinalFantasyTacticsPartyBuilderBlazor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.Services
{
    public static class AttributeCalculatorService
    {
        public const int UNIT_STAT_NORMALIZER = 1638400;
        public static readonly Dictionary<string, string> elementSVGLocations = new Dictionary<string, string>
        {
            {"Fire", @"img/Elemental_Status_Icons/flame.svg" },
            {"Ice", @"img/Elemental_Status_Icons/gem.svg" },
            {"Water", @"img/Elemental_Status_Icons/drop.svg" },
            {"Lightning", @"img/Elemental_Status_Icons/flash.svg" },
            {"Wind", @"img/Elemental_Status_Icons/wind.svg" },
            {"Earth", @"img/Elemental_Status_Icons/landslide.svg" },
            {"Dark", @"img/Elemental_Status_Icons/dark-night.svg" },
            {"Holy", @"img/Elemental_Status_Icons/holy-star.svg" },
        };

        public static readonly Dictionary<string, string> effectSVGLocations = new Dictionary<string, string>
        {
            {"Berserk", @"img/Status_Ailment_Icons/berserk.svg" },
            {"Blind", @"img/Status_Ailment_Icons/blind.svg" },
            {"Charm", @"img/Status_Ailment_Icons/charm.svg" },
            {"Confuse", @"img/Status_Ailment_Icons/confused.svg" },
            {"Disable", @"img/Status_Ailment_Icons/disabled.svg" },
            {"Doom", @"img/Status_Ailment_Icons/doom.svg" },
            {"Frog", @"img/Status_Ailment_Icons/frog.svg" },
            {"Immobilize", @"img/Status_Ailment_Icons/immobile.svg" },
            {"KO", @"img/Status_Ailment_Icons/KO.svg" },
            {"Poison", @"img/Status_Ailment_Icons/poison.svg" },
            {"Silence", @"img/Status_Ailment_Icons/silence.svg" },
            {"Sleep", @"img/Status_Ailment_Icons/sleep.svg" },
            {"Slow", @"img/Status_Ailment_Icons/slow.svg" },
            {"Stone", @"img/Status_Ailment_Icons/stone.svg" },
            {"Stop", @"img/Status_Ailment_Icons/stop.svg" },
            {"Traitor", @"img/Status_Ailment_Icons/traitor.svg" },
            {"Vampire", @"img/Status_Ailment_Icons/vampire.svg" },
            {"Undead", @"img/Status_Ailment_Icons/zombie.svg" },
        };

        public static Dictionary<string, string> GetElementSVGLocations(string elementList)
        {
            if (String.IsNullOrEmpty(elementList))
            {
                return new Dictionary<string, string>();
            }

            return elementSVGLocations.Where(m => elementList.Contains(m.Key)).ToDictionary(k => k.Key, v => v.Value);
        }

        public static Dictionary<string, string> GetEffectSVGLocations(string effectList)
        {
            if (String.IsNullOrEmpty(effectList))
            {
                return new Dictionary<string, string>();
            }

            return effectSVGLocations.Where(m => effectList.Contains(m.Key)).ToDictionary(k => k.Key, v => v.Value);
        }

        public static UnitDetailsViewModel CalculateHPAndMP(Item headItem, Item bodyItem, UnitDetailsViewModel unit, Job job)
        {
            unit.Unit.MaxHP = ((job.HPMultiplier * unit.RawHP) / 1638400) + (headItem != null ? headItem.HPBonus ?? 0 : 0) + (bodyItem != null ?
                bodyItem.HPBonus ?? 0 : 0);
            unit.Unit.MaxMP = ((job.MPMultiplier * unit.RawMP) / 1638400) + (headItem != null ? headItem.MPBonus ?? 0 : 0) + (bodyItem != null ?
                bodyItem.MPBonus ?? 0 : 0);
            return unit;
        }

        public static UnitDetailsViewModel CalculateBasicStats(Item weaponItem1, Item weaponItem2, Item headItem, Item bodyItem, Item accessoryItem, UnitDetailsViewModel unit, Job job)
        {
            unit.Move = (job.BaseMoveLength + (unit.Accessory != null ? unit.Accessory.MoveBonus : 0)).ToString();
            unit.Jump = (job.BaseJumpHeight + (unit.Accessory != null ? unit.Accessory.JumpBonus : 0)).ToString();
            unit.Speed = ((job.SpeedMulitplier * unit.RawSpeed) / UNIT_STAT_NORMALIZER).ToString();
            unit.PhysicalAttackPower = (((job.PhysicalAttackMultiplier * unit.RawPhysicalAttack) / UNIT_STAT_NORMALIZER) + (weaponItem1 != null ? weaponItem1.PhysicalAttackBoost ?? 0 : 0) + (weaponItem2 != null ? weaponItem2.PhysicalAttackBoost ?? 0 : 0) +
                (headItem != null ? headItem.PhysicalAttackBoost ?? 0 : 0) + (bodyItem != null ? bodyItem.PhysicalAttackBoost ?? 0 : 0) + (accessoryItem != null ? accessoryItem.PhysicalAttackBoost ?? 0 : 0)).ToString("00");
            unit.MagicalAttackPower = (((job.MagicalAttackMultiplier * unit.RawMagicalAttack) / UNIT_STAT_NORMALIZER) + (weaponItem1 != null ? weaponItem1.MagicAttackBoost ?? 0 : 0) + (weaponItem2 != null ? weaponItem2.MagicAttackBoost ?? 0 : 0) +
                (headItem != null ? headItem.MagicAttackBoost ?? 0 : 0) + (bodyItem != null ? bodyItem.MagicAttackBoost ?? 0 : 0) +
                (accessoryItem != null ? accessoryItem.MagicAttackBoost ?? 0 : 0)).ToString("00");
            return unit;
        }

        public static UnitDetailsViewModel CalculateEvasionStats(Item shieldItem, Item accessoryItem, Job unitJob, UnitDetailsViewModel unit)
        {
            unit.PhysicalCharacterEvade = unitJob.BaseCombatEvasion.ToString("00");
            unit.MagicalCharacterEvade = unitJob.BaseCombatEvasion.ToString("00");
            unit.PhysicalShieldEvade = (shieldItem != null ? shieldItem.PhysicalEvade ?? 0 : 0).ToString("00");
            unit.MagicalShieldEvade = (shieldItem != null ? shieldItem.MagicalEvade ?? 0 : 0).ToString("00");
            unit.PhysicalAccessoryEvade = (accessoryItem != null ? accessoryItem.PhysicalEvade ?? 0 : 0).ToString("00");
            unit.MagicalAccessoryEvade = (accessoryItem != null ? accessoryItem.MagicalEvade ?? 0 : 0).ToString("00");
            return unit;
        }

        public static UnitDetailsViewModel CalculateResistancesAndImmunities(List<Item> characterItems, UnitDetailsViewModel unit)
        {
            Dictionary<string, int> resistLevels = elementSVGLocations.ToDictionary(m => m.Key, m => 0);
            unit.Resistances = new UnitResistAndImmunityViewModel();

            foreach (Item item in characterItems)
            {
                resistLevels["Fire"] += (int)(item?.ElementAbsorbed?.Contains("Fire") == true ? ResistLevel.Absorbed : item?.ElementNegated?.Contains("Fire") == true ? ResistLevel.Negated :
                    item?.ElementHalved?.Contains("Fire") == true ? ResistLevel.Halved : item?.ElementWeakness?.Contains("Fire") == true ? ResistLevel.Weak : ResistLevel.Normal);

                resistLevels["Ice"] += (int)(item?.ElementAbsorbed?.Contains("Ice") == true ? ResistLevel.Absorbed : item?.ElementNegated?.Contains("Ice") == true ? ResistLevel.Negated :
                    item?.ElementHalved?.Contains("Ice") == true ? ResistLevel.Halved : item?.ElementWeakness?.Contains("Ice") == true ? ResistLevel.Weak : ResistLevel.Normal);

                resistLevels["Lightning"] += (int)(item?.ElementAbsorbed?.Contains("Lightning") == true ? ResistLevel.Absorbed : item?.ElementNegated?.Contains("Lightning") == true ? ResistLevel.Negated :
                    item?.ElementHalved?.Contains("Lightning") == true ? ResistLevel.Halved : item?.ElementWeakness?.Contains("Lightning") == true ? ResistLevel.Weak : ResistLevel.Normal);

                resistLevels["Water"] += (int)(item?.ElementAbsorbed?.Contains("Water") == true ? ResistLevel.Absorbed : item?.ElementNegated?.Contains("Water") == true ? ResistLevel.Negated :
                    item?.ElementHalved?.Contains("Water") == true ? ResistLevel.Halved : item?.ElementWeakness?.Contains("Water") == true ? ResistLevel.Weak : ResistLevel.Normal);

                resistLevels["Wind"] += (int)(item?.ElementAbsorbed?.Contains("Wind") == true ? ResistLevel.Absorbed : item?.ElementNegated?.Contains("Wind") == true ? ResistLevel.Negated :
                    item?.ElementHalved?.Contains("Wind") == true ? ResistLevel.Halved : item?.ElementWeakness?.Contains("Wind") == true ? ResistLevel.Weak : ResistLevel.Normal);

                resistLevels["Earth"] += (int)(item?.ElementAbsorbed?.Contains("Earth") == true ? ResistLevel.Absorbed : item?.ElementNegated?.Contains("Earth") == true ? ResistLevel.Negated :
                    item?.ElementHalved?.Contains("Earth") == true ? ResistLevel.Halved : item?.ElementWeakness?.Contains("Earth") == true ? ResistLevel.Weak : ResistLevel.Normal);

                resistLevels["Holy"] += (int)(item?.ElementAbsorbed?.Contains("Holy") == true ? ResistLevel.Absorbed : item?.ElementNegated?.Contains("Holy") == true ? ResistLevel.Negated :
                    item?.ElementHalved?.Contains("Holy") == true ? ResistLevel.Halved : item?.ElementWeakness?.Contains("Holy") == true ? ResistLevel.Weak : ResistLevel.Normal);

                resistLevels["Dark"] += (int)(item?.ElementAbsorbed?.Contains("Dark") == true ? ResistLevel.Absorbed : item?.ElementNegated?.Contains("Dark") == true ? ResistLevel.Negated :
                    item?.ElementHalved?.Contains("Dark") == true ? ResistLevel.Halved : item?.ElementWeakness?.Contains("Dark") == true ? ResistLevel.Weak : ResistLevel.Normal);

                unit.Resistances.PhysicalResist = "Normal";
                unit.Resistances.MagicalResist = "Normal";

                //unit.Resistances.IsBerserkImmune = !unit.Resistances.IsBerserkImmune ? item?.ImmuneStatusEffect?.Contains("Berserk") == true ? true : false : true;
                //unit.Resistances.IsBlindImmune = !unit.Resistances.IsBlindImmune ? item?.ImmuneStatusEffect?.Contains("Blind") == true ? true : false : true;
                //unit.Resistances.IsCharmImmune = !unit.Resistances.IsCharmImmune ? item?.ImmuneStatusEffect?.Contains("Charm") == true ? true : false : true;
                //unit.Resistances.IsConfusedImmune = !unit.Resistances.IsConfusedImmune ? item?.ImmuneStatusEffect?.Contains("Confused") == true ? true : false : true;
                //unit.Resistances.IsDontActImmune = !unit.Resistances.IsDontActImmune ? item?.ImmuneStatusEffect?.Contains("Disable") == true ? true : false : true;
                //unit.Resistances.IsDontMoveImmune = !unit.Resistances.IsDontMoveImmune ? item?.ImmuneStatusEffect?.Contains("Immobilize") == true ? true : false : true;
                //unit.Resistances.IsDoomImmune = !unit.Resistances.IsDoomImmune ? item?.ImmuneStatusEffect?.Contains("Doom") == true ? true : false : true;
                //unit.Resistances.IsKOImmune = !unit.Resistances.IsKOImmune ? item?.ImmuneStatusEffect?.Contains("KO") == true ? true : false : true;
                //unit.Resistances.IsPoisonImmune = !unit.Resistances.IsPoisonImmune ? item?.ImmuneStatusEffect?.Contains("Poison") == true ? true : false : true;
                //unit.Resistances.IsSilenceImmune = !unit.Resistances.IsSilenceImmune ? item?.ImmuneStatusEffect?.Contains("Silence") == true ? true : false : true;
                //unit.Resistances.IsSleepImmune = !unit.Resistances.IsSleepImmune ? item?.ImmuneStatusEffect?.Contains("Sleep") == true ? true : false : true;
                //unit.Resistances.IsSlowImmune = !unit.Resistances.IsSlowImmune ? item?.ImmuneStatusEffect?.Contains("Slow") == true ? true : false : true;
                //unit.Resistances.IsStoneImmune = !unit.Resistances.IsStoneImmune ? item?.ImmuneStatusEffect?.Contains("Stone") == true ? true : false : true;
                //unit.Resistances.IsStopImmune = !unit.Resistances.IsStopImmune ? item?.ImmuneStatusEffect?.Contains("Stop") == true ? true : false : true;
                //unit.Resistances.IsToadImmune = !unit.Resistances.IsToadImmune ? item?.ImmuneStatusEffect?.Contains("Toad") == true ? true : false : true;
                //unit.Resistances.IsTraitorImmune = !unit.Resistances.IsTraitorImmune ? item?.ImmuneStatusEffect?.Contains("Traitor") == true ? true : false : true;
                //unit.Resistances.IsUndeadImmune = !unit.Resistances.IsUndeadImmune ? item?.ImmuneStatusEffect?.Contains("Undead") == true ? true : false : true;
                //unit.Resistances.IsVampireImmune = !unit.Resistances.IsVampireImmune ? item?.ImmuneStatusEffect?.Contains("Vampire") == true ? true : false : true;
                unit.Resistances.IsBerserkImmune = true;
                unit.Resistances.IsBlindImmune = true;
                unit.Resistances.IsCharmImmune = true;
                unit.Resistances.IsConfusedImmune = true;
                unit.Resistances.IsDontActImmune = true;
                unit.Resistances.IsDontMoveImmune = true;
                unit.Resistances.IsDoomImmune = true;
                unit.Resistances.IsKOImmune = true;
                unit.Resistances.IsPoisonImmune = true;
                unit.Resistances.IsSilenceImmune = true;
                unit.Resistances.IsSleepImmune = true;
                unit.Resistances.IsSlowImmune = true;
                unit.Resistances.IsStoneImmune = true;
                unit.Resistances.IsStopImmune = true;
                unit.Resistances.IsToadImmune = true;
                unit.Resistances.IsTraitorImmune = true;
                unit.Resistances.IsUndeadImmune = true;
                unit.Resistances.IsVampireImmune = true;
            }

            unit.Resistances.FireResist = UnitResistAndImmunityViewModel.resistLevelDescriptions[(ResistLevel)resistLevels["Fire"]];
            unit.Resistances.IceResist = UnitResistAndImmunityViewModel.resistLevelDescriptions[(ResistLevel)resistLevels["Ice"]];
            unit.Resistances.LightningResist = UnitResistAndImmunityViewModel.resistLevelDescriptions[(ResistLevel)resistLevels["Lightning"]];
            unit.Resistances.WaterResist = UnitResistAndImmunityViewModel.resistLevelDescriptions[(ResistLevel)resistLevels["Water"]];
            unit.Resistances.WindResist = UnitResistAndImmunityViewModel.resistLevelDescriptions[(ResistLevel)resistLevels["Wind"]];
            unit.Resistances.EarthResist = UnitResistAndImmunityViewModel.resistLevelDescriptions[(ResistLevel)resistLevels["Earth"]];
            unit.Resistances.HolyResist = UnitResistAndImmunityViewModel.resistLevelDescriptions[(ResistLevel)resistLevels["Holy"]];
            unit.Resistances.DarkResist = UnitResistAndImmunityViewModel.resistLevelDescriptions[(ResistLevel)resistLevels["Dark"]];
            unit.Resistances.PhysicalResist = "Normal";
            unit.Resistances.MagicalResist = "Normal";

            return unit;
        }
    }
}
