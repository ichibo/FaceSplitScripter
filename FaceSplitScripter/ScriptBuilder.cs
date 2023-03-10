using System.Text;

namespace FaceParser
{
    public class ScriptBuilder
    {
        const string LOOTSPLIT_CONTAINER_VARIABLE_NAME = "faceSplitContainer";

        const string TREASURE_MAP_TOME_VARIABLE_NAME = "faceSplitTmapTome";
        const string ASPECT_TOME_VARIABLE_NAME = "faceSplitAspectTome";
        const string SKILLSCROLL_TOME_VARIABLE_NAME = "faceSplitScrollTome";
        const string SKLL_ORB_VARIABLE_NAME = "faceSplitSkillOrb";
        const string MCD_VARIABLE_NAME = "faceSplitMcd";

        public const string TOME_ITEM_TYPE = "29104";
        public const string SKILL_ORB_ITEM_TYPE = "22336";
        public const string MCD_ITEM_TYPE = "17087";

        public const string ASPECT_TOME_HUE = "2618";
        public const string TMAP_TOME_HUE = "2990";
        public const string SKILLSCROLL_TOME_HUE = "2963";

        const int GLOBAL_COOLDOWN_IN_MSECS = 1000;

        private StringBuilder _scriptBuilder;
        private StringBuilder _errorBuilder;

        public ScriptBuilder()
        {
            _scriptBuilder = new StringBuilder();
            _errorBuilder = new StringBuilder();
        }

        public string GetScript()
        {
            return _scriptBuilder.ToString();
        }

        public string GetErrors()
        {
            return _errorBuilder.ToString();
        }

        public void InitializeVars()
        {
            _scriptBuilder.AppendLine($"unsetvar '{LOOTSPLIT_CONTAINER_VARIABLE_NAME}'");

            _scriptBuilder.AppendLine($"unsetvar '{TREASURE_MAP_TOME_VARIABLE_NAME}'");
            _scriptBuilder.AppendLine($"unsetvar '{ASPECT_TOME_VARIABLE_NAME}'");
            _scriptBuilder.AppendLine($"unsetvar '{SKILLSCROLL_TOME_VARIABLE_NAME}'");

            _scriptBuilder.AppendLine($"unsetvar '{SKLL_ORB_VARIABLE_NAME}'");
            _scriptBuilder.AppendLine($"unsetvar '{MCD_VARIABLE_NAME}'");
        }

        public void DoubleClickTreasureMapTome()
        {
            _scriptBuilder.AppendLine($"dclick '{TREASURE_MAP_TOME_VARIABLE_NAME}'");
        }

        public void DoubleClickAspectTome()
        {
            _scriptBuilder.AppendLine($"dclick '{ASPECT_TOME_VARIABLE_NAME}'");
        }

        public void DoubleClickSkillScrollTome()
        {
            _scriptBuilder.AppendLine($"dclick '{SKILLSCROLL_TOME_VARIABLE_NAME}'");
        }

        public void AddGCDWait()
        {
            _scriptBuilder.AppendLine($"wait {GLOBAL_COOLDOWN_IN_MSECS}");
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
            _scriptBuilder.AppendLine($"setvar '{LOOTSPLIT_CONTAINER_VARIABLE_NAME}'");
            _scriptBuilder.AppendLine($"dclick '{LOOTSPLIT_CONTAINER_VARIABLE_NAME}'");
            AddGCDWait();
        }

        public void SetScriptIds()
        {
            _scriptBuilder.AppendLine($"if findtype '{TOME_ITEM_TYPE}' '{LOOTSPLIT_CONTAINER_VARIABLE_NAME}' '{ASPECT_TOME_HUE}' as tempVariableA");
            _scriptBuilder.AppendLine($"setvar '{ASPECT_TOME_VARIABLE_NAME}' tempVariableA");

            _scriptBuilder.AppendLine($"if findtype '{TOME_ITEM_TYPE}' '{LOOTSPLIT_CONTAINER_VARIABLE_NAME}' '{TMAP_TOME_HUE}' as tempVariableB");
            _scriptBuilder.AppendLine($"setvar '{TREASURE_MAP_TOME_VARIABLE_NAME}' tempVariableB");

            _scriptBuilder.AppendLine($"if findtype '{TOME_ITEM_TYPE}' '{LOOTSPLIT_CONTAINER_VARIABLE_NAME}' '{SKILLSCROLL_TOME_HUE}' as tempVariableC");
            _scriptBuilder.AppendLine($"setvar '{SKILLSCROLL_TOME_VARIABLE_NAME}' tempVariableC");

            _scriptBuilder.AppendLine($"if findtype '{SKILL_ORB_ITEM_TYPE}' '{LOOTSPLIT_CONTAINER_VARIABLE_NAME}' as tempVariableD");
            _scriptBuilder.AppendLine($"setvar '{SKLL_ORB_VARIABLE_NAME}' tempVariableD");

            _scriptBuilder.AppendLine($"if findtype '{MCD_ITEM_TYPE}' '{LOOTSPLIT_CONTAINER_VARIABLE_NAME}' as tempVariableE");
            _scriptBuilder.AppendLine($"setvar '{MCD_VARIABLE_NAME}' tempVariableE");
        }

        public void LiftSkillOrb(int quantity)
        {
            _scriptBuilder.AppendLine($"lift '{SKLL_ORB_VARIABLE_NAME}' {quantity}");
        }

        public void LiftMCD(int quantity)
        {
            _scriptBuilder.AppendLine($"lift '{MCD_VARIABLE_NAME}' {quantity}");
        }

        public void DropOnSelf()
        {
            _scriptBuilder.AppendLine($"drop 'self'");
        }

        public void AddComment(string text)
        {
            _scriptBuilder.AppendLine($"// {text} //");
        }

        public void AddOverhead(string text)
        {
            _scriptBuilder.AppendLine($"overhead '{text}' 0");
        }

        public void AddErroredItem(string text)
        {
            text.Trim();
            _errorBuilder.AppendLine(text);
        }
    }
}
