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

        public RazorScriptBuilder ConvertLootsplitTextToRazorMacro(string text)
        {
            IEnumerable<ILootItem> lootItems = LootTextParser.ParseFullLootsplitText(text);
            return CreateRazorMacroFromLootItems(lootItems);
        }

        public RazorScriptBuilder CreateRazorMacroFromLootItems(IEnumerable<ILootItem> lootItems)
        {
            // 0. Do script initializations.
            _scriptBuilder.AddOverhead("-Target Loot Container-");
            _scriptBuilder.InitializeVars();
            _scriptBuilder.GetTargetContainerForScript();
            _scriptBuilder.SetScriptIds();

            // 1. Organize into lists by groups.
            ILootItem[] skillOrbs = lootItems.Where(x => x.LootType == LootType.SkillOrb).ToArray();
            ILootItem[] mcds = lootItems.Where(x => x.LootType == LootType.MCD).ToArray();
            ILootItem[] distills = lootItems.Where(x => x.LootType == LootType.Distill).ToArray();
            ILootItem[] cores = lootItems.Where(x => x.LootType == LootType.Core).ToArray();
            ILootItem[] tmaps = lootItems.Where(x => x.LootType == LootType.TreasureMap).ToArray();
            ILootItem[] skillScrolls = lootItems.Where(x => x.LootType == LootType.SkillScroll).ToArray();
            ILootItem[] manualItems = lootItems.Where(x => x.LootType == LootType.Manual).ToArray();

            // 2.Iterate on each item type and build the necessary scripts.
            GenerateSkillOrbScripts(skillOrbs);
            GenerateMCDScripts(mcds);
            GenerateAspectTomeScripts(distills, cores);
            GenerateTmapsTomeScripts(tmaps);
            GenerateSkillScrollTomeScripts(skillScrolls);

            // 3. Any additional non-scripting steps.
            ProcessManualItems(manualItems);
            ProcessMissingItems();

            _scriptBuilder.AddOverhead("All loot complete!");

            return _scriptBuilder;
        }

        private void GenerateSkillOrbScripts(ILootItem[] skillOrbs)
        {
            if (skillOrbs.Length > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverheadAndScript("Starting skill orbs...");

                int quantity = skillOrbs.Length;

                _scriptBuilder.CheckInsufficientSkillOrbs(quantity);
                _scriptBuilder.PullSkillOrbsToBackpack(quantity);

                _scriptBuilder.AddOverheadAndScript("Skill orbs done.");
            }
        }

        private void GenerateMCDScripts(ILootItem[] mcds)
        {
            if (mcds.Length > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverheadAndScript("Starting MCDs...");

                int quantity = mcds.Length;

                _scriptBuilder.CheckInsufficientMCDs(quantity);
                _scriptBuilder.PullMCDsToBackpack(quantity);

                _scriptBuilder.AddOverheadAndScript("MCDs done.");
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

        private void GenerateTmapsTomeScripts(IEnumerable<ILootItem> tmaps)
        {
            if (tmaps.Count() > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverheadAndScript("Starting treasure maps...");
                _scriptBuilder.DoubleClickTreasureMapTome();
                _scriptBuilder.AddGCDWait();

                foreach (ILootItem tmap in tmaps)
                {
                    _scriptBuilder.AddRazorComment(tmap.Description);
                    _scriptBuilder.GumpResponse(Constants.TMAP_TOME_GUMP_ID, tmap.GumpResponseButtonForTome);
                    _scriptBuilder.AddMissingItemCheck(MISSING_OBJECT_TEXT_STRING, tmap.Description);
                }

                _scriptBuilder.AddOverheadAndScript("Treasure maps done.");
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.GumpClose(Constants.TMAP_TOME_GUMP_ID);
            }
        }

        private void GenerateAspectTomeScripts(IEnumerable<ILootItem> distills, IEnumerable<ILootItem> cores)
        {
            // Cores Pg1, Cores Pg2, Dclick,  Distills Pg1, Distills Pg2
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

            // Cores Pg1, Cores Pg2, Dclick,  Distills Pg1, Distills Pg2
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