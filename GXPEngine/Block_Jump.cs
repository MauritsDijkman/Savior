﻿using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Block_Jump : Sprite
    {
        public Block_Jump() : base("block_jump.png")
        {
            SetXY(0, 0);
            SetOrigin(width / 2, height / 2);
        }
    }
}
