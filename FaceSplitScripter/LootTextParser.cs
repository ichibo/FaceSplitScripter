using FaceSplitScripter.LootItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceSplitScripter
{
    internal static class LootTextParser
    {
        public static IEnumerable<ILootItem> ParseFullLootsplitText(string text)
        {
            List<ILootItem> fullList = new List<ILootItem>();

            // 1. Split items by ","
            string[] splitLootText = text.Split(',');

            foreach (string lootLine in splitLootText)
            {
                IEnumerable<ILootItem> lootItems = ParseSingleLootsplitText(lootLine);

                if (lootItems.Count() > 0)
                {
                    fullList.AddRange(lootItems);
                }
            }

            return fullList;
        }

        public static IEnumerable<ILootItem> ParseSingleLootsplitText(string text)
        {
            int itemCount = ScriptUtilities.GetQuantityFromText(text);
            List<ILootItem> items = new List<ILootItem>();
            string lowercaseText = text.ToLower();
            string processedDescription = text.Trim();

            if (lowercaseText.Contains(Constants.SKILL_ORB_IDENTIFIER))
            {
                for (int i = 0; i < itemCount; i++)
                {
                    items.Add(new SkillOrbLootItem(processedDescription));
                }
            }

            else if (lowercaseText.Contains(Constants.MCD_IDENTIFIER))
            {
                for (int i = 0; i < itemCount; i++)
                {
                    items.Add(new MCDLootItem(processedDescription));
                }
            }

            else if (lowercaseText.Contains(Constants.CHROMATIC_IDENTIFIER) && lowercaseText.Contains(Constants.CORE_IDENTIFIER))
            {
                for (int i = 0; i < itemCount; i++)
                {
                    items.Add(new ChromaticCoreLootItem(processedDescription));
                }
            }

            else if (lowercaseText.Contains(Constants.CHROMATIC_IDENTIFIER) && (lowercaseText.Contains(Constants.EXTRACT_IDENTIFIER) || lowercaseText.Contains(Constants.DISTILL_IDENTIFIER)))
            {
                for (int i = 0; i < itemCount; i++)
                {
                    items.Add(new ChromaticDistillLootItem(processedDescription));
                }
            }

            else if (lowercaseText.Contains(Constants.CORE_IDENTIFIER))
            {
                Aspect aspect = EnumUtilities.GetAspectFromText(lowercaseText);

                if (aspect != Aspect.None)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        items.Add(new AspectCoreLootItem(processedDescription, aspect));
                    }
                }
            }

            else if (lowercaseText.Contains(Constants.EXTRACT_IDENTIFIER) || lowercaseText.Contains(Constants.DISTILL_IDENTIFIER))
            {
                Aspect aspect = EnumUtilities.GetAspectFromText(lowercaseText);

                if (aspect != Aspect.None)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        items.Add(new AspectDistillLootItem(processedDescription, aspect));
                    }
                }
            }

            else if (text.ToLower().Contains(Constants.TREASURE_MAP_IDENTIFIER))
            {
                TreasureMapLevel tmapLevel = EnumUtilities.GetTreasureMapLevelFromText(lowercaseText);

                if (tmapLevel != TreasureMapLevel.None)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        items.Add(new TreasureMapLootItem(processedDescription, tmapLevel));
                    }
                }
            }

            else if (text.ToLower().Contains(Constants.LUMBER_MAP_IDENTIFIER))
            {
                ResourceType resourceType = EnumUtilities.GetResourceTypeFromText(lowercaseText);

                if (resourceType != ResourceType.Normal)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        items.Add(new ResourceMapLumberLootItem(processedDescription, resourceType));
                    }
                }
            }

            else if (text.ToLower().Contains(Constants.MINING_MAP_IDENTIFIER))
            {
                ResourceType resourceType = EnumUtilities.GetResourceTypeFromText(lowercaseText);

                if (resourceType != ResourceType.Normal)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        items.Add(new ResourceMapMiningLootItem(processedDescription, resourceType));
                    }
                }
            }

            else if (text.ToLower().Contains(Constants.SKINNING_MAP_IDENTIFIER))
            {
                ResourceType resourceType = EnumUtilities.GetResourceTypeFromText(lowercaseText);

                if (resourceType != ResourceType.Normal)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        items.Add(new ResourceMapSkinningLootItem(processedDescription, resourceType));
                    }
                }
            }

            else if (text.ToLower().Contains(Constants.FISHING_MAP_IDENTIFIER))
            {
                ResourceType resourceType = EnumUtilities.GetResourceTypeFromText(lowercaseText);

                if (resourceType != ResourceType.Normal)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        items.Add(new ResourceMapFishingLootItem(processedDescription, resourceType));
                    }
                }
            }

            // Anything else might be a skill scroll since they don't have any static identifier
            else
            {
                SkillScroll skillScroll = EnumUtilities.GetSkillScrollFromText(lowercaseText);

                if (skillScroll != SkillScroll.None)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        items.Add(new SkillScrollLootItem(processedDescription, skillScroll));
                    }
                }
            }

            // We couldn't find anything to add for this line, must be manual.
            // Show the whole text as-is.
            if (items.Count() == 0)
            {
                items.Add(new ManualLootItem(text));
            }

            return items;
        }
    }
}
