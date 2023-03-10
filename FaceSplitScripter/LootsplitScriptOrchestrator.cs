using FaceSplitScripter.LootItems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FaceParser
{
    public class LootsplitScriptOrchestrator
    {
        public const string ASPECT_TOME_GUMP_ID = "265325939";
        public const string TMAP_TOME_GUMP_ID = "1863945839";
        public const string SKILLSCROLL_TOME_GUMP_ID = "2125225775";

        public const string NEXT_PAGE_FOR_SKILLSCROLL_TOME = "5";

        public const string CORE_IDENTIFIER = "core";
        public const string EXTRACT_IDENTIFIER = "extr";
        public const string TREASURE_MAP_IDENTIFIER = "lvl";
        public const string SKILL_ORB_IDENTIFIER = "skill orb";
        public const string MCD_IDENTIFIER = "mcd";

        private ScriptBuilder _scriptBuilder;

        public LootsplitScriptOrchestrator()
        {
            _scriptBuilder = new ScriptBuilder();
        }

        public ScriptBuilder ConvertLootsplitTextToRazorMacro(string text)
        {
            IEnumerable<ILootItem> lootItems = ParseFullLootsplitText(text);
            return CreateRazorMacroFromLootItems(lootItems);
        }

        public IEnumerable<ILootItem> ParseFullLootsplitText(string text)
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

        public IEnumerable<ILootItem> ParseSingleLootsplitText(string text)
        {
            int itemCount = GetQuantityFromText(text);
            List<ILootItem> items = new List<ILootItem>();

            if (text.ToLower().Contains(SKILL_ORB_IDENTIFIER))
            {
                for (int i = 0; i < itemCount; i++)
                {
                    items.Add(new SkillOrbLootItem());
                }
            }

            else if (text.ToLower().Contains(MCD_IDENTIFIER))
            {
                for (int i = 0; i < itemCount; i++)
                {
                    items.Add(new MCDLootItem());
                }
            }

            else if(text.ToLower().Contains(CORE_IDENTIFIER))
            {
                Aspect aspect = EnumUtils.GetAspectFromText(text);

                if (aspect != Aspect.None)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        items.Add(new AspectCoreLootItem(aspect));
                    }
                }

                else
                {
                    _scriptBuilder.AddErroredItem(text);
                }
            }

            else if (text.ToLower().Contains(EXTRACT_IDENTIFIER))
            {
                Aspect aspect = EnumUtils.GetAspectFromText(text);

                if (aspect != Aspect.None)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        items.Add(new AspectExtractLootItem(aspect));
                    }
                }

                else
                {
                    _scriptBuilder.AddErroredItem(text);
                }
            }

            else if (text.ToLower().Contains(TREASURE_MAP_IDENTIFIER))
            {
                TreasureMapLevel tmapLevel = EnumUtils.GetTreasureMapLevelFromText(text);

                if (tmapLevel != TreasureMapLevel.None)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        items.Add(new TreasureMapLootItem(tmapLevel));
                    }
                }

                else
                {
                    _scriptBuilder.AddErroredItem(text);
                }
            }

            // Anything else might be a skill scroll since they don't have any static identifier
            else
            {
                SkillScroll skillScroll = EnumUtils.GetSkillScrollFromText(text);

                if (skillScroll != SkillScroll.None)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        int skillScrollPage = EnumUtils.GetSkillScrollPage(skillScroll);
                        items.Add(new SkillScrollLootItem(skillScroll, skillScrollPage));
                    }
                }

                else
                {
                    _scriptBuilder.AddErroredItem(text);
                }
            }

            return items;
        }

        public int GetQuantityFromText(string text)
        {
            int quantityStartIndex = text.IndexOf('(') + 1;
            int quantityEndIndex = text.IndexOf(')');
            int quantityLength = quantityEndIndex - quantityStartIndex;

            string quantityText = text.Substring(quantityStartIndex, quantityLength);

            try
            {
                int quantity = Int32.Parse(quantityText);
                return quantity;
            }
            catch
            {
                return 1;
            }
        }

        public ScriptBuilder CreateRazorMacroFromLootItems(IEnumerable<ILootItem> lootItems)
        {
            // 0. SetVar to tome chest.
            _scriptBuilder.AddOverhead("-- Target the tome container --");
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

            // 2.Iterate on each item per tome and do the gump click once.
            if (skillOrbs.Length > 0)
            {
                _scriptBuilder.AddGCDWait();
                GenerateSkillOrbScripts(skillOrbs);
            }

            if (mcds.Length > 0)
            {
                _scriptBuilder.AddGCDWait();
                GenerateMCDScripts(mcds);
            }

            if (extracts.Length > 0 || cores.Length > 0)
            {
                _scriptBuilder.AddGCDWait();
                GenerateAspectTomeScripts(extracts, cores);
            }

            if (tmaps.Length > 0)
            {
                _scriptBuilder.AddGCDWait();
                GenerateTmapsTomeScripts(tmaps);
            }

            if (skillScrolls.Length > 0)
            {
                _scriptBuilder.AddGCDWait();
                GenerateSkillScrollTomeScripts(skillScrolls);
            }

            _scriptBuilder.AddOverhead("All loot complete!");

            return _scriptBuilder;
        }

        private void GenerateSkillOrbScripts(ILootItem[] skillOrbs)
        {
            _scriptBuilder.AddOverhead("Starting skill orbs...");
            _scriptBuilder.AddComment("Beginning Skill Orbs");

            int quantity = skillOrbs.Length;

            _scriptBuilder.LiftSkillOrb(quantity);
            _scriptBuilder.AddGCDWait();
            _scriptBuilder.DropOnSelf();
            _scriptBuilder.AddGCDWait();

            _scriptBuilder.AddOverhead("Skill orbs done.");
        }

        private void GenerateMCDScripts(ILootItem[] mcds)
        {
            _scriptBuilder.AddOverhead("Starting MCDs...");
            _scriptBuilder.AddComment("Beginning MCDs");

            int quantity = mcds.Length;

            _scriptBuilder.LiftMCD(quantity);
            _scriptBuilder.AddGCDWait();
            _scriptBuilder.DropOnSelf();
            _scriptBuilder.AddGCDWait();

            _scriptBuilder.AddOverhead("MCDs done.");
        }

        private void GenerateSkillScrollTomeScripts(ILootItem[] skillScrolls)
        {
            _scriptBuilder.AddOverhead("Starting skill scrolls...");
            _scriptBuilder.AddComment("Beginning Skill Scroll Tome");
            _scriptBuilder.DoubleClickSkillScrollTome();
            _scriptBuilder.AddGCDWait();

            ILootItem[] pageOneScrolls = skillScrolls.Where(x => x.TomePage == 1).ToArray();
            ILootItem[] pageTwoScrolls = skillScrolls.Where(x => x.TomePage == 2).ToArray();

            foreach (ILootItem scroll in pageOneScrolls)
            {
                _scriptBuilder.WaitForGump(SKILLSCROLL_TOME_GUMP_ID);
                _scriptBuilder.GumpResponse(SKILLSCROLL_TOME_GUMP_ID, scroll.GumpResponseButtonForTome);
            }

            if (pageTwoScrolls.Length > 0)
            {
                _scriptBuilder.AddComment("Skill Scroll Tome Page 2");
                _scriptBuilder.WaitForGump(SKILLSCROLL_TOME_GUMP_ID);
                _scriptBuilder.GumpResponse(SKILLSCROLL_TOME_GUMP_ID, NEXT_PAGE_FOR_SKILLSCROLL_TOME);

                foreach (ILootItem scroll in pageTwoScrolls)
                {
                    _scriptBuilder.WaitForGump(SKILLSCROLL_TOME_GUMP_ID);
                    _scriptBuilder.GumpResponse(SKILLSCROLL_TOME_GUMP_ID, scroll.GumpResponseButtonForTome);
                }
            }

            _scriptBuilder.AddOverhead("Skill scrolls done.");
            _scriptBuilder.AddGCDWait();
            _scriptBuilder.GumpClose(SKILLSCROLL_TOME_GUMP_ID);
        }

        private void GenerateTmapsTomeScripts(ILootItem[] tmaps)
        {
            _scriptBuilder.AddOverhead("Starting treasure maps...");
            _scriptBuilder.AddComment("Beginning Treasure Map Tome");
            _scriptBuilder.DoubleClickTreasureMapTome();
            _scriptBuilder.AddGCDWait();

            foreach (ILootItem tmap in tmaps)
            {
                _scriptBuilder.WaitForGump(TMAP_TOME_GUMP_ID);
                _scriptBuilder.GumpResponse(TMAP_TOME_GUMP_ID, tmap.GumpResponseButtonForTome);
            }

            _scriptBuilder.AddOverhead("Treasure maps done.");
            _scriptBuilder.AddGCDWait();
            _scriptBuilder.GumpClose(TMAP_TOME_GUMP_ID);
        }

        private void GenerateAspectTomeScripts(ILootItem[] extracts, ILootItem[] cores)
        {
            _scriptBuilder.AddOverhead("Starting aspect items...");
            _scriptBuilder.AddComment("Beginning Aspect Tome");
            _scriptBuilder.DoubleClickAspectTome();
            _scriptBuilder.AddGCDWait();

            foreach (ILootItem core in cores)
            {
                _scriptBuilder.WaitForGump(ASPECT_TOME_GUMP_ID);
                _scriptBuilder.GumpResponse(ASPECT_TOME_GUMP_ID, core.GumpResponseButtonForTome);
            }

            foreach (ILootItem extract in extracts)
            {
                _scriptBuilder.WaitForGump(ASPECT_TOME_GUMP_ID);
                _scriptBuilder.GumpResponse(ASPECT_TOME_GUMP_ID, extract.GumpResponseButtonForTome);
            }

            _scriptBuilder.AddOverhead("Aspect items done.");
            _scriptBuilder.AddGCDWait();
            _scriptBuilder.GumpClose(ASPECT_TOME_GUMP_ID);
        }
    }
}