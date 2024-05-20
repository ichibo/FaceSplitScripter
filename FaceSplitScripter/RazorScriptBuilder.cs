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

            foreach (var nonTomeItems in NonTomeItemDetails.GetAllItems())
            {
                _scriptBuilder.AppendLine($"unsetvar '{nonTomeItems.VariableName}'");
            }

            _scriptBuilder.AppendLine($"unsetvar '{Constants.FISHING_TOME_VARIABLE_NAME}'");
            _scriptBuilder.AppendLine($"unsetvar '{Constants.LUMBER_TOME_VARIABLE_NAME}'");
            _scriptBuilder.AppendLine($"unsetvar '{Constants.ORE_TOME_VARIABLE_NAME}'");
            _scriptBuilder.AppendLine($"unsetvar '{Constants.SKINNING_TOME_VARIABLE_NAME}'");

            _scriptBuilder.AppendLine($"removelist {Constants.MISSING_ITEMS_LIST_NAME}");
            _scriptBuilder.AppendLine($"createlist {Constants.MISSING_ITEMS_LIST_NAME}");

            _scriptBuilder.AppendLine($"clearsysmsg");
        }

        public void DoubleClickByVariableName(string variableName)
        {
            _scriptBuilder.AppendLine($"dclick '{variableName}'");
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
            WaitForGump(gumpId);
            _scriptBuilder.AppendLine($"gumpresponse {response} {gumpId}");
            WaitForGump(gumpId);
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
            SetVariableForTopContainerByTypeAndHue(Constants.TOME_ITEM_TYPE, Constants.ASPECT_TOME_HUE, Constants.ASPECT_TOME_VARIABLE_NAME);
            SetVariableForTopContainerByTypeAndHue(Constants.TOME_ITEM_TYPE, Constants.TMAP_TOME_HUE, Constants.TREASURE_MAP_TOME_VARIABLE_NAME);
            SetVariableForTopContainerByTypeAndHue(Constants.TOME_ITEM_TYPE, Constants.SKILLSCROLL_TOME_HUE, Constants.SKILLSCROLL_TOME_VARIABLE_NAME);
            SetVariableForTopContainerByTypeAndHue(Constants.TOME_ITEM_TYPE, Constants.LUMBER_TOME_HUE, Constants.LUMBER_TOME_VARIABLE_NAME);
            SetVariableForTopContainerByTypeAndHue(Constants.TOME_ITEM_TYPE, Constants.ORE_TOME_HUE, Constants.ORE_TOME_VARIABLE_NAME);
            SetVariableForTopContainerByTypeAndHue(Constants.TOME_ITEM_TYPE, Constants.SKINNING_TOME_HUE, Constants.SKINNING_TOME_VARIABLE_NAME);
            SetVariableForTopContainerByTypeAndHue(Constants.TOME_ITEM_TYPE, Constants.FISHING_TOME_HUE, Constants.FISHING_TOME_VARIABLE_NAME);

            foreach (var nonTomeItems in NonTomeItemDetails.GetAllItems())
            {
                SetVariableForTopContainerByNonTomeItemDetails(nonTomeItems);
            }
        }

        private void SetVariableForTopContainerByTypeAndHue(string type, string hue, string variableName)
        {
            _scriptBuilder.AppendLine($"if findtype '{type}' '{Constants.LOOTSPLIT_CONTAINER_VARIABLE_NAME}' '{hue}' as tempVariable");
            _scriptBuilder.AppendLine($"setvar '{variableName}' tempVariable");
            _scriptBuilder.AppendLine($"endif");
        }

        private void SetVariableForTopContainerByNonTomeItemDetails(INonTomeItemDetails nonTomeItemDetails)
        {
            SetVariableForTopContainerByTypeAndHue(nonTomeItemDetails.ItemType, nonTomeItemDetails.Hue, nonTomeItemDetails.VariableName);
        }

        public void LiftItemByVariableName(string variableName, int quantity)
        {
            _scriptBuilder.AppendLine($"lift '{variableName}' {quantity}");
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

        public void AddOverheadAndScript(string text)
        {
            AddRazorComment(text);
            AddOverhead(text);
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

        internal void CheckInsufficientItemsInTopContainerByItemNameHue(string itemName, string hue, string description, int quantity)
        {
            _scriptBuilder.AppendLine($"if not findtype '{itemName}' '{Constants.LOOTSPLIT_CONTAINER_VARIABLE_NAME}' {hue} {quantity}");
            _scriptBuilder.AppendLine($"pushlist '{Constants.MISSING_ITEMS_LIST_NAME}' '{description}(s)' back");
            _scriptBuilder.AppendLine($"overhead 'Insufficient {description}!' 38");
            _scriptBuilder.AppendLine($"endif");
        }

        internal void PullItemToBackpackFromTopContainerByItemNameVariableNameHue(string itemName, string hue, string variableName, int quantity)
        {
            _scriptBuilder.AppendLine($"if findtype '{itemName}' '{Constants.LOOTSPLIT_CONTAINER_VARIABLE_NAME}' {hue}");
            LiftItemByVariableName(variableName, quantity);
            AddGCDWait();
            DropOnSelf();
            AddGCDWait();
            _scriptBuilder.AppendLine($"endif");
        }
    }
}
