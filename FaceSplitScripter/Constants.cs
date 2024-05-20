using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceSplitScripter
{
    internal static class Constants
    {
        // Script variable names
        public const string LOOTSPLIT_CONTAINER_VARIABLE_NAME = "faceSplitContainer";
        public const string TREASURE_MAP_TOME_VARIABLE_NAME = "faceSplitTmapTome";
        public const string ASPECT_TOME_VARIABLE_NAME = "faceSplitAspectTome";
        public const string SKILLSCROLL_TOME_VARIABLE_NAME = "faceSplitScrollTome";

        public const string LUMBER_TOME_VARIABLE_NAME = "faceSplitLumberTome";
        public const string ORE_TOME_VARIABLE_NAME = "faceSplitOreTome";
        public const string SKINNING_TOME_VARIABLE_NAME = "faceSplitSkinningTome";
        public const string FISHING_TOME_VARIABLE_NAME = "faceSplitFishingTome";

        public const string MISSING_ITEMS_LIST_NAME = "faceSplitMissingList";

        // Item Types
        public const string TOME_ITEM_TYPE = "29104";


        // Item Hues
        public const string ASPECT_TOME_HUE = "2618";
        public const string TMAP_TOME_HUE = "2990";
        public const string SKILLSCROLL_TOME_HUE = "2963";
        public const string LUMBER_TOME_HUE = "2799";
        public const string ORE_TOME_HUE = "2796";
        public const string SKINNING_TOME_HUE = "2651";
        public const string FISHING_TOME_HUE = "2722";

        // Script configs
        public const int GLOBAL_COOLDOWN_IN_MSECS = 1000;

        // Gump IDs
        public const string ASPECT_TOME_GUMP_ID = "265325939";
        public const string TMAP_TOME_GUMP_ID = "1863945839";
        public const string SKILLSCROLL_TOME_GUMP_ID = "2125225775";
        public const string LUMBER_TOME_GUMP_ID = "3576069391";
        public const string ORE_TOME_GUMP_ID = "1667380559";
        public const string SKINNING_TOME_GUMP_ID = "4027121519";
        public const string FISHING_TOME_GUMP_ID = "3448468591";

        // Gump specifics
        public const string NEXT_PAGE_FOR_SKILLSCROLL_TOME = "5";
        public const string NEXT_PAGE_FOR_ASPECT_CORE_TOME = "6";
        public const string NEXT_ITEM_TYPE_PAGE_FOR_ASPECT_CORE_TOME = "8";

        // Identifiers from the sheet to determine what the loot type is.  Skillscroll is the default.
        public const string CORE_IDENTIFIER = "core";
        public const string EXTRACT_IDENTIFIER = "extr";
        public const string DISTILL_IDENTIFIER = "distill";
        public const string TREASURE_MAP_IDENTIFIER = "lvl";
        public const string SKILL_ORB_IDENTIFIER = "skill orb";
        public const string MCD_IDENTIFIER = "mcd";

        public const string CHROMATIC_IDENTIFIER = "chromatic";
        public const string LUMBER_MAP_IDENTIFIER = "lumber map";
        public const string SKINNING_MAP_IDENTIFIER = "skinning map";
        public const string MINING_MAP_IDENTIFIER = "ore map";
        public const string FISHING_MAP_IDENTIFIER = "fishing map";

        public const string FULL_SPLIT_STRING = @"Skill Orb(1) ,MCD(1) ,LVL1(1) ,LVL2(1) ,LVL3(1) ,LVL4(1) ,LVL5(1) ,LVL6(1) ,LVL7(1) ,Dull Copper Ore Map(1) ,Shadow Iron Ore Map(1) ,Copper Ore Map(1) ,Bronze Ore Map(1) ,Gold Ore Map(1) ,Agapite Ore Map(1) ,Verite Ore Map(1) ,Valorite Ore Map(1) ,Avarite Ore Map(1) ,Dullhide Skinning Map(1) ,Shadowhide Skinning Map(1) ,Copperhide Skinning Map(1) ,Bronzehide Skinning Map(1) ,Goldenhide Skinning Map(1) ,Rosehide Skinning Map(1) ,Verehide Skinning Map(1) ,Valehide Skinning Map(1) ,Avarhide Skinning Map(1) ,Dullscale Fishing Map(1) ,Shadowscale Fishing Map(1) ,Copperscale Fishing Map(1) ,Bronzescale Fishing Map(1) ,Goldenscale Fishing Map(1) ,Rosescale Fishing Map(1) ,Verescale Fishing Map(1) ,Valescale Fishing Map(1) ,Avarscale Fishing Map(1) ,Dullwood Lumber Map(1) ,Shadowwood Lumber Map(1) ,Copperwood Lumber Map(1) ,Bronzewood Lumber Map(1) ,Goldenwood Lumber Map(1) ,Rosewood Lumber Map(1) ,Verewood Lumber Map(1) ,Valewood Lumber Map(1) ,Avarwood Lumber Map(1) ,CORE Air(1) ,CORE Arcane(1) ,CORE Artisan(1) ,CORE Blood(1) ,CORE Chromatic(1) ,CORE Command(1) ,CORE Death(1) ,CORE Discipline(1) ,CORE Earth(1) ,CORE Eldritch(1) ,CORE Fire(1) ,CORE Fortune(1) ,CORE Frost(1) ,CORE Gadget(1) ,CORE Harvest(1) ,CORE Holy(1) ,CORE Lightning(1) ,CORE Lyric(1) ,CORE Madness(1) ,CORE Poison(1) ,CORE Shadow(1) ,CORE Void(1) ,CORE Water(1) ,EXTR Air(1) ,EXTR Arcane(1) ,EXTR Artisan(1) ,EXTR Blood(1) ,EXTR Chromatic(1) ,EXTR Command(1) ,EXTR Death(1) ,EXTR Discipline(1) ,EXTR Earth(1) ,EXTR Eldritch(1) ,EXTR Fire(1) ,EXTR Fortune(1) ,EXTR Frost(1) ,EXTR Gadget(1) ,EXTR Harvest(1) ,EXTR Holy(1) ,EXTR Lightning(1) ,EXTR Lyric(1) ,EXTR Madness(1) ,EXTR Poison(1) ,EXTR Shadow(1) ,EXTR Void(1) ,EXTR Water(1) ,Alchy(1) ,Animal Lore(1) ,Animal Tame(1) ,Arms Lore(1) ,Beg(1) ,Blacksmith(1) ,Camp(1) ,Carpentry(1) ,Carto(1) ,Chivalry(1) ,Cook(1) ,Detect(1) ,Discordance(1) ,Fish(1) ,Forensic(1) ,Herd(1) ,Inscript(1) ,Item ID(1) ,Lockpick(1) ,Lumber(1) ,Mine(1) ,Music(1) ,Necromancy(1) ,Peace(1) ,Poison(1) ,Provo(1) ,Spirit(1) ,Stealth(1) ,Tailor(1) ,Taste(1) ,Tinker(1) ,Track(1) ,Veterinary(1)";
    }

    internal interface INonTomeItemDetails
    {
        string ItemType { get; }
        string ItemName { get; }
        string VariableName { get; }
        string Hue { get; }
        string Description { get; }
    }

    public class MCDItemDetails : INonTomeItemDetails
    {
        public string ItemType => "17087";
        public string ItemName => "book of truth";
        public string VariableName => "faceSplitMcd";
        public string Hue => "0";
        public string Description => "MCD";
    }

    public class SkillOrbItemDetails : INonTomeItemDetails
    {
        public string ItemType => "22336";
        public string ItemName => "void orb";
        public string VariableName => "faceSplitSkillOrb";
        public string Hue => "2966";
        public string Description => "Skill Orb";
    }

    public class ChromaticCoreItemDetails : INonTomeItemDetails
    {
        public string ItemType => "4025";
        public string ItemName => "chroma core";
        public string VariableName => "faceSplitChromaticCore";
        public string Hue => "0";
        public string Description => "Chromatic Core";
    }

    public class ChromaticDistillItemDetails : INonTomeItemDetails
    {
        public string ItemType => "4026";
        public string ItemName => "chroma distil";
        public string VariableName => "faceSplitChromaticDistill";
        public string Hue => "0";
        public string Description => "Chromatic Distill";
    }

    public static class NonTomeItemDetails
    {
        internal static INonTomeItemDetails MCD => new MCDItemDetails();
        internal static INonTomeItemDetails SkillOrb => new SkillOrbItemDetails();
        internal static INonTomeItemDetails ChromaticCore => new ChromaticCoreItemDetails();
        internal static INonTomeItemDetails ChromaticDistill => new ChromaticDistillItemDetails();

        internal static IEnumerable<INonTomeItemDetails> GetAllItems()
        {
            yield return MCD;
            yield return SkillOrb;
            yield return ChromaticCore;
            yield return ChromaticDistill;
        }
    }
}
