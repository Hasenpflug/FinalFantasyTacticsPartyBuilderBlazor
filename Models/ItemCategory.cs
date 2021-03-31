using System;
using System.Collections.Generic;

namespace FinalFantasyTacticsPartyBuilderBlazor.Models
{
    public partial class ItemCategory
    {
        public ItemCategory()
        {
            Items = new HashSet<Item>();
            Jobitems = new HashSet<JobItem>();
        }

        public int ItemCategoryId { get; set; }
        public int EquipmentCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public string EquipmentCategoryName { get; set; }
        public bool CanDualWield { get; set; }
        public bool CanDoubleHand { get; set; }
        public bool IsTwoHandOnly { get; set; }
        public string StartingItemImagePath { get; set; }
        public bool IsFemaleOnly { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<JobItem> Jobitems { get; set; }
    }
}
