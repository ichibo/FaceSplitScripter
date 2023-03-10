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
        public const string SKLL_ORB_VARIABLE_NAME = "faceSplitSkillOrb";
        public const string MCD_VARIABLE_NAME = "faceSplitMcd";

        // Item Types
        public const string TOME_ITEM_TYPE = "29104";
        public const string SKILL_ORB_ITEM_TYPE = "22336";
        public const string MCD_ITEM_TYPE = "17087";

        // Item Hues
        public const string ASPECT_TOME_HUE = "2618";
        public const string TMAP_TOME_HUE = "2990";
        public const string SKILLSCROLL_TOME_HUE = "2963";
        
        // Script configs
        public const int GLOBAL_COOLDOWN_IN_MSECS = 1000;

        // Gump IDs
        public const string ASPECT_TOME_GUMP_ID = "265325939";
        public const string TMAP_TOME_GUMP_ID = "1863945839";
        public const string SKILLSCROLL_TOME_GUMP_ID = "2125225775";

        // Gump specifics
        public const string NEXT_PAGE_FOR_SKILLSCROLL_TOME = "5";

        // Identifiers from the sheet to determine what the loot type is.  Skillscroll is the default.
        public const string CORE_IDENTIFIER = "core";
        public const string EXTRACT_IDENTIFIER = "extr";
        public const string TREASURE_MAP_IDENTIFIER = "lvl";
        public const string SKILL_ORB_IDENTIFIER = "skill orb";
        public const string MCD_IDENTIFIER = "mcd";
    }
}
