using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Level1_Background : Sprite
    {
        public Level1_Background() : base("background_level1_tile.png")
        {
            SetOrigin(width / 2, height / 2);
            SetXY(game.width / 2, game.height / 2);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            if (Globals.aIsPressed == true)
            {
                x = x + 6;
            }

            if (Globals.dIsPressed == true)
            {
                x = x + -6;
            }

            if (x <= -7200)
            {
                x = -7200;
            }

            if (x >= width)
            {
                x = width;
            }
        }
    }
}
