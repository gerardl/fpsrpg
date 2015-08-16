using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPSRPGPrototype.Items
{
    public class WearableItem : BaseClasses.Item
    {
        // get reference to player somehow?

        public enum WearableTypes
        {
            Helmet = 1,
            Chest = 2,
            Legs = 3
            // probably a lot more
        }

        public WearableTypes wearableType;

        public Combat.DefenseConfig defenseBonus;


        public WearableItem()
        {
            itemType = ItemTypes.Wearable;
            defenseBonus = new Combat.DefenseConfig();
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
    }
}
