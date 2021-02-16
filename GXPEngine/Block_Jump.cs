using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Block_Jump : Sprite
    {
        float blockX;
        float blockY;

        public Block_Jump(float blockX, float blockY) : base("block_jump.png")
        {
            x = blockX;
            y = blockY;

            SetXY(blockX, blockY);
            SetOrigin(width / 2, height / 2);
        }
    }
}
