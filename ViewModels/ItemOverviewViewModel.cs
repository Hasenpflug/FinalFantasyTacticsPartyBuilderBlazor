using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.ViewModels
{
    public class ItemOverviewViewModel
    {
        public ItemOverviewViewModel()
        {
            WeaponPower = "000";
            WeaponHit = "000";
            AccessoryPhysicalEvade = "00";
            AccessoryMagicalEvade = "00";
            ShieldPhysicalEvade = "00";
            ShieldMagicalEvade = "00";
            PhysicalAttackPower = "00";
            MagicalAttackPower = "00";
            ImagePath = "";
            Name = "";
        }

        public int ItemID { get; set; }

        public int EquipmentCategoryID { get; set; }

        public string EquipmentCategoryName { get; set; }

        public int ItemCategoryID { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string WeaponPower { get; set; }

        public string WeaponHit { get; set; }

        public string ShieldPhysicalEvade { get; set; }

        public string ShieldMagicalEvade { get; set; }

        public string AccessoryPhysicalEvade { get; set; }

        public string AccessoryMagicalEvade { get; set; }

        public string PhysicalAttackPower { get; set; }

        public string MagicalAttackPower { get; set; }

        public int HPBonus { get; set; }

        public int MPBonus { get; set; }

        public int MoveBonus { get; set; }

        public int JumpBonus { get; set; }

        public int SpeedBonus { get; set; }
    }
}
