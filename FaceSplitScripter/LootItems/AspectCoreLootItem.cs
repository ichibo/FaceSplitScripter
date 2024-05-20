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
                    case Aspect.Arcane:
                        return "101";
                    case Aspect.Artisan:
                        return "102";
                    case Aspect.Blood:
                        return "103";
                    case Aspect.Command:
                        return "104";
                    case Aspect.Death:
                        return "105";
                    case Aspect.Discipline:
                        return "106";
                    case Aspect.Earth:
                        return "107";
                    case Aspect.Eldritch:
                        return "108";
                    case Aspect.Fire:
                        return "109";
                    case Aspect.Fortune:
                        return "110";
                    case Aspect.Frost:
                        return "111";
                    case Aspect.Gadget:
                        return "112";
                    case Aspect.Harvest:
                        return "113";
                    case Aspect.Holy:
                        return "114";
                    case Aspect.Lightning:
                        return "100";
                    case Aspect.Lyric:
                        return "101";
                    case Aspect.Madness:
                        return "102";
                    case Aspect.Poison:
                        return "103";
                    case Aspect.Shadow:
                        return "104";
                    case Aspect.Void:
                        return "105";
                    case Aspect.War:
                        return "106";
                    case Aspect.Water:
                        return "107";

                    default:
                        return "0";
                }
            }
        }

    }
}
