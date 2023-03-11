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
            { "alchy", SkillScroll.Alchemy },
            { "animal lore", SkillScroll.AnimalLore },
            { "animal tame", SkillScroll.AnimalTame },
            { "arms lore", SkillScroll.ArmsLore },
            { "beg", SkillScroll.Begging },
            { "blacksmith", SkillScroll.Blacksmithing },
            { "camp", SkillScroll.Camping },
            { "carpentry", SkillScroll.Carpentry },
            { "carto", SkillScroll.Cartography },
            { "chivalry", SkillScroll.Chivalry },
            { "cook", SkillScroll.Cooking },
            { "detect", SkillScroll.DetectHidden },
            { "discordance", SkillScroll.Discordance },
            { "fish", SkillScroll.Fishing },
            { "forensic", SkillScroll.ForensicEvaluation },
            { "herd", SkillScroll.Herding },
            { "inscript", SkillScroll.Inscription },
            { "item id", SkillScroll.ItemID },
            { "lockpick", SkillScroll.Lockpicking },
            { "lumber", SkillScroll.LumberJacking },
            { "mine", SkillScroll.Mining },
            { "music", SkillScroll.Musicianship },
            { "necromancy", SkillScroll.Necromancy },
            { "peace", SkillScroll.Peacemaking },
            { "poison", SkillScroll.Poisoning },
            { "provo", SkillScroll.Provocation },
            { "remove", SkillScroll.RemoveTrap },
            { "spirit", SkillScroll.SpiritSpeak },
            { "stealth", SkillScroll.Stealthing },
            { "tailor", SkillScroll.Tailoring },
            { "taste", SkillScroll.TasteID },
            { "tinker", SkillScroll.Tinkering },
            { "track", SkillScroll.Tracking },
            { "veterinary", SkillScroll.Veterinary }
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
            string lowercaseText = strippedQuantityText.ToLower();

            foreach (string scrollKey in SkillScrollStringMapping.Keys)
            {
                // Skill scrolls should be an exact match as some links or other items may contain the same words as the skill scrolls.
                if (lowercaseText.Equals(scrollKey))
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
