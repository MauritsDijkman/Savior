using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Ground_Level1 : Sprite
    {
        public Ground_Level1() : base("ground_level1.png")
        {
            SetXY(0, 688);
        }
    }
}
