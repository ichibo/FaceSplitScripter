using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceSplitScripter.LootItems
{
    internal class ManualLootItem : ILootItem
    {
        public LootType LootType { get { return LootType.Manual; } }
        public string GumpResponseButtonForTome { get { return "0"; } }
        public int TomePage { get { return 0; } }
        public string Description { get; private set; }

        public ManualLootItem(string description)
        {
            Description = description;
        }
    }
}
