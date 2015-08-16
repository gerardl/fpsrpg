using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPSRPGPrototype.Combat
{
    [Serializable]
    public class DamageConfig
    {
        public int minPhysical = 0;
        public int maxPhysical = 1;
        public int fire = 0;
        public int ice = 0;
        public int electric = 0;

        public DamageConfig Copy()
        {
            DamageConfig newConfig = new DamageConfig();
            newConfig.minPhysical = this.minPhysical;
            newConfig.maxPhysical = this.maxPhysical;
            newConfig.fire = this.fire;
            newConfig.ice = this.ice;
            newConfig.electric = this.electric;
            return newConfig;
        }
    }

}
