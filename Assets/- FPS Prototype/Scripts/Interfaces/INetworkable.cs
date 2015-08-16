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

    // For all objects that can be used by pressing E
    public interface INetworkable
    {
        //ConnectionStatuses connectionStatus { get; set; }

        void NetworkInitialize();
    }
}
