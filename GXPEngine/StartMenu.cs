using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class StartMenu : GameObject
    {
        Sprite startmenu;

        Button_Start _start_button;
        Button_Credits _credits_button;
        Button_Quit _quit_button;

        Sprite start_button_normal;
        Sprite start_button_hover;

        Sprite credits_button_normal;
        Sprite credits_button_hover;

        Sprite quit_button_normal;
        Sprite quit_button_hover;

        public StartMenu() : base()
        {
            startmenu = new Sprite("StartMenu.png");
            AddChild(startmenu);

            _start_button = new Button_Start();
            AddChild(_start_button);

            _credits_button = new Button_Credits();
            AddChild(_credits_button);

            _quit_button = new Button_Quit();
            AddChild(_quit_button);

            start_button_normal = new Sprite("start_button_normal.png");
            AddChild(start_button_normal);

            start_button_hover = new Sprite("start_button_hover.png");
            AddChild(start_button_hover);

            credits_button_normal = new Sprite("credits_button_normal.png");
            AddChild(credits_button_normal);

            credits_button_hover = new Sprite("credits_button_hover.png");
            AddChild(credits_button_hover);

            quit_button_normal = new Sprite("quit_button_normal.png");
            AddChild(quit_button_normal);

            quit_button_hover = new Sprite("quit_button_hover.png");
            AddChild(quit_button_hover);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleClickButton()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleClickButton()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_start_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    CreateGame();
                }

                if (_credits_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    CreateCredits();
                }

                if (_quit_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    MyGame mygame = game as MyGame;
                    mygame.quitIsPressed = true;

                    MyGame.timeSince = Time.now;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleHoverButton()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleHoverButton()
        {
            if (_start_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                start_button_normal.visible = false;
                start_button_hover.visible = true;
            }
            else
            {
                start_button_normal.visible = true;
                start_button_hover.visible = false;
            }

            if (_credits_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                credits_button_normal.visible = false;
                credits_button_hover.visible = true;
            }
            else
            {
                credits_button_normal.visible = true;
                credits_button_hover.visible = false;
            }

            if (_quit_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                quit_button_normal.visible = false;
                quit_button_hover.visible = true;
            }
            else
            {
                quit_button_normal.visible = true;
                quit_button_hover.visible = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            HandleClickButton();
            HandleHoverButton();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        CreateGame()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void CreateGame()
        {
            DestroyStartMenu();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        CreateCredits()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void CreateCredits()
        {
            DestroyStartMenu();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        DestroyStartMenu()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void DestroyStartMenu()
        {
            LateDestroy();
            LateRemove();
        }
    }
}
