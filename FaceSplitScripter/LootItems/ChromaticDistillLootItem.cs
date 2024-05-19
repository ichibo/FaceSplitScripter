using FaceSplitScripter;

namespace FaceSplitScripter.LootItems
{
    internal class ChromaticDistillLootItem: ILootItem
    {
        public LootType LootType { get { return LootType.ChromaticDistill; } }
        public int TomePage { get { return 0; } }
        public string GumpResponseButtonForTome { get { return "none"; } }
        public string Description { get; private set; }

        public ChromaticDistillLootItem(string description)
        {
            Description = description;
        }
    }
}
