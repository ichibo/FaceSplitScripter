namespace FaceSplitScripter
{
    public class AspectExtractLootItem : ILootItem
    {
        public LootType LootType { get { return LootType.Extract; } }
        public Aspect Aspect { get; private set; }
        public int TomePage { get { return 1; } }
        public string Description { get; private set; }

        public AspectExtractLootItem(string description, Aspect aspect)
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
                        return "30";
                    case Aspect.Artisan:
                        return "31";
                    case Aspect.Blood:
                        return "32";
                    case Aspect.Command:
                        return "33";
                    case Aspect.Death:
                        return "34";
                    case Aspect.Discipline:
                        return "35";
                    case Aspect.Earth:
                        return "36";
                    case Aspect.Eldritch:
                        return "37";
                    case Aspect.Fire:
                        return "38";
                    case Aspect.Fortune:
                        return "39";
                    case Aspect.Holy:
                        return "40";
                    case Aspect.Lyric:
                        return "41";
                    case Aspect.Poison:
                        return "42";
                    case Aspect.Shadow:
                        return "43";
                    case Aspect.Void:
                        return "44";
                    case Aspect.War:
                        return "45";
                    case Aspect.Water:
                        return "46";

                    default:
                        return "0";
                }
            }
        }
    }
}
