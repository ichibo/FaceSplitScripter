namespace FaceSplitScripter
{
    public class AspectExtractLootItem : ILootItem
    {
        public LootType LootType { get { return LootType.Extract; } }
        public Aspect Aspect { get; private set; }
        public int TomePage { get { return EnumUtilities.GetAspectTomePage(Aspect); } }
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
                        return "200";
                    case Aspect.Artisan:
                        return "201";
                    case Aspect.Blood:
                        return "202";
                    case Aspect.Command:
                        return "203";
                    case Aspect.Death:
                        return "204";
                    case Aspect.Discipline:
                        return "205";
                    case Aspect.Earth:
                        return "206";
                    case Aspect.Eldritch:
                        return "207";
                    case Aspect.Fire:
                        return "208";
                    case Aspect.Fortune:
                        return "209";
                    case Aspect.Gadget:
                        return "210";
                    case Aspect.Harvest:
                        return "211";
                    case Aspect.Holy:
                        return "212";
                    case Aspect.Lyric:
                        return "213";
                    case Aspect.Poison:
                        return "214";
                    case Aspect.Shadow:
                        return "200";
                    case Aspect.Void:
                        return "201";
                    case Aspect.War:
                        return "202";
                    case Aspect.Water:
                        return "203";

                    default:
                        return "0";
                }
            }
        }
    }
}
