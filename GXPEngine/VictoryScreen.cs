using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class VictoryScreen : GameObject
    {
        Sprite victoryscreen;

        Button_Start _restart_button;
        Button_Credits _credits_button;
        Button_Quit _main_menu_button;

        Sprite restart_button_normal;
        Sprite restart_button_hover;

        Sprite credits_button_normal;
        Sprite credits_button_hover;

        Sprite main_menu_button_normal;
        Sprite main_menu_button_hover;

        bool restarthoversoundHasPlayed = false;
        bool creditshoversoundHasPlayed = false;
        bool mainmenuhoversoundHasPlayed = false;

        Sound _hover;

        public VictoryScreen() : base()
        {
            _hover = new Sound("hover.wav", false, false);

            victoryscreen = new Sprite("VictoryScreen.png");
            AddChild(victoryscreen);

            _restart_button = new Button_Start();
            AddChild(_restart_button);

            _credits_button = new Button_Credits();
            AddChild(_credits_button);

            _main_menu_button = new Button_Quit();
            AddChild(_main_menu_button);

            restart_button_normal = new Sprite("restart2_button_normal.png");
            AddChild(restart_button_normal);

            restart_button_hover = new Sprite("restart2_button_hover.png");
            AddChild(restart_button_hover);

            credits_button_normal = new Sprite("credits_button_normal.png");
            AddChild(credits_button_normal);

            credits_button_hover = new Sprite("credits_button_hover.png");
            AddChild(credits_button_hover);

            main_menu_button_normal = new Sprite("main_menu_button_normal.png");
            AddChild(main_menu_button_normal);

            main_menu_button_hover = new Sprite("main_menu_button_hover.png");
            AddChild(main_menu_button_hover);

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
                    RestartGame();
                }

                if (_credits_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    CreateCreditsMenu();
                }

                if (_main_menu_button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    GoBackToStartMenu();
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

            if (_main_menu_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                main_menu_button_normal.visible = false;
                main_menu_button_hover.visible = true;
            }
            else
            {
                main_menu_button_normal.visible = true;
                main_menu_button_hover.visible = false;
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

            if (_credits_button.HitTestPoint(Input.mouseX, Input.mouseY) && creditshoversoundHasPlayed == false)
            {
                _hover.Play();
                creditshoversoundHasPlayed = true;
            }
            else if (!_credits_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                creditshoversoundHasPlayed = false;
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
        //                                                                                                                        CreateCreditsMenu()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void CreateCreditsMenu()
        {
            DestroyVictoryScreen();

            MyGame mygame = game as MyGame;
            mygame.ResetWholeGame();
            mygame.CreateCreditsMenu();
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
            HandleHoverSound();
        }
    }
}