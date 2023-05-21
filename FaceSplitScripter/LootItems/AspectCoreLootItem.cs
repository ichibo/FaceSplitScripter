namespace FaceSplitScripter
{
    public class AspectCoreLootItem : ILootItem
    {
        public LootType LootType { get { return LootType.Core; } }
        public Aspect Aspect { get; private set; }
        public int TomePage { get { return EnumUtilities.GetAspectTomePage(Aspect); } }
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
                        return "100";
                    case Aspect.Artisan:
                        return "101";
                    case Aspect.Blood:
                        return "102";
                    case Aspect.Command:
                        return "103";
                    case Aspect.Death:
                        return "104";
                    case Aspect.Discipline:
                        return "105";
                    case Aspect.Earth:
                        return "106";
                    case Aspect.Eldritch:
                        return "107";
                    case Aspect.Fire:
                        return "108";
                    case Aspect.Fortune:
                        return "109";
                    case Aspect.Gadget:
                        return "110";
                    case Aspect.Harvest:
                        return "111";
                    case Aspect.Holy:
                        return "112";
                    case Aspect.Lyric:
                        return "113";
                    case Aspect.Poison:
                        return "114";
                    case Aspect.Shadow:
                        return "100";
                    case Aspect.Void:
                        return "101";
                    case Aspect.War:
                        return "102";
                    case Aspect.Water:
                        return "103";

                    default:
                        return "0";
                }
            }
        }

    }
}
