namespace FaceSplitScripter
{
    public class AspectCoreLootItem : ILootItem
    {
        public LootType LootType { get { return LootType.Core; } }
        public Aspect Aspect { get; private set; }
        public int TomePage { get { return 1; } }
        public string Description { get; private set; }

        public AspectCoreLootItem(string description, Aspect aspect)
        {
            Description = description;
            Aspect = aspect;
        }

        public string GumpResponseButtonForTome
        {
            get
            {
                switch (Aspect)
                {
                    case Aspect.Air:
                        return "10";
                    case Aspect.Artisan:
                        return "11";
                    case Aspect.Blood:
                        return "12";
                    case Aspect.Command:
                        return "13";
                    case Aspect.Death:
                        return "14";
                    case Aspect.Discipline:
                        return "15";
                    case Aspect.Earth:
                        return "16";
                    case Aspect.Eldritch:
                        return "17";
                    case Aspect.Fire:
                        return "18";
                    case Aspect.Fortune:
                        return "19";
                    case Aspect.Holy:
                        return "20";
                    case Aspect.Lyric:
                        return "21";
                    case Aspect.Poison:
                        return "22";
                    case Aspect.Shadow:
                        return "23";
                    case Aspect.Void:
                        return "24";
                    case Aspect.War:
                        return "25";
                    case Aspect.Water:
                        return "26";

                    default:
                        return "0";
                }
            }
        }

    }
}
