using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor
{
    public class Enums
    {
        public enum Jobs
        {
            Squire = 1, Chemist, Knight, Archer, WhiteMage, BlackMage, Monk, Thief, Mystic, TimeMage, Geomancer, Dragoon, Orator, Summoner, Samurai,
            Ninja, Arithmetician, Dancer, Bard, Mime, DarkKnight, OnionKnightBasic, OnionKnightMastered
        }

        public enum Gender
        {
            Male, Female, Monster
        }

        public enum EquipmentCategoriesList
        {
            Weapon, Shield, Helmet, Armor, Accessory, Misc
        }

        public enum ItemCategoriesList
        {
            Axe = 1, Bag, Book, Bow, Cloth, Crossbow, FellSword, Flail, Gun, Instrument, Katana, Knife, KnightSword,
            NinjaBlade, Pole, Rod, Polearm, Staff, Sword, ThrowingItem, Unarmed, HeavyArmour, Clothes, HairAdornment,
            Hat, Helmet, Robe, Shield, Shoe, Armguard, Ring, Cloak, Perfume, LipRouge, Armlet
        }
    }
}
