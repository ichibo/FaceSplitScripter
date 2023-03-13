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
            ILootItem[] extracts = lootItems.Where(x => x.LootType == LootType.Extract).ToArray();
            ILootItem[] cores = lootItems.Where(x => x.LootType == LootType.Core).ToArray();
            ILootItem[] tmaps = lootItems.Where(x => x.LootType == LootType.TreasureMap).ToArray();
            ILootItem[] skillScrolls = lootItems.Where(x => x.LootType == LootType.SkillScroll).ToArray();
            ILootItem[] manualItems = lootItems.Where(x => x.LootType == LootType.Manual).ToArray();

            // 2.Iterate on each item type and build the necessary scripts.
            GenerateSkillOrbScripts(skillOrbs);
            GenerateMCDScripts(mcds);
            GenerateAspectTomeScripts(extracts, cores);
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

        private void GenerateAspectTomeScripts(IEnumerable<ILootItem> extracts, IEnumerable<ILootItem> cores)
        {
            if (extracts.Count() > 0 || cores.Count() > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverheadAndScript("Starting aspect items...");
                _scriptBuilder.DoubleClickAspectTome();
                _scriptBuilder.AddGCDWait();

                foreach (ILootItem core in cores)
                {
                    _scriptBuilder.AddRazorComment(core.Description);
                    _scriptBuilder.GumpResponse(Constants.ASPECT_TOME_GUMP_ID, core.GumpResponseButtonForTome);
                    _scriptBuilder.AddMissingItemCheck(MISSING_OBJECT_TEXT_STRING, core.Description);
                }

                foreach (ILootItem extract in extracts)
                {
                    _scriptBuilder.AddRazorComment(extract.Description);
                    _scriptBuilder.GumpResponse(Constants.ASPECT_TOME_GUMP_ID, extract.GumpResponseButtonForTome);
                    _scriptBuilder.AddMissingItemCheck(MISSING_OBJECT_TEXT_STRING, extract.Description);
                }

                _scriptBuilder.AddOverheadAndScript("Aspect items done.");
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