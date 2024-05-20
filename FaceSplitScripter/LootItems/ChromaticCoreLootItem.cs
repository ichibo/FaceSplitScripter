using FaceSplitScripter;

namespace FaceSplitScripter.LootItems
{
    internal class ChromaticCoreLootItem: ILootItem
    {
        public LootType LootType { get { return LootType.ChromaticCore; } }
        public int TomePage { get { return 0; } }
        public string GumpResponseButtonForTome { get { return "none"; } }
        public string Description { get; private set; }

        public ChromaticCoreLootItem(string description)
        {
            Description = description;
        }
    }
}
