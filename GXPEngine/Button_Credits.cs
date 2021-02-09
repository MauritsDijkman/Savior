using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Button_Credits : Sprite
    {
        public Button_Credits() : base("credits_button_empty.png")
        {
            SetXY(470, 729); //dit is een wijziging
        }
    }
}
