using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPSRPGPrototype.Interfaces
{
    public enum Actions
    {
        Use,
        Read,
        Take
    }

    // For all objects that can be used by pressing E
    public interface IInteractive
    {
        string Name { get; }
        bool IsActive { get; }
        Actions Action { get; }

        void Interact(Player.PlayerController player);
    }
}
