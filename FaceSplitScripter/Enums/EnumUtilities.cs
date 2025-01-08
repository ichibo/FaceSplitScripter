using System;
using System.Collections.Generic;

namespace FaceSplitScripter
{
    internal class EnumUtilities
    {
        public static Dictionary<string, Aspect> AspectStringMapping = new Dictionary<string, Aspect>
        {
            { "air", Aspect.Air},
            { "artisan", Aspect.Artisan},
            { "blood", Aspect.Blood},
            { "command", Aspect.Command},
            { "death", Aspect.Death},
            { "discipline", Aspect.Discipline},
            { "earth", Aspect.Earth},
            { "eldritch", Aspect.Eldritch},
            { "fire", Aspect.Fire},
            { "fortune", Aspect.Fortune},
            { "gadget", Aspect.Gadget},
            { "harvest", Aspect.Harvest},
            { "holy", Aspect.Holy},
            { "lyric", Aspect.Lyric},
            { "poison", Aspect.Poison},
            { "shadow", Aspect.Shadow},
            { "void", Aspect.Void},
            { "war", Aspect.War},
            { "water", Aspect.Water}
        };

        public static Dictionary<string, SkillScroll> SkillScrollStringMapping = new Dictionary<string, SkillScroll>
        {
            { "Alchemy", SkillScroll.Alchemy },
            { "Animal Lore", SkillScroll.AnimalLore },
            { "Animal Taming", SkillScroll.AnimalTame },
            { "Arms Lore", SkillScroll.ArmsLore },
            { "Begging", SkillScroll.Begging },
            { "Blacksmith", SkillScroll.Blacksmithing },
            { "Camping", SkillScroll.Camping },
            { "Carpentry", SkillScroll.Carpentry },
            { "Cartography", SkillScroll.Cartography },
            { "Chivalry", SkillScroll.Chivalry },
            { "Cooking", SkillScroll.Cooking },
            { "Detect Hidden", SkillScroll.DetectHidden },
            { "Discordance", SkillScroll.Discordance },
            { "Fishing", SkillScroll.Fishing }, 
            { "Forensic Eval", SkillScroll.ForensicEvaluation },
            { "Herding", SkillScroll.Herding },
            { "Inscription", SkillScroll.Inscription },
            { "Item ID", SkillScroll.ItemID },
            { "Lockpicking", SkillScroll.Lockpicking },
            { "Lumberjacking", SkillScroll.LumberJacking },
            { "Mining", SkillScroll.Mining },
            { "Musicianship", SkillScroll.Musicianship },
            { "Necromancy", SkillScroll.Necromancy },
            { "Peacemaking", SkillScroll.Peacemaking },
            { "Poisoning", SkillScroll.Poisoning },
            { "Provocation", SkillScroll.Provocation },
            { "Spirit Speak", SkillScroll.SpiritSpeak },
            { "Stealth", SkillScroll.Stealthing },
            { "Tailoring", SkillScroll.Tailoring },
            { "Taste Id", SkillScroll.TasteID },
            { "Tinkering", SkillScroll.Tinkering },
            { "Tracking", SkillScroll.Tracking },
            { "Veterinary", SkillScroll.Veterinary }
        };

        public static Aspect GetAspectFromText(string text)
        {
            string lowercaseText = text.ToLower();

            foreach (string aspectKey in AspectStringMapping.Keys)
            {
                if (lowercaseText.Contains(aspectKey))
                {
                    return AspectStringMapping[aspectKey];
                }
            }

            return Aspect.None;
        }

        public static SkillScroll GetSkillScrollFromText(string text)
        {
            string strippedQuantityText = ScriptUtilities.RemoveQuantityFromText(text);

            // Shouldn't be a space here but trim just in case.
            string lowercaseText = strippedQuantityText.ToLower().Trim();

            foreach (string scrollKey in SkillScrollStringMapping.Keys)
            {

                // Skill scrolls should be an exact match as some links or other items may contain the same words as the skill scrolls.
                if (String.Equals(lowercaseText, scrollKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    return SkillScrollStringMapping[scrollKey];
                }
            }

            return SkillScroll.None;
        }

        public static int GetSkillScrollPage(SkillScroll skillScroll)
        {
            switch (skillScroll)
            {
                case SkillScroll.Alchemy:
                case SkillScroll.AnimalLore:
                case SkillScroll.AnimalTame:
                case SkillScroll.ArmsLore:
                case SkillScroll.Begging:
                case SkillScroll.Blacksmithing:
                case SkillScroll.Camping:
                case SkillScroll.Carpentry:
                case SkillScroll.Cartography:
                case SkillScroll.Chivalry:
                case SkillScroll.Cooking:
                case SkillScroll.DetectHidden:
                case SkillScroll.Discordance:
                case SkillScroll.Fishing:
                case SkillScroll.ForensicEvaluation:
                case SkillScroll.Herding:
                case SkillScroll.Inscription:
                case SkillScroll.ItemID:
                case SkillScroll.Lockpicking:
                case SkillScroll.LumberJacking:
                    return 1;

                case SkillScroll.Mining:
                case SkillScroll.Musicianship:
                case SkillScroll.Necromancy:
                case SkillScroll.Peacemaking:
                case SkillScroll.Poisoning:
                case SkillScroll.Provocation:
                case SkillScroll.RemoveTrap:
                case SkillScroll.SpiritSpeak:
                case SkillScroll.Stealthing:
                case SkillScroll.Tailoring:
                case SkillScroll.TasteID:
                case SkillScroll.Tinkering:
                case SkillScroll.Tracking:
                case SkillScroll.Veterinary:
                    return 2;

                default:
                    return 1;
            }
        }

        public static int GetAspectTomePage(Aspect aspect)
        {
            switch (aspect)
            {
                case Aspect.Air:
                case Aspect.Artisan:
                case Aspect.Blood:
                case Aspect.Command:
                case Aspect.Death:
                case Aspect.Discipline:
                case Aspect.Earth:
                case Aspect.Eldritch:
                case Aspect.Fire:
                case Aspect.Fortune:
                case Aspect.Gadget:
                case Aspect.Harvest:
                case Aspect.Holy:
                case Aspect.Lyric:
                case Aspect.Poison:
                    return 1;

                case Aspect.Shadow:
                case Aspect.Void:
                case Aspect.War:
                case Aspect.Water:
                    return 2;

                default:
                    return 1;
            }
        }

        public static TreasureMapLevel GetTreasureMapLevelFromText(string text)
        {
            // Remove LVL
            string tmapLevel = text.Substring(3, 1);

            switch (tmapLevel)
            {
                case "1":
                    return TreasureMapLevel.Level1;
                case "2":
                    return TreasureMapLevel.Level2;
                case "3":
                    return TreasureMapLevel.Level3;
                case "4":
                    return TreasureMapLevel.Level4;
                case "5":
                    return TreasureMapLevel.Level5;
                case "6":
                    return TreasureMapLevel.Level6;

                default:
                    return TreasureMapLevel.None;
            }
        }
    }
}
