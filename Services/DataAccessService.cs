using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalFantasyTacticsPartyBuilderBlazor.Models;
using FinalFantasyTacticsPartyBuilderBlazor.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalFantasyTacticsPartyBuilderBlazor.Services
{
    public class DataAccessService
    {
        public List<UnitOverviewPanelViewModel> GetUnitOverviewPanelData(List<UnitOverviewPanelViewModel> units)
        {
            if (units != null)
            {
                foreach (UnitOverviewPanelViewModel unit in units)
                {
                    unit.JobName = Enum.GetName(typeof(Enums.Jobs), unit.JobID);
                    unit.JobName = unit.JobName.Contains("Onion") ? "OnionKnight" : unit.JobName;
                    unit.HpDigits = unit.MaxHP.ToString().ToCharArray();
                    unit.MpDigits = unit.MaxMP.ToString().ToCharArray();
                }
            }

            return units;
        }

        public async Task<List<JobOverviewViewModel>> GetJobSelectionDataAsync(int genderID)
        {
            List<JobOverviewViewModel> viewModels;
            JobOverviewRangeViewModel rangeViewModel;
            string genderName = Enum.GetName(typeof(Enums.Gender), genderID);

            using (FFTContext context = new FFTContext())
            {
                viewModels = await context.Jobs.Where(m => genderID == 0 ? !m.IsFemaleOnly : !m.IsMaleOnly).Select(m => new JobOverviewViewModel
                {
                    JobID = m.JobId,
                    FileName = (m.PspName.Contains("(") ? m.PspName.Remove(m.PspName.IndexOf("(")) : m.PspName).Replace(" ", ""),
                    DisplayName = m.PspName,
                    GenderID = genderID,
                    Gender = genderName,
                    HPMultiplier = m.HPMultiplier,
                    HPGrowthConstant = m.HPGrowthConstant,
                    HPGrowthConstantLabel = m.HPGrowthConstant,
                    MPMultiplier = m.MPMultiplier,
                    MPGrowthConstant = m.MPGrowthConstant,
                    MPGrowthConstantLabel = m.MPGrowthConstant,
                    SpeedMulitplier = m.SpeedMulitplier,
                    SpeedGrowthConstant = m.SpeedGrowthConstant,
                    SpeedGrowthConstantLabel = m.SpeedGrowthConstant,
                    PhysicalAttackMultiplier = m.PhysicalAttackMultiplier,
                    PhysicalAttackGrowthConstant = m.PhysicalAttackGrowthConstant,
                    PhysicalAttackGrowthConstantLabel = m.PhysicalAttackGrowthConstant,
                    MagicalAttackMultiplier = m.MagicalAttackMultiplier,
                    MagicalAttackGrowthConstant = m.MagicalAttackGrowthConstant,
                    MagicalAttackGrowthConstantLabel = m.MagicalAttackGrowthConstant,
                    BaseMoveLength = m.BaseMoveLength,
                    BaseJumpHeight = m.BaseJumpHeight,
                    BaseCombatEvasion = m.BaseCombatEvasion
                }).ToListAsync();

                rangeViewModel = new JobOverviewRangeViewModel
                {
                    HPGrowthConstantMax = context.Jobs.Max(c => c.HPGrowthConstant),
                    HPGrowthConstantMin = context.Jobs.Min(c => c.HPGrowthConstant),
                    MPGrowthConstantMax = context.Jobs.Max(c => c.MPGrowthConstant),
                    MPGrowthConstantMin = context.Jobs.Min(c => c.MPGrowthConstant),
                    SpeedGrowthConstantMax = context.Jobs.Max(c => c.SpeedGrowthConstant),
                    SpeedGrowthConstantMin = context.Jobs.Min(c => c.SpeedGrowthConstant),
                    PhysicalAttackGrowthConstantMax = context.Jobs.Max(c => c.PhysicalAttackGrowthConstant),
                    PhysicalAttackGrowthConstantMin = context.Jobs.Min(c => c.PhysicalAttackGrowthConstant),
                    MagicalAttackGrowthConstantMax = context.Jobs.Max(c => c.MagicalAttackGrowthConstant),
                    MagicalAttackGrowthConstantMin = context.Jobs.Min(c => c.MagicalAttackGrowthConstant)
                };

                int commonDenominator = (new int[] { rangeViewModel.HPGrowthConstantMax, rangeViewModel.MPGrowthConstantMax, rangeViewModel.SpeedGrowthConstantMax,
                    rangeViewModel.PhysicalAttackGrowthConstantMax, rangeViewModel.MagicalAttackGrowthConstantMax}).Max();
                rangeViewModel.HPGrowthConstantMultiplier = (float)commonDenominator / (float)rangeViewModel.HPGrowthConstantMax;
                rangeViewModel.MPGrowthConstantMultiplier = (float)commonDenominator / (float)rangeViewModel.MPGrowthConstantMax;
                rangeViewModel.SpeedGrowthConstantMultiplier = (float)commonDenominator / (float)rangeViewModel.SpeedGrowthConstantMax;
                rangeViewModel.PhysicalAttackGrowthConstantMultiplier = (float)commonDenominator / (float)rangeViewModel.PhysicalAttackGrowthConstantMax;
                rangeViewModel.MagicalAttackGrowthConstantMultiplier = (float)commonDenominator / (float)rangeViewModel.MagicalAttackGrowthConstantMax;

                foreach (JobOverviewViewModel viewModel in viewModels)
                {
                    viewModel.HPGrowthConstant = (int)((rangeViewModel.HPGrowthConstantMax + rangeViewModel.HPGrowthConstantMin - viewModel.HPGrowthConstant) *
                        rangeViewModel.HPGrowthConstantMultiplier);
                    viewModel.MPGrowthConstant = (int)((rangeViewModel.MPGrowthConstantMax + rangeViewModel.MPGrowthConstantMin - viewModel.MPGrowthConstant) *
                        rangeViewModel.MPGrowthConstantMultiplier);
                    viewModel.SpeedGrowthConstant = (int)((rangeViewModel.SpeedGrowthConstantMax + rangeViewModel.SpeedGrowthConstantMin - viewModel.SpeedGrowthConstant) *
                        rangeViewModel.SpeedGrowthConstantMultiplier);
                    viewModel.PhysicalAttackGrowthConstant = (int)((rangeViewModel.PhysicalAttackGrowthConstantMax + rangeViewModel.PhysicalAttackGrowthConstantMin -
                        viewModel.PhysicalAttackGrowthConstant) * rangeViewModel.PhysicalAttackGrowthConstantMultiplier);
                    viewModel.MagicalAttackGrowthConstant = (int)((rangeViewModel.MagicalAttackGrowthConstantMax + rangeViewModel.MagicalAttackGrowthConstantMin -
                        viewModel.MagicalAttackGrowthConstant) * rangeViewModel.MagicalAttackGrowthConstantMultiplier);
                }
            }

            return viewModels;
        }

        public UnitOverviewViewModel GetUnitOverviewData(UnitOverviewViewModel unit)
        {
            unit.JobName = Enum.GetName(typeof(Enums.Jobs), unit.JobID);
            unit.JobName = unit.JobName.Contains("Onion") ? "Onion Knight" : unit.JobName;
            unit.JobPortraitPath = String.Format("/img/Jobs/{0}_{1}_Portrait.png", unit.JobName.Contains("Onion") ? "OnionKnight" : unit.JobName, unit.GenderName);
            unit.JobName = string.Concat(unit.JobName.Select(m => Char.IsUpper(m) ? " " + m : m.ToString())).Trim();
            unit.GenderName = Enum.GetName(typeof(Enums.Gender), unit.Gender);

            unit.AttributeDigits = new UnitOverviewHpMpViewModel
            {
                HpDigits = unit.MaxHP.ToString().ToCharArray(),
                MpDigits = unit.MaxMP.ToString().ToCharArray(),
                ExperienceDigits = unit.Experience < 10 ? ("0" + unit.Experience.ToString()).ToCharArray() : unit.Experience.ToString().ToCharArray(),
                LevelDigits = unit.Level < 10 ? ("0" + unit.Level.ToString()).ToCharArray() : unit.Level.ToString().ToCharArray(),
                PositionDigits = unit.Position < 10 ? ("0" + unit.Position.ToString()).ToCharArray() : unit.Position.ToString().ToCharArray()
            };

            return unit;
        }

        public async Task<UnitDismissViewModel> GetUnitDismissDataAsync(UnitDismissViewModel unit)
        {
            using (FFTContext context = new FFTContext())
            {
                Random random = new Random();
                int quoteCount = context.Quotes.Count(m => m.Gender == unit.Gender);
                unit.Quote = await context.Quotes.Skip(random.Next(quoteCount)).Select(m => m.Text).FirstAsync();
            }

            unit.JobName = Enum.GetName(typeof(Enums.Jobs), unit.JobID);
            unit.JobName = unit.JobName.Contains("Onion") ? "Onion Knight" : unit.JobName;
            unit.JobPortraitPath = String.Format("/img/Jobs/{0}_{1}_Portrait.png", unit.JobName.Contains("Onion") ? "OnionKnight" : unit.JobName, unit.GenderName);

            return unit;
        }

        public async Task<UnitDetailsViewModel> GetUnitStatsDetailDataAsync(UnitDetailsViewModel unit, int? ItemId = null)
        {
            Item weaponItem1 = null, weaponItem2 = null, headItem = null, bodyItem = null, accessoryItem = null;
            List<Item> items = new List<Item>();

            unit.Unit.JobName = Enum.GetName(typeof(Enums.Jobs), unit.Unit.JobID);
            unit.Unit.JobName = unit.Unit.JobName.Contains("Onion") ? "Onion Knight" : unit.Unit.JobName;
            unit.Unit.JobPortraitPath = String.Format("/img/Jobs/{0}_{1}_Portrait.png", unit.Unit.JobName.Contains("Onion") ? "OnionKnight" : unit.Unit.JobName, unit.Unit.GenderName);
            unit.Unit.JobName = string.Concat(unit.Unit.JobName.Select(m => Char.IsUpper(m) ? " " + m : m.ToString())).Trim();
            unit.Unit.GenderName = Enum.GetName(typeof(Enums.Gender), unit.Unit.Gender);

            using (FFTContext context = new FFTContext())
            {
                if (ItemId != null)
                {
                    int categoryListLength = Enum.GetNames(typeof(Enums.EquipmentCategoriesList)).Length;
                    Item selectedItem = await context.Items.SingleAsync(m => m.ItemId == ItemId);

                    switch ((Enums.EquipmentCategoriesList)selectedItem.ItemCategory.EquipmentCategoryId)
                    {
                        case Enums.EquipmentCategoriesList.Weapon:
                            unit.WeaponID = selectedItem.ItemId;
                            break;
                        case Enums.EquipmentCategoriesList.Shield:
                            unit.ShieldID = selectedItem.ItemId;
                            break;
                        case Enums.EquipmentCategoriesList.Helmet:
                            unit.HeadID = selectedItem.ItemId;
                            break;
                        case Enums.EquipmentCategoriesList.Armor:
                            unit.BodyID = selectedItem.ItemId;
                            break;
                        case Enums.EquipmentCategoriesList.Accessory:
                            unit.AccessoryID = selectedItem.ItemId;
                            break;
                    }
                }

                if (unit.WeaponID != default(int))
                {
                    weaponItem1 = await context.Items.SingleAsync(m => m.ItemId == unit.WeaponID);
                    unit.WeaponRight = new ItemOverviewViewModel
                    {
                        WeaponPower = weaponItem1.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Weapon ? weaponItem1.AttackPower.Value.ToString("D3") : "000",
                        WeaponHit = weaponItem1.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Weapon ? weaponItem1.HitPercentage.Value.ToString("D3") : "000",
                        ShieldPhysicalEvade = weaponItem1.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Shield ? weaponItem1.PhysicalEvade.Value.ToString("D2") : "00",
                        ShieldMagicalEvade = weaponItem1.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Shield ? weaponItem1.MagicalEvade.Value.ToString("D2") : "00",
                        PhysicalAttackPower = weaponItem1.PhysicalAttackBoost.HasValue ? weaponItem1.PhysicalAttackBoost.Value.ToString("D2") : "00",
                        MagicalAttackPower = weaponItem1.MagicAttackBoost.HasValue ? weaponItem1.MagicAttackBoost.Value.ToString("D2") : "00",
                        ImagePath = weaponItem1.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Weapon ? @"/img/Item_Icons/Weapons/" +
                        weaponItem1.IconFileName : @"/img/Item_Icons/Armour/" + weaponItem1.IconFileName,
                        Name = weaponItem1.PspName,
                    };

                    items.Add(weaponItem1);
                }
                else
                {
                    unit.WeaponRight = new ItemOverviewViewModel();
                }

                if (unit.ShieldID != default && weaponItem1.ItemCategory.IsTwoHandOnly)
                {
                    weaponItem2 = await context.Items.SingleAsync(m => m.ItemId == unit.ShieldID);
                    unit.WeaponLeft = new ItemOverviewViewModel
                    {
                        WeaponPower = weaponItem2 != null ? weaponItem2.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Weapon ?
                            weaponItem2.AttackPower.HasValue ? weaponItem2.AttackPower.Value.ToString("D3") : "000" : "000" : "000",
                        WeaponHit = weaponItem2 != null ? weaponItem2.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Weapon ?
                            weaponItem2.HitPercentage.HasValue ? weaponItem2.HitPercentage.Value.ToString("D3") : "000" : "000" : "000",
                        ShieldPhysicalEvade = weaponItem2 != null ? weaponItem2.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Shield ?
                            weaponItem2.PhysicalEvade.HasValue ? weaponItem2.PhysicalEvade.Value.ToString("D3") : "00" : "00" : "00",
                        ShieldMagicalEvade = weaponItem2 != null ? weaponItem2.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Shield ?
                            weaponItem2.MagicalEvade.HasValue ? weaponItem2.MagicalEvade.Value.ToString("D3") : "00" : "00" : "00",
                        PhysicalAttackPower = weaponItem2 != null ? weaponItem2.PhysicalAttackBoost.HasValue ? weaponItem2.PhysicalAttackBoost.Value.ToString("D2") : "00" : "00",
                        MagicalAttackPower = weaponItem2 != null ? weaponItem2.MagicAttackBoost.HasValue ? weaponItem2.MagicAttackBoost.Value.ToString("D2") : "00" : "00",
                        ImagePath = weaponItem2 != null ? weaponItem2.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Weapon ? @"/img/Item_Icons/Weapons/" +
                        weaponItem2.IconFileName : @"/img/Item_Icons/Armour/" + weaponItem2.IconFileName : "",
                        Name = weaponItem2 != null ? weaponItem2.PspName : "",
                    };

                    items.Add(weaponItem2);
                }
                else
                {
                    unit.WeaponLeft = new ItemOverviewViewModel();
                }

                if (unit.HeadID != default(int))
                {
                    headItem = await context.Items.SingleAsync(m => m.ItemId == unit.HeadID);
                    unit.Head = new ItemOverviewViewModel
                    {
                        HPBonus = headItem.HPBonus ?? 0,
                        MPBonus = headItem.MPBonus ?? 0,
                        PhysicalAttackPower = headItem.PhysicalAttackBoost.HasValue ? headItem.PhysicalAttackBoost.Value.ToString("D2") : "00",
                        MagicalAttackPower = headItem.MagicAttackBoost.HasValue ? headItem.MagicAttackBoost.Value.ToString("D2") : "00",
                        SpeedBonus = headItem.SpeedBoost ?? 0,
                        ImagePath = @"/img/Item_Icons/Armour/" + headItem.IconFileName,
                        Name = headItem.PspName,
                    };

                    items.Add(headItem);
                }
                else
                {
                    unit.Head = new ItemOverviewViewModel();
                }

                if (unit.BodyID != default(int))
                {
                    bodyItem = await context.Items.SingleAsync(m => m.ItemId == unit.BodyID);
                    unit.Body = new ItemOverviewViewModel
                    {
                        HPBonus = bodyItem.HPBonus ?? 0,
                        MPBonus = bodyItem.MPBonus ?? 0,
                        PhysicalAttackPower = bodyItem.PhysicalAttackBoost.HasValue ? bodyItem.PhysicalAttackBoost.Value.ToString("D2") : "00",
                        MagicalAttackPower = bodyItem.MagicAttackBoost.HasValue ? bodyItem.MagicAttackBoost.Value.ToString("D2") : "00",
                        SpeedBonus = bodyItem.SpeedBoost ?? 0,
                        ImagePath = @"/img/Item_Icons/Armour/" + context.Items.Single(m => m.ItemId == unit.BodyID).IconFileName,
                        Name = context.Items.Single(m => m.ItemId == unit.BodyID).PspName,
                    };

                    items.Add(bodyItem);
                }
                else
                {
                    unit.Body = new ItemOverviewViewModel();
                }

                if (unit.AccessoryID != default(int))
                {
                    accessoryItem = await context.Items.SingleAsync(m => m.ItemId == unit.AccessoryID);
                    unit.Accessory = new ItemOverviewViewModel
                    {
                        ItemCategoryID = accessoryItem.ItemCategoryId,
                        MoveBonus = accessoryItem.MoveBoost ?? 0,
                        JumpBonus = accessoryItem.JumpBoost ?? 0,
                        SpeedBonus = accessoryItem.SpeedBoost ?? 0,
                        PhysicalAttackPower = accessoryItem.PhysicalAttackBoost.HasValue ? accessoryItem.PhysicalAttackBoost.Value.ToString() : "00",
                        MagicalAttackPower = accessoryItem.MagicAttackBoost.HasValue ? accessoryItem.MagicAttackBoost.Value.ToString() : "00",
                        AccessoryPhysicalEvade = accessoryItem.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Accessory ? accessoryItem.PhysicalEvade.HasValue ?
                            accessoryItem.PhysicalEvade.Value.ToString("D3") : "00" : "00",
                        AccessoryMagicalEvade = accessoryItem.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Accessory ? accessoryItem.MagicalEvade.HasValue ?
                            accessoryItem.MagicalEvade.Value.ToString("D3") : "00" : "00",
                        ImagePath = @"/img/Item_Icons/Accessories/" + context.Items.Single(m => m.ItemId == unit.AccessoryID).IconFileName,
                        Name = context.Items.Single(m => m.ItemId == unit.AccessoryID).PspName,
                    };

                    items.Add(accessoryItem);
                }
                else
                {
                    unit.Accessory = new ItemOverviewViewModel();
                }

                Job unitJob = await context.Jobs.SingleAsync(m => m.JobId == unit.Unit.JobID);
                unit = AttributeCalculatorService.CalculateHPAndMP(headItem, bodyItem, unit, unitJob);
                unit = AttributeCalculatorService.CalculateBasicStats(weaponItem1, weaponItem2, headItem, bodyItem, accessoryItem, unit, unitJob);
                unit = AttributeCalculatorService.CalculateEvasionStats(weaponItem2, accessoryItem, unitJob, unit);
                unit.PrimaryAbilityJobID = unitJob.JobId;
                unit.PrimaryAbilityName = unitJob.AbilitySetPspName;
            }

            unit.Unit.AttributeDigits = new UnitOverviewHpMpViewModel
            {
                HpDigits = unit.Unit.MaxHP.ToString().ToCharArray(),
                MpDigits = unit.Unit.MaxMP.ToString().ToCharArray(),
                ExperienceDigits = unit.Unit.Experience < 10 ? ("0" + unit.Unit.Experience.ToString()).ToCharArray() : unit.Unit.Experience.ToString().ToCharArray(),
                LevelDigits = unit.Unit.Level < 10 ? ("0" + unit.Unit.Level.ToString()).ToCharArray() : unit.Unit.Level.ToString().ToCharArray(),
                PositionDigits = unit.Unit.Position < 10 ? ("0" + unit.Unit.Position.ToString()).ToCharArray() : unit.Unit.Position.ToString().ToCharArray()
            };

            unit = AttributeCalculatorService.CalculateResistancesAndImmunities(items, unit);
            return unit;
        }

        public async Task<List<UnitItemLookupViewModel>> GetUnitItemLookupDataAsync(int JobId, int EquipmentCategoryId, bool isFemale)
        {
            List<UnitItemLookupViewModel> items = new List<UnitItemLookupViewModel>();

            using (FFTContext context = new FFTContext())
            {
                if (EquipmentCategoryId != (int)Enums.EquipmentCategoriesList.Accessory)
                {
                    Job unitJob = await context.Jobs.SingleAsync(m => m.JobId == JobId);

                    items.AddRange(unitJob.Jobitems.Where(m => m.ItemCategory.EquipmentCategoryId == EquipmentCategoryId).Select(m => new UnitItemLookupViewModel
                    {
                        ItemCategoryID = m.ItemCategoryId,
                        ItemCategoryName = m.ItemCategory.ItemCategoryName,
                        StartingItemImagePath = m.ItemCategory.StartingItemImagePath
                    }));
                }
                else
                {
                    items.AddRange(await context.ItemCategories.Where(m => m.EquipmentCategoryId == EquipmentCategoryId && (!isFemale ? m.IsFemaleOnly : true)).Select(m => new UnitItemLookupViewModel
                    {
                        ItemCategoryID = m.ItemCategoryId,
                        ItemCategoryName = m.ItemCategoryName,
                        StartingItemImagePath = m.StartingItemImagePath
                    }).ToListAsync());
                }
            }

            return items;
        }

        public async Task<List<ItemOverviewViewModel>> GetItemSelectionDataAsync(int ItemCategoryId)
        {
            List<ItemOverviewViewModel> items = new List<ItemOverviewViewModel>();

            using (FFTContext context = new FFTContext())
            {
                items.AddRange(await context.Items.Where(m => m.ItemCategoryId == ItemCategoryId).Select(c => new ItemOverviewViewModel
                {
                    ItemID = c.ItemId,
                    EquipmentCategoryID = c.ItemCategory.EquipmentCategoryId,
                    EquipmentCategoryName = c.ItemCategory.EquipmentCategoryName.ToLower(),
                    ItemCategoryID = ItemCategoryId,
                    AccessoryMagicalEvade = (c.ItemCategoryId == (int)Enums.ItemCategoriesList.Cloak ? c.MagicalEvade ?? 0 : 0).ToString(),
                    AccessoryPhysicalEvade = (c.ItemCategoryId == (int)Enums.ItemCategoriesList.Cloak ? c.PhysicalEvade ?? 0 : 0).ToString(),
                    HPBonus = (c.HPBonus ?? 0),
                    MPBonus = (c.MPBonus ?? 0),
                    MoveBonus = (c.MoveBoost ?? 0),
                    JumpBonus = (c.JumpBoost ?? 0),
                    SpeedBonus = (c.SpeedBoost ?? 0),
                    ShieldPhysicalEvade = (c.ItemCategoryId == (int)Enums.ItemCategoriesList.Shield ? c.PhysicalEvade ?? 0 : 0).ToString(),
                    ShieldMagicalEvade = (c.ItemCategoryId == (int)Enums.ItemCategoriesList.Shield ? c.MagicalEvade ?? 0 : 0).ToString(),
                    PhysicalAttackPower = (c.PhysicalAttackBoost ?? 0).ToString(),
                    MagicalAttackPower = (c.MagicAttackBoost ?? 0).ToString(),
                    Name = c.PspName,
                    WeaponPower = (c.AttackPower ?? 0).ToString(),
                    WeaponHit = (c.HitPercentage ?? 0).ToString(),
                    ImagePath = (c.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Weapon ? @"/img/Item_Icons/Weapons/" : c.ItemCategory.EquipmentCategoryId > (int)Enums.EquipmentCategoriesList.Weapon &&
                        c.ItemCategory.EquipmentCategoryId < (int)Enums.EquipmentCategoriesList.Accessory ? @"/img/Item_Icons/Armour/" : @"/img/Item_Icons/Accessories/") + c.IconFileName
                }).ToListAsync());
            }

            return items;
        }

        public async Task<ItemDetailsViewModel> GetItemDetailsDataAsync(int ItemId)
        {
            ItemDetailsViewModel itemDetails;

            using (FFTContext context = new FFTContext())
            {
                Item item = await context.Items.SingleAsync(m => m.ItemId == ItemId);
                itemDetails = new ItemDetailsViewModel
                {
                    ItemID = item.ItemId,
                    AcquiredViaCatch = item.AcquiredViaCatch,
                    AcquiredViaInitialEquip = item.AcquiredViaInitialEquip,
                    AcquiredViaLocation = item.AcquiredViaLocation,
                    AcquiredViaMode = item.AcquiredViaMode,
                    AcquiredViaPoach = item.AcquiredViaPoach,
                    AcquiredViaSteal = item.AcquiredViaSteal,
                    AcquiredViaTreasureHunt = item.AcquiredViaTreasureHunt,
                    AttackPower = item.AttackPower?.ToString("D3") ?? "0",
                    Cost = item.Cost,
                    Description = item.Description,
                    Element = AttributeCalculatorService.GetElementSVGLocations(item.Element),
                    ElementAbsorbed = AttributeCalculatorService.GetElementSVGLocations(item.ElementAbsorbed),
                    ElementBoosted = AttributeCalculatorService.GetElementSVGLocations(item.ElementBoosted),
                    ElementHalved = AttributeCalculatorService.GetElementSVGLocations(item.ElementHalved),
                    ElementNegated = AttributeCalculatorService.GetElementSVGLocations(item.ElementNegated),
                    ElementWeakness = AttributeCalculatorService.GetElementSVGLocations(item.ElementWeakness),
                    EquipStatusEffect = AttributeCalculatorService.GetEffectSVGLocations(item.EquipStatusEffect),
                    HitPercentage = item.HitPercentage?.ToString("D2") ?? "0",
                    HitStatusEffect = AttributeCalculatorService.GetEffectSVGLocations(item.HitStatusEffect),
                    HPBonus = item.HPBonus?.ToString() ?? "0",
                    ImagePath = (item.ItemCategory.EquipmentCategoryId == (int)Enums.EquipmentCategoriesList.Weapon ? @"/img/Item_Icons/Weapons/" : item.ItemCategory.EquipmentCategoryId > (int)Enums.EquipmentCategoriesList.Weapon &&
                        item.ItemCategory.EquipmentCategoryId < (int)Enums.EquipmentCategoriesList.Accessory ? @"/img/Item_Icons/Armour/" : @"/img/Item_Icons/Accessories/") + item.IconFileName,
                    ImmuneStatusEffect = AttributeCalculatorService.GetEffectSVGLocations(item.ImmuneStatusEffect),
                    JumpBoost = item.JumpBoost?.ToString() ?? "0",
                    MagicalEvade = item.MagicalEvade?.ToString() ?? "0",
                    MagicAttackBoost = item.MagicAttackBoost?.ToString() ?? "0",
                    MoveBoost = item.MoveBoost?.ToString() ?? "0",
                    MPBonus = item.MPBonus?.ToString() ?? "0",
                    Name = item.PspName,
                    PhysicalAttackBoost = item.PhysicalAttackBoost?.ToString() ?? "0",
                    PhysicalEvade = item.PhysicalEvade?.ToString() ?? "0",
                    RemoveStatusEffect = AttributeCalculatorService.GetEffectSVGLocations(item.RemoveStatusEffect),
                    SpeedBoost = item.SpeedBoost?.ToString() ?? "0",
                    SpellEffect = AttributeCalculatorService.GetEffectSVGLocations(item.SpellEffect),
                    ItemCategoryName = item.ItemCategory.ItemCategoryName
                };
            }

            return itemDetails;
        }

        public async Task<List<JobTreantNodeViewModel>> GetJobTreeDataAsync(string gender)
        {
            List<JobTreantNodeViewModel> viewModels = new List<JobTreantNodeViewModel>();
            using (FFTContext context = new FFTContext())
            {
                List<Job> jobs = await context.Jobs.Where(m => !m.PspName.Contains("1")).ToListAsync();

                foreach (Job job in jobs)
                {
                    viewModels.Add(new JobTreantNodeViewModel
                    {
                        RequiredGender = job.IsMaleOnly ? "Male" : job.IsFemaleOnly ? "Female" : "",
                        ImagePath = $"/img/Jobs/{(Enum.GetName(typeof(Enums.Jobs), job.JobId).Contains("Onion") ? "OnionKnight" : Enum.GetName(typeof(Enums.Jobs), job.JobId))}_" +
                            $"{(job.PspName.Contains("Dancer") ? "Female" : job.PspName.Contains("Bard") ? "Male" : gender)}_Standing.png",
                        JobName = job.PspName.Contains("Onion") ? "Onion Knight" : job.PspName,
                        IdentifierName = job.PspName.Contains("Onion") ? "onionknight" : job.PspName.Replace(" ", "").ToLower(),
                        JobPrerequisiteNames = job.Jobprerequisites.Select(m => (m.JobLevelRequiredPsp > -1 ? $"Level {m.JobLevelRequiredPsp} " : "Mastered ") + $"{jobs.Single(c => c.JobId == m.JobRequiredId).PspName}").ToList()
                    });
                };

                viewModels.Single(m => m.JobName.Contains("Dark")).JobPrerequisiteNames.Add("Crystallize 20 Units");
            }

            return viewModels;
        }

        public async Task<UnitDetailsViewModel> PopulateNewUnitDataAsync(int JobId, int gender, int position)
        {
            UnitDetailsViewModel unit;
            Item weaponItem1 = null, weaponItem2 = null, headItem = null, bodyItem = null;
            Random r = new Random();

            using (FFTContext context = new FFTContext())
            {
                Job unitJob = await context.Jobs.SingleAsync(m => m.JobId == JobId);
                int nameCount = context.UnitNames.Count(m => m.Gender == gender);

                unit = new UnitDetailsViewModel
                {
                    Unit = new UnitOverviewViewModel
                    {
                        UnitName = await context.UnitNames.Where(m => m.Gender == gender).Skip(r.Next(nameCount)).Select(m => m.Name).FirstAsync()
                    }
                };

                unit.Unit.JobID = JobId;
                unit.Unit.JobName = Enum.GetName(typeof(Enums.Jobs), JobId);
                unit.Unit.Gender = gender;
                unit.Unit.GenderName = Enum.GetName(typeof(Enums.Gender), gender);
                unit.Unit.Position = position;
                unit.Unit.Level = 1;
                unit.Unit.Experience = r.Next(0, 99);
                unit.Unit.Brave = r.Next(40, 70);
                unit.Unit.Faith = r.Next(40, 70);
                unit.RawHP = gender == 0 ? r.Next(491520, 524287) : gender == 1 ? r.Next(458752, 491519) : r.Next(573440, 622591);
                unit.RawMP = gender == 0 ? r.Next(229376, 245759) : gender == 1 ? r.Next(245760, 262143) : r.Next(98304, 147455);
                unit.RawPhysicalAttack = gender == 0 ? 81920 : gender == 1 ? 65536 : r.Next(81920, 98303);
                unit.RawMagicalAttack = gender == 0 ? 65536 : gender == 1 ? 81920 : r.Next(81920, 98303);
                unit.RawSpeed = gender == 0 ? 98304 : gender == 1 ? 98304 : 81920;

                List<ItemCategoryViewModel> jobItems = await context.JobItems.Where(m => m.JobId == unit.Unit.JobID).Select(m => new ItemCategoryViewModel
                {
                    EquipmentCategoryID = m.ItemCategory.EquipmentCategoryId,
                    ItemCategoryID = m.ItemCategoryId
                }).ToListAsync();

                if (jobItems.Any(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Weapon))
                {
                    int randomWeaponID = jobItems.FirstOrDefault(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Weapon).ItemCategoryID;
                    weaponItem1 = jobItems.Any(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Weapon) ? context.Items.FirstOrDefault(m => m.ItemCategoryId ==
                    randomWeaponID && m.IsStartingItem) : null;
                    unit.WeaponID = weaponItem1 != null ? weaponItem1.ItemId : 0;
                    unit.ShieldID = JobId == (int)Enums.Jobs.Ninja ? unit.WeaponID : 0;
                }

                if (jobItems.Any(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Shield))
                {
                    int randomShieldID = jobItems.FirstOrDefault(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Shield).ItemCategoryID;
                    weaponItem2 = jobItems.Any(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Shield) ? context.Items.FirstOrDefault(m => m.ItemCategoryId ==
                    randomShieldID && m.IsStartingItem) : null;
                    unit.ShieldID = weaponItem2 != null ? weaponItem2.ItemId : 0;
                }

                if (jobItems.Any(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Helmet))
                {
                    int randomHeadID = jobItems.FirstOrDefault(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Helmet).ItemCategoryID;
                    headItem = jobItems.Any(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Helmet) ? context.Items.Where(c => c.ItemCategoryId == randomHeadID)
                        .FirstOrDefault(m => m.IsStartingItem) : null;
                    unit.HeadID = headItem != null ? headItem.ItemId : 0;
                }

                if (jobItems.Any(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Armor))
                {
                    int randomBodyID = jobItems.FirstOrDefault(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Armor).ItemCategoryID;
                    bodyItem = jobItems.Any(m => m.EquipmentCategoryID == (int)Enums.EquipmentCategoriesList.Armor) ? context.Items.FirstOrDefault(m => m.ItemCategoryId ==
                    randomBodyID && m.IsStartingItem) : null;
                    unit.BodyID = bodyItem != null ? bodyItem.ItemId : 0;
                }

                unit.Resistances = new UnitResistAndImmunityViewModel();
                unit = AttributeCalculatorService.CalculateHPAndMP(headItem, bodyItem, unit, unitJob);
                unit = AttributeCalculatorService.CalculateBasicStats(weaponItem1, weaponItem2, headItem, bodyItem, null, unit, unitJob);
                unit = AttributeCalculatorService.CalculateEvasionStats(weaponItem2, null, unitJob, unit);
                unit = AttributeCalculatorService.CalculateResistancesAndImmunities(new List<Item> { weaponItem1, weaponItem2, headItem, bodyItem }, unit);
                unit.PrimaryAbilityJobID = unit.Unit.JobID;
                unit.PrimaryAbilityName = unitJob.AbilitySetPspName;
            }

            return unit;
        }
    }
}
