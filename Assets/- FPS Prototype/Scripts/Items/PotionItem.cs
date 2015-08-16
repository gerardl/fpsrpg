using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPSRPGPrototype.Items
{
    [Serializable]
    public class PotionItem : BaseClasses.Item
    {
        public enum PotionTypes
        {
            Health = 1,
            Mana = 2
        }


        public PotionTypes potionType;
        public int healthRestored;
        public int manaRestored;

        public PotionItem()
        {
            itemType = ItemTypes.Potion;
        }

        public override void Activate()
        {
            // get instance of player and heal him based on type
            // and xRestored

            base.Activate();
        }
    }
}
