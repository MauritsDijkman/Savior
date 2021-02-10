using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    class Healthbar : GameObject
    {
        Player _owner;

        public Healthbar(Player owner)
        {
            _owner = owner;
        }
    }
}
