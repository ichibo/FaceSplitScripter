﻿namespace FaceSplitScripter
{
    public class SkillScrollLootItem : ILootItem
    {
        public LootType LootType { get { return LootType.SkillScroll; } }
        public SkillScroll SkillScroll { get; private set; }

        public int TomePage { get { return EnumUtilities.GetSkillScrollPage(SkillScroll); } }
        public string Description { get; private set; }

        public SkillScrollLootItem(string description, SkillScroll skillScroll)
        {
            Description = description;
            SkillScroll = skillScroll;
        }

        public string GumpResponseButtonForTome
        {
            get
            {
                switch (SkillScroll)
                {
                    case SkillScroll.Alchemy:
                        return "10";
                    case SkillScroll.AnimalLore:
                        return "11";
                    case SkillScroll.AnimalTame:
                        return "12";
                    case SkillScroll.ArmsLore:
                        return "13";
                    case SkillScroll.Begging:
                        return "14";
                    case SkillScroll.Blacksmithing:
                        return "15";
                    case SkillScroll.Camping:
                        return "16";
                    case SkillScroll.Carpentry:
                        return "17";
                    case SkillScroll.Cartography:
                        return "18";
                    case SkillScroll.Chivalry:
                        return "19";
                    case SkillScroll.Cooking:
                        return "20";
                    case SkillScroll.DetectHidden:
                        return "21";
                    case SkillScroll.Discordance:
                        return "22";
                    case SkillScroll.Fishing:
                        return "23";
                    case SkillScroll.ForensicEvaluation:
                        return "24";
                    case SkillScroll.Herding:
                        return "25";
                    case SkillScroll.Inscription:
                        return "26";
                    case SkillScroll.ItemID:
                        return "27";
                    case SkillScroll.Lockpicking:
                        return "28";
                    case SkillScroll.LumberJacking:
                        return "29";
                    case SkillScroll.Mining:
                        return "10";
                    case SkillScroll.Musicianship:
                        return "11";
                    case SkillScroll.Necromancy:
                        return "12";
                    case SkillScroll.Peacemaking:
                        return "13";
                    case SkillScroll.Poisoning:
                        return "14";
                    case SkillScroll.Provocation:
                        return "15";
                    case SkillScroll.SpiritSpeak:
                        return "16";
                    case SkillScroll.Stealthing:
                        return "17";
                    case SkillScroll.Tailoring:
                        return "18";
                    case SkillScroll.TasteID:
                        return "19";
                    case SkillScroll.Tinkering:
                        return "20";
                    case SkillScroll.Tracking:
                        return "21";
                    case SkillScroll.Veterinary:
                        return "22";

                    default:
                        return "0";
                }
            }
        }
    }
}
