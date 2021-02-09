using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Button_Quit : Sprite
    {
        public Button_Quit() : base("quit_button_empty.png")
        {
            SetXY(470, 873);
        }
    }
}
