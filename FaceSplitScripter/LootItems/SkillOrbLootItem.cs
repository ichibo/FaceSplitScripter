using FaceParser;

namespace FaceSplitScripter.LootItems
{
    internal class SkillOrbLootItem: ILootItem
    {
        public LootType LootType { get { return LootType.SkillOrb; } }
        public int TomePage { get { return 0; } }
        public string GumpResponseButtonForTome { get { return "none"; } }

        public SkillOrbLootItem()
        {
        }
    }
}
