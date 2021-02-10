using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Button_Restart : Sprite
    {
        public Button_Restart() : base("restart_button_empty.png")
        {
            SetXY(470, 729);
        }
    }
}
