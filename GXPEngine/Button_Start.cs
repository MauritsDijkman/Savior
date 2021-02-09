using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Button_Start : Sprite
    {
        public Button_Start() : base("start_button_empty.png")
        {
            SetXY(470, 585);
        }
    }
}
