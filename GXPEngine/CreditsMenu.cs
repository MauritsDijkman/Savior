using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    class CreditsMenu : GameObject
    {
        Sprite creditsmenu;

        public CreditsMenu() : base()
        {
            creditsmenu = new Sprite("CreditsMenu.png");
            AddChild(creditsmenu);
        }
    }
}
