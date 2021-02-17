using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class VictoryScreen : GameObject
    {
        Sprite victoryscreen;

        Button_Back _back_button;
        Button_Restart _restart_button;

        Sprite back_main_button_normal;
        Sprite back_main_button_hover;

        Sprite restart_button_normal;
        Sprite restart_button_hover;

        public VictoryScreen() : base()
        {
            victoryscreen = new Sprite("VictoryScreen.png");
            AddChild(victoryscreen);

            _back_button = new Button_Back();
            AddChild(_back_button);

            _restart_button = new Button_Restart();
            AddChild(_restart_button);

            back_main_button_normal = new Sprite("back_main_button_normal.png");
            AddChild(back_main_button_normal);

            back_main_button_hover = new Sprite("back_main_button_hover.png");
            AddChild(back_main_button_hover);

            restart_button_normal = new Sprite("restart_button_normal.png");
            AddChild(restart_button_normal);

            restart_button_hover = new Sprite("restart_button_hover.png");
            AddChild(restart_button_hover);

            Globals.showMouseCursor = true;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleClickButton()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleClickButton()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_back_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    GoBackToStartMenu();
                }

                if (_restart_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    RestartGame();
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleHoverButton()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleHoverButton()
        {
            if (_back_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                back_main_button_normal.visible = false;
                back_main_button_hover.visible = true;
            }
            else
            {
                back_main_button_normal.visible = true;
                back_main_button_hover.visible = false;
            }

            if (_restart_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                restart_button_normal.visible = false;
                restart_button_hover.visible = true;
            }
            else
            {
                restart_button_normal.visible = true;
                restart_button_hover.visible = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        GoBackToStartMenu()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void GoBackToStartMenu()
        {
            DestroyVictoryScreen();

            MyGame mygame = game as MyGame;
            mygame.ResetWholeGame();
            mygame.CreateMenu();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        RestartGame()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void RestartGame()
        {
            DestroyVictoryScreen();

            MyGame mygame = game as MyGame;
            mygame.ResetWholeGame();
            mygame.CreateLevel1();
            mygame.startMusic();

            Globals.bossIsDead = false;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        DestroyVictoryScreen()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void DestroyVictoryScreen()
        {
            LateDestroy();
            LateRemove();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            HandleClickButton();
            HandleHoverButton();
        }
    }
}