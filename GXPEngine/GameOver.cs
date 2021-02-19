using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    class GameOver : GameObject
    {
        Sprite gameover;

        Button_Back _main_menu_button;
        Button_Restart _restart_button;

        Sprite back_main_button_normal;
        Sprite back_main_button_hover;

        Sprite restart_button_normal;
        Sprite restart_button_hover;

        bool restarthoversoundHasPlayed = false;
        bool mainmenuhoversoundHasPlayed = false;

        Sound _hover;

        public GameOver() : base()
        {
            _hover = new Sound("hover.wav", false, false);

            gameover = new Sprite("GameOver.png");
            AddChild(gameover);

            _main_menu_button = new Button_Back();
            AddChild(_main_menu_button);

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
                if (_restart_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    RestartLevel();
                    Globals.aIsPressed = false;
                    Globals.dIsPressed = false;
                }

                if (_main_menu_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    GoBackToStartMenu();
                    Globals.aIsPressed = false;
                    Globals.dIsPressed = false;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleHoverButton()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleHoverButton()
        {
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

            if (_main_menu_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                back_main_button_normal.visible = false;
                back_main_button_hover.visible = true;
            }
            else
            {
                back_main_button_normal.visible = true;
                back_main_button_hover.visible = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleHoverSound()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleHoverSound()
        {
            if (_restart_button.HitTestPoint(Input.mouseX, Input.mouseY) && restarthoversoundHasPlayed == false)
            {
                _hover.Play();
                restarthoversoundHasPlayed = true;
            }
            else if (!_restart_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                restarthoversoundHasPlayed = false;
            }

            if (_main_menu_button.HitTestPoint(Input.mouseX, Input.mouseY) && mainmenuhoversoundHasPlayed == false)
            {
                _hover.Play();
                mainmenuhoversoundHasPlayed = true;
            }
            else if (!_main_menu_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                mainmenuhoversoundHasPlayed = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        GoBackToStartMenu()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void GoBackToStartMenu()
        {
            DestroyGameOver();

            MyGame mygame = game as MyGame;
            mygame.ResetWholeGame();
            mygame.CreateMenu();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        RestartLevel()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void RestartLevel()
        {
            DestroyGameOver();

            MyGame mygame = game as MyGame;
            mygame.ResetWholeGame();
            mygame.CreateLevel1();
            mygame.startMusic();

            Globals.bossIsDead = false;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        DestroyGameOver()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void DestroyGameOver()
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
            HandleHoverSound();
        }
    }
}
