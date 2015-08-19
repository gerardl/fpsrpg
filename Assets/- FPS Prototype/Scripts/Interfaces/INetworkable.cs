using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPSRPGPrototype.Interfaces
{
    public enum ConnectionStatuses
    {
       Offline = 1,
       Connected = 2,
       Ready = 3
    }

    // I'm not using this right now, I will implement
    // more robust network code once we get further along, and once
    // unet is out of beta.
    public interface INetworkable
    {
        //ConnectionStatuses connectionStatus { get; set; }

        void NetworkInitialize();
    }
}
