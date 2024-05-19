using FaceSplitScripter.LootItems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FaceSplitScripter
{
    public class LootsplitScriptOrchestrator
    {
        private RazorScriptBuilder _scriptBuilder;
        const string MISSING_OBJECT_TEXT_STRING = "of that type are current";

        public LootsplitScriptOrchestrator()
        {
            _scriptBuilder = new RazorScriptBuilder();
        }

        public RazorScriptBuilder ConvertLootsplitTextToRazorMacro(string text, bool handleNonTomeItems = true)
        {
            IEnumerable<ILootItem> lootItems = LootTextParser.ParseFullLootsplitText(text);
            return CreateRazorMacroFromLootItems(lootItems, handleNonTomeItems);
        }

        public RazorScriptBuilder CreateRazorMacroFromLootItems(IEnumerable<ILootItem> lootItems, bool handleNonTomeItems)
        {
            // 0. Do script initializations.
            _scriptBuilder.AddOverhead("-Target Loot Container-");
            _scriptBuilder.InitializeVars();
            _scriptBuilder.GetTargetContainerForScript();
            _scriptBuilder.SetScriptIds();

            // 1. Organize into lists by groups.
            ILootItem[] skillOrbs = lootItems.Where(x => x.LootType == LootType.SkillOrb).ToArray();
            ILootItem[] mcds = lootItems.Where(x => x.LootType == LootType.MCD).ToArray();
            ILootItem[] chromaticCores = lootItems.Where(x => x.LootType == LootType.ChromaticCore).ToArray();
            ILootItem[] chromaticDistills = lootItems.Where(x => x.LootType == LootType.ChromaticDistill).ToArray();
            ILootItem[] distills = lootItems.Where(x => x.LootType == LootType.Distill).ToArray();
            ILootItem[] cores = lootItems.Where(x => x.LootType == LootType.Core).ToArray();
            ILootItem[] tmaps = lootItems.Where(x => x.LootType == LootType.TreasureMap).ToArray();
            ILootItem[] oreMaps = lootItems.Where(x => x.LootType == LootType.ResourceMapMining).ToArray();
            ILootItem[] skinningMaps = lootItems.Where(x => x.LootType == LootType.ResourceMapSkinning).ToArray();
            ILootItem[] lumberMaps = lootItems.Where(x => x.LootType == LootType.ResourceMapLumber).ToArray();
            ILootItem[] fishingMaps = lootItems.Where(x => x.LootType == LootType.ResourceMapFishing).ToArray();
            ILootItem[] skillScrolls = lootItems.Where(x => x.LootType == LootType.SkillScroll).ToArray();
            ILootItem[] manualItems = lootItems.Where(x => x.LootType == LootType.Manual).ToArray();

            // 2.Iterate on each item type and build the necessary scripts.
            if (handleNonTomeItems)
            {
                GenerateTopLevelItemScript(skillOrbs, NonTomeItemDetails.SkillOrb);
                GenerateTopLevelItemScript(mcds, NonTomeItemDetails.MCD);
                GenerateTopLevelItemScript(chromaticCores, NonTomeItemDetails.ChromaticCore);
                GenerateTopLevelItemScript(chromaticDistills, NonTomeItemDetails.ChromaticDistill);
            }

            else
            {
                manualItems = manualItems.Concat(skillOrbs.Take(1)).Concat(mcds.Take(1)).Concat(chromaticCores.Take(1)).Concat(chromaticDistills.Take(1)).ToArray();
            }

            GenerateAspectTomeScripts(distills, cores);

            GenerateBasicMapTomeScripts(tmaps, "treasure", Constants.TMAP_TOME_GUMP_ID, Constants.TREASURE_MAP_TOME_VARIABLE_NAME);
            GenerateBasicMapTomeScripts(oreMaps, "ore", Constants.ORE_TOME_GUMP_ID, Constants.ORE_TOME_VARIABLE_NAME);
            GenerateBasicMapTomeScripts(skinningMaps, "skinning", Constants.SKINNING_TOME_GUMP_ID, Constants.SKINNING_TOME_VARIABLE_NAME);
            GenerateBasicMapTomeScripts(lumberMaps, "lumber", Constants.LUMBER_TOME_GUMP_ID, Constants.LUMBER_TOME_VARIABLE_NAME);
            GenerateBasicMapTomeScripts(fishingMaps, "fishing", Constants.FISHING_TOME_GUMP_ID, Constants.FISHING_TOME_VARIABLE_NAME);

            GenerateSkillScrollTomeScripts(skillScrolls);

            // 3. Any additional non-scripting steps.
            ProcessManualItems(manualItems);
            ProcessMissingItems();

            _scriptBuilder.AddOverhead("All loot complete!");

            return _scriptBuilder;
        }

        private void GenerateTopLevelItemScript(ILootItem[] lootItem, INonTomeItemDetails nonTomeItemDetails)
        {
            if (lootItem.Length > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverheadAndScript($"Starting {nonTomeItemDetails.Description}...");

                int quantity = lootItem.Length;

                _scriptBuilder.CheckInsufficientItemsInTopContainerByItemNameHue(nonTomeItemDetails.ItemName, nonTomeItemDetails.Hue, nonTomeItemDetails.Description, quantity);
                _scriptBuilder.PullItemToBackpackFromTopContainerByItemNameVariableNameHue(nonTomeItemDetails.ItemName, nonTomeItemDetails.Hue, nonTomeItemDetails.VariableName, quantity);

                _scriptBuilder.AddOverheadAndScript($"{nonTomeItemDetails.Description} done.");
            }
        }

        private void GenerateSkillScrollTomeScripts(IEnumerable<ILootItem> skillScrolls)
        {
            if (skillScrolls.Count() > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverheadAndScript("Starting skill scrolls...");
                _scriptBuilder.DoubleClickSkillScrollTome();
                _scriptBuilder.AddGCDWait();

                ILootItem[] pageOneScrolls = skillScrolls.Where(x => x.TomePage == 1).ToArray();
                ILootItem[] pageTwoScrolls = skillScrolls.Where(x => x.TomePage == 2).ToArray();

                foreach (ILootItem scroll in pageOneScrolls)
                {
                    _scriptBuilder.AddRazorComment(scroll.Description);
                    _scriptBuilder.GumpResponse(Constants.SKILLSCROLL_TOME_GUMP_ID, scroll.GumpResponseButtonForTome);
                    _scriptBuilder.AddMissingItemCheck(MISSING_OBJECT_TEXT_STRING, scroll.Description);
                }

                if (pageTwoScrolls.Length > 0)
                {
                    _scriptBuilder.AddRazorComment("Skill Scroll Tome Page 2");
                    _scriptBuilder.GumpResponse(Constants.SKILLSCROLL_TOME_GUMP_ID, Constants.NEXT_PAGE_FOR_SKILLSCROLL_TOME);

                    foreach (ILootItem scroll in pageTwoScrolls)
                    {
                        _scriptBuilder.AddRazorComment(scroll.Description);
                        _scriptBuilder.GumpResponse(Constants.SKILLSCROLL_TOME_GUMP_ID, scroll.GumpResponseButtonForTome);
                        _scriptBuilder.AddMissingItemCheck(MISSING_OBJECT_TEXT_STRING, scroll.Description);
                    }
                }

                _scriptBuilder.AddOverheadAndScript("Skill scrolls done.");
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.GumpClose(Constants.SKILLSCROLL_TOME_GUMP_ID);
            }
        }

        private void GenerateBasicMapTomeScripts(IEnumerable<ILootItem> maps, string description, string gumpId, string tomeVariableName)
        {
            if (maps.Count() > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverheadAndScript($"Starting {description} maps...");
                _scriptBuilder.DoubleClickByVariableName(tomeVariableName);
                _scriptBuilder.AddGCDWait();

                foreach (ILootItem map in maps)
                {
                    _scriptBuilder.AddRazorComment(map.Description);
                    _scriptBuilder.GumpResponse(gumpId, map.GumpResponseButtonForTome);
                    _scriptBuilder.AddMissingItemCheck(MISSING_OBJECT_TEXT_STRING, map.Description);
                }

                _scriptBuilder.AddOverheadAndScript($"{description} maps done.");
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.GumpClose(gumpId);
            }
        }

        private void GenerateAspectTomeScripts(IEnumerable<ILootItem> distills, IEnumerable<ILootItem> cores)
        {
            // Do cores Pg1/Pg2, Close tome if needed, Re-open to reset starting page, then do distills if needed.
            if (cores.Count() > 0)
            {
                ILootItem[] pageOneCores = cores.Where(x => x.TomePage == 1).ToArray();
                ILootItem[] pageTwoCores = cores.Where(x => x.TomePage == 2).ToArray();

                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverheadAndScript("Starting aspect cores...");
                _scriptBuilder.DoubleClickAspectTome();
                _scriptBuilder.AddGCDWait();

                foreach (ILootItem core in pageOneCores)
                {
                    _scriptBuilder.AddRazorComment(core.Description);
                    _scriptBuilder.GumpResponse(Constants.ASPECT_TOME_GUMP_ID, core.GumpResponseButtonForTome);
                    _scriptBuilder.AddMissingItemCheck(MISSING_OBJECT_TEXT_STRING, core.Description);
                }

                if (pageTwoCores.Length > 0)
                {
                    _scriptBuilder.AddRazorComment("Aspect Cores Page 2");
                    _scriptBuilder.GumpResponse(Constants.ASPECT_TOME_GUMP_ID, Constants.NEXT_PAGE_FOR_ASPECT_CORE_TOME);

                    foreach (ILootItem core in pageTwoCores)
                    {
                        _scriptBuilder.AddRazorComment(core.Description);
                        _scriptBuilder.GumpResponse(Constants.ASPECT_TOME_GUMP_ID, core.GumpResponseButtonForTome);
                        _scriptBuilder.AddMissingItemCheck(MISSING_OBJECT_TEXT_STRING, core.Description);
                    }
                }

                _scriptBuilder.AddOverheadAndScript("Aspect cores done.");
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.GumpClose(Constants.ASPECT_TOME_GUMP_ID);
            }

            if (distills.Count() > 0)
            {
                ILootItem[] pageOneDistills = distills.Where(x => x.TomePage == 1).ToArray();
                ILootItem[] pageTwoDistills = distills.Where(x => x.TomePage == 2).ToArray();

                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverheadAndScript("Starting aspect distills...");
                _scriptBuilder.DoubleClickAspectTome();
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.GumpResponse(Constants.ASPECT_TOME_GUMP_ID, Constants.NEXT_ITEM_TYPE_PAGE_FOR_ASPECT_CORE_TOME);

                foreach (ILootItem distill in pageOneDistills)
                {
                    _scriptBuilder.AddRazorComment(distill.Description);
                    _scriptBuilder.GumpResponse(Constants.ASPECT_TOME_GUMP_ID, distill.GumpResponseButtonForTome);
                    _scriptBuilder.AddMissingItemCheck(MISSING_OBJECT_TEXT_STRING, distill.Description);
                }

                if (pageTwoDistills.Length > 0)
                {
                    _scriptBuilder.AddRazorComment("Aspect Distills Page 2");
                    _scriptBuilder.GumpResponse(Constants.ASPECT_TOME_GUMP_ID, Constants.NEXT_PAGE_FOR_ASPECT_CORE_TOME);

                    foreach (ILootItem distill in pageTwoDistills)
                    {
                        _scriptBuilder.AddRazorComment(distill.Description);
                        _scriptBuilder.GumpResponse(Constants.ASPECT_TOME_GUMP_ID, distill.GumpResponseButtonForTome);
                        _scriptBuilder.AddMissingItemCheck(MISSING_OBJECT_TEXT_STRING, distill.Description);
                    }
                }

                _scriptBuilder.AddOverheadAndScript("Aspect distills done.");
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.GumpClose(Constants.ASPECT_TOME_GUMP_ID);
            }
        }

        private void ProcessManualItems(IEnumerable<ILootItem> manualItems)
        {
            foreach (ILootItem manualItem in manualItems)
            {
                _scriptBuilder.AddManualItem(manualItem.Description);
            }
        }

        private void ProcessMissingItems()
        {
            _scriptBuilder.DisplayMissingItems();
        }
    }
}