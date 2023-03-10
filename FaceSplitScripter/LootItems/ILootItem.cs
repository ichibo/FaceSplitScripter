namespace FaceSplitScripter
{
    public interface ILootItem
    {
        LootType LootType { get; }
        string GumpResponseButtonForTome { get; }
        int TomePage { get; }
        string Description { get; }
    }
}
