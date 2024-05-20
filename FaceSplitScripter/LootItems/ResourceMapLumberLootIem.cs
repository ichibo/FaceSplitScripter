namespace FaceSplitScripter
{
    internal class ResourceMapLumberLootItem: ILootItem
    {
        public LootType LootType { get { return LootType.ResourceMapLumber; } }
        public ResourceType ResourceType { get; private set; }
        public int TomePage { get { return 1; } }
        public string Description { get; private set; }

        public ResourceMapLumberLootItem(string description, ResourceType resourceType)
        {
            Description = description;
            ResourceType = resourceType;
        }

        public string GumpResponseButtonForTome
        {
            get
            {
                switch (ResourceType)
                {
                    case ResourceType.Dull:
                        return "10";
                    case ResourceType.Shadow:
                        return "11";
                    case ResourceType.Copper:
                        return "12";
                    case ResourceType.Bronze:
                        return "13";
                    case ResourceType.Gold:
                        return "14";
                    case ResourceType.Rose:
                        return "15";
                    case ResourceType.Ver:
                        return "16";
                    case ResourceType.Val:
                        return "17";
                    case ResourceType.Ava:
                        return "18";

                    default:
                        return "0";
                }
            }
        }
    }
}

