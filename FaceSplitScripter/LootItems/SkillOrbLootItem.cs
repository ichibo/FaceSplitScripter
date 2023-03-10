using FaceSplitScripter;

namespace FaceSplitScripter.LootItems
{
    internal class SkillOrbLootItem: ILootItem
    {
        public LootType LootType { get { return LootType.SkillOrb; } }
        public int TomePage { get { return 0; } }
        public string GumpResponseButtonForTome { get { return "none"; } }
        public string Description { get; private set; }

        public SkillOrbLootItem(string description)
        {
            Description = description;
        }
    }
}
