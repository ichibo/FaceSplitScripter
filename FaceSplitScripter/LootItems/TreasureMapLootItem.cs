namespace FaceSplitScripter
{
    internal class TreasureMapLootItem: ILootItem
    {
        public LootType LootType { get { return LootType.TreasureMap; } }
        public TreasureMapLevel TreasureMapLevel { get; private set; }
        public int TomePage { get { return 1; } }
        public string Description { get; private set; }

        public TreasureMapLootItem(string description, TreasureMapLevel treasureMapLevel)
        {
            Description = description;
            TreasureMapLevel = treasureMapLevel;
        }

        public string GumpResponseButtonForTome
        {
            get
            {
                switch (TreasureMapLevel)
                {
                    case TreasureMapLevel.Level1:
                        return "10";
                    case TreasureMapLevel.Level2:
                        return "11";
                    case TreasureMapLevel.Level3:
                        return "12";
                    case TreasureMapLevel.Level4:
                        return "13";
                    case TreasureMapLevel.Level5:
                        return "14";
                    case TreasureMapLevel.Level6:
                        return "15";

                    default:
                        return "0";
                }
            }
        }
    }
}

