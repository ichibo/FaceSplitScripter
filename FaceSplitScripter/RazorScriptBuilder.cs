using FaceSplitScripter;
using System;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace FaceSplitScripter
{
    public class RazorScriptBuilder
    {
        private StringBuilder _scriptBuilder;
        private StringBuilder _manualItems;

        public RazorScriptBuilder()
        {
            _scriptBuilder = new StringBuilder();
            _manualItems = new StringBuilder();
        }

        public string GetScript()
        {
            return _scriptBuilder.ToString();
        }

        public string GetManualItems()
        {
            return _manualItems.ToString();
        }

        public void InitializeVars()
        {
            _scriptBuilder.AppendLine($"unsetvar '{Constants.LOOTSPLIT_CONTAINER_VARIABLE_NAME}'");

            _scriptBuilder.AppendLine($"unsetvar '{Constants.TREASURE_MAP_TOME_VARIABLE_NAME}'");
            _scriptBuilder.AppendLine($"unsetvar '{Constants.ASPECT_TOME_VARIABLE_NAME}'");
            _scriptBuilder.AppendLine($"unsetvar '{Constants.SKILLSCROLL_TOME_VARIABLE_NAME}'");

            _scriptBuilder.AppendLine($"unsetvar '{Constants.SKLL_ORB_VARIABLE_NAME}'");
            _scriptBuilder.AppendLine($"unsetvar '{Constants.MCD_VARIABLE_NAME}'");


            _scriptBuilder.AppendLine($"removelist {Constants.MISSING_ITEMS_LIST_NAME}");
            _scriptBuilder.AppendLine($"createlist {Constants.MISSING_ITEMS_LIST_NAME}");

            _scriptBuilder.AppendLine($"clearsysmsg");
        }

        public void DoubleClickTreasureMapTome()
        {
            _scriptBuilder.AppendLine($"dclick '{Constants.TREASURE_MAP_TOME_VARIABLE_NAME}'");
        }

        public void DoubleClickAspectTome()
        {
            _scriptBuilder.AppendLine($"dclick '{Constants.ASPECT_TOME_VARIABLE_NAME}'");
        }

        public void DoubleClickSkillScrollTome()
        {
            _scriptBuilder.AppendLine($"dclick '{Constants.SKILLSCROLL_TOME_VARIABLE_NAME}'");
        }

        public void AddGCDWait()
        {
            _scriptBuilder.AppendLine($"wait {Constants.GLOBAL_COOLDOWN_IN_MSECS}");
        }

        public void WaitForGump(string gumpId)
        {
            _scriptBuilder.AppendLine($"waitforgump {gumpId}");
        }

        public void GumpResponse(string gumpId, string response)
        {
            _scriptBuilder.AppendLine($"gumpresponse {response} {gumpId}");
        }

        public void GumpClose(string gumpId)
        {
            _scriptBuilder.AppendLine($"gumpclose {gumpId}");
        }

        public void GetTargetContainerForScript()
        {
            _scriptBuilder.AppendLine($"setvar '{Constants.LOOTSPLIT_CONTAINER_VARIABLE_NAME}'");
            _scriptBuilder.AppendLine($"dclick '{Constants.LOOTSPLIT_CONTAINER_VARIABLE_NAME}'");
            AddGCDWait();
        }

        public void SetScriptIds()
        {
            _scriptBuilder.AppendLine($"if findtype '{Constants.TOME_ITEM_TYPE}' '{Constants.LOOTSPLIT_CONTAINER_VARIABLE_NAME}' '{Constants.ASPECT_TOME_HUE}' as tempVariableA");
            _scriptBuilder.AppendLine($"setvar '{Constants.ASPECT_TOME_VARIABLE_NAME}' tempVariableA");
            _scriptBuilder.AppendLine($"endif");

            _scriptBuilder.AppendLine($"if findtype '{Constants.TOME_ITEM_TYPE}' '{Constants.LOOTSPLIT_CONTAINER_VARIABLE_NAME}' '{Constants.TMAP_TOME_HUE}' as tempVariableB");
            _scriptBuilder.AppendLine($"setvar '{Constants.TREASURE_MAP_TOME_VARIABLE_NAME}' tempVariableB");
            _scriptBuilder.AppendLine($"endif");

            _scriptBuilder.AppendLine($"if findtype '{Constants.TOME_ITEM_TYPE}' '{Constants.LOOTSPLIT_CONTAINER_VARIABLE_NAME}' '{Constants.SKILLSCROLL_TOME_HUE}' as tempVariableC");
            _scriptBuilder.AppendLine($"setvar '{Constants.SKILLSCROLL_TOME_VARIABLE_NAME}' tempVariableC");
            _scriptBuilder.AppendLine($"endif");

            _scriptBuilder.AppendLine($"if findtype '{Constants.SKILL_ORB_ITEM_TYPE}' '{Constants.LOOTSPLIT_CONTAINER_VARIABLE_NAME}' as tempVariableD");
            _scriptBuilder.AppendLine($"setvar '{Constants.SKLL_ORB_VARIABLE_NAME}' tempVariableD");
            _scriptBuilder.AppendLine($"endif");

            _scriptBuilder.AppendLine($"if findtype '{Constants.MCD_ITEM_TYPE}' '{Constants.LOOTSPLIT_CONTAINER_VARIABLE_NAME}' as tempVariableE");
            _scriptBuilder.AppendLine($"setvar '{Constants.MCD_VARIABLE_NAME}' tempVariableE");
            _scriptBuilder.AppendLine($"endif");
        }

        public void LiftSkillOrb(int quantity)
        {
            _scriptBuilder.AppendLine($"lift '{Constants.SKLL_ORB_VARIABLE_NAME}' {quantity}");
        }

        public void LiftMCD(int quantity)
        {
            _scriptBuilder.AppendLine($"lift '{Constants.MCD_VARIABLE_NAME}' {quantity}");
        }

        public void DropOnSelf()
        {
            _scriptBuilder.AppendLine($"drop 'self'");
        }

        public void AddRazorComment(string text)
        {
            _scriptBuilder.AppendLine($"// {text} //");
        }

        public void AddOverhead(string text)
        {
            _scriptBuilder.AppendLine($"overhead '{text}' 0");
        }

        public void AddManualItem(string text)
        {
            _manualItems.AppendLine(text);
        }

        internal void AddMissingItemCheck(object missingItemText, string description)
        {
            string updatedDescription = ScriptUtilities.RemoveQuantityFromText(description);

            _scriptBuilder.AppendLine($"if insysmsg '{missingItemText}'");
            _scriptBuilder.AppendLine($"pushlist '{Constants.MISSING_ITEMS_LIST_NAME}' '{updatedDescription}' back");
            _scriptBuilder.AppendLine($"endif");
        }

        internal void DisplayMissingItems()
        {
            _scriptBuilder.AppendLine($"if list '{Constants.MISSING_ITEMS_LIST_NAME}' '>' 0");
            _scriptBuilder.AppendLine($"overhead 'Missing items:' 38");
            _scriptBuilder.AppendLine($"endif");

            _scriptBuilder.AppendLine($"foreach 'missingItem' in '{Constants.MISSING_ITEMS_LIST_NAME}'");
            _scriptBuilder.AppendLine($"overhead missingItem 38");
            _scriptBuilder.AppendLine($"endfor");
        }
    }
}
