using FaceParser;

namespace FaceSplitScripter.LootItems
{
    internal class MCDLootItem: ILootItem
    {
        public LootType LootType { get { return LootType.MCD; } }
        public int TomePage { get { return 0; } }
        public string GumpResponseButtonForTome { get { return "none"; } }

        public MCDLootItem()
        {
        }
    }
}
