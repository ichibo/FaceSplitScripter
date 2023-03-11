using FaceSplitScripter.LootItems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FaceSplitScripter
{
    public class LootsplitScriptOrchestrator
    {
        private RazorScriptBuilder _scriptBuilder;

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
            _scriptBuilder.AddOverhead("-- Target Loot Container --");
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

            ProcessManualItems(manualItems);

            _scriptBuilder.AddOverhead("All loot complete!");

            return _scriptBuilder;
        }

        private void GenerateSkillOrbScripts(ILootItem[] skillOrbs)
        {
            if (skillOrbs.Length > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverhead("Starting skill orbs...");
                _scriptBuilder.AddRazorComment("Beginning Skill Orbs");

                int quantity = skillOrbs.Length;

                _scriptBuilder.LiftSkillOrb(quantity);
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.DropOnSelf();
                _scriptBuilder.AddGCDWait();

                _scriptBuilder.AddOverhead("Skill orbs done.");
            }
        }

        private void GenerateMCDScripts(ILootItem[] mcds)
        {
            if (mcds.Length > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverhead("Starting MCDs...");
                _scriptBuilder.AddRazorComment("Beginning MCDs");

                int quantity = mcds.Length;

                _scriptBuilder.LiftMCD(quantity);
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.DropOnSelf();
                _scriptBuilder.AddGCDWait();

                _scriptBuilder.AddOverhead("MCDs done.");
            }
        }

        private void GenerateSkillScrollTomeScripts(IEnumerable<ILootItem> skillScrolls)
        {
            if (skillScrolls.Count() > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverhead("Starting skill scrolls...");
                _scriptBuilder.AddRazorComment("Beginning Skill Scroll Tome");
                _scriptBuilder.DoubleClickSkillScrollTome();
                _scriptBuilder.AddGCDWait();

                ILootItem[] pageOneScrolls = skillScrolls.Where(x => x.TomePage == 1).ToArray();
                ILootItem[] pageTwoScrolls = skillScrolls.Where(x => x.TomePage == 2).ToArray();

                foreach (ILootItem scroll in pageOneScrolls)
                {
                    _scriptBuilder.AddRazorComment(scroll.Description);
                    _scriptBuilder.WaitForGump(Constants.SKILLSCROLL_TOME_GUMP_ID);
                    _scriptBuilder.GumpResponse(Constants.SKILLSCROLL_TOME_GUMP_ID, scroll.GumpResponseButtonForTome);
                }

                if (pageTwoScrolls.Length > 0)
                {
                    _scriptBuilder.AddRazorComment("Skill Scroll Tome Page 2");
                    _scriptBuilder.WaitForGump(Constants.SKILLSCROLL_TOME_GUMP_ID);
                    _scriptBuilder.GumpResponse(Constants.SKILLSCROLL_TOME_GUMP_ID, Constants.NEXT_PAGE_FOR_SKILLSCROLL_TOME);

                    foreach (ILootItem scroll in pageTwoScrolls)
                    {
                        _scriptBuilder.AddRazorComment(scroll.Description);
                        _scriptBuilder.WaitForGump(Constants.SKILLSCROLL_TOME_GUMP_ID);
                        _scriptBuilder.GumpResponse(Constants.SKILLSCROLL_TOME_GUMP_ID, scroll.GumpResponseButtonForTome);
                    }
                }

                _scriptBuilder.AddOverhead("Skill scrolls done.");
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.GumpClose(Constants.SKILLSCROLL_TOME_GUMP_ID);
            }
        }

        private void GenerateTmapsTomeScripts(IEnumerable<ILootItem> tmaps)
        {
            if (tmaps.Count() > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverhead("Starting treasure maps...");
                _scriptBuilder.AddRazorComment("Beginning Treasure Map Tome");
                _scriptBuilder.DoubleClickTreasureMapTome();
                _scriptBuilder.AddGCDWait();

                foreach (ILootItem tmap in tmaps)
                {
                    _scriptBuilder.AddRazorComment(tmap.Description);
                    _scriptBuilder.WaitForGump(Constants.TMAP_TOME_GUMP_ID);
                    _scriptBuilder.GumpResponse(Constants.TMAP_TOME_GUMP_ID, tmap.GumpResponseButtonForTome);
                }

                _scriptBuilder.AddOverhead("Treasure maps done.");
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.GumpClose(Constants.TMAP_TOME_GUMP_ID);
            }
        }

        private void GenerateAspectTomeScripts(IEnumerable<ILootItem> extracts, IEnumerable<ILootItem> cores)
        {
            if (extracts.Count() > 0 || cores.Count() > 0)
            {
                _scriptBuilder.AddGCDWait();
                _scriptBuilder.AddOverhead("Starting aspect items...");
                _scriptBuilder.AddRazorComment("Beginning Aspect Tome");
                _scriptBuilder.DoubleClickAspectTome();
                _scriptBuilder.AddGCDWait();

                foreach (ILootItem core in cores)
                {
                    _scriptBuilder.AddRazorComment(core.Description);
                    _scriptBuilder.WaitForGump(Constants.ASPECT_TOME_GUMP_ID);
                    _scriptBuilder.GumpResponse(Constants.ASPECT_TOME_GUMP_ID, core.GumpResponseButtonForTome);
                }

                foreach (ILootItem extract in extracts)
                {
                    _scriptBuilder.AddRazorComment(extract.Description);
                    _scriptBuilder.WaitForGump(Constants.ASPECT_TOME_GUMP_ID);
                    _scriptBuilder.GumpResponse(Constants.ASPECT_TOME_GUMP_ID, extract.GumpResponseButtonForTome);
                }

                _scriptBuilder.AddOverhead("Aspect items done.");
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
    }
}