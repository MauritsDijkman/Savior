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

        Sprite controls_button_normal;
        Sprite controls_button_hover;

        Sprite quit_button_normal;
        Sprite quit_button_hover;

        Sound _music;
        SoundChannel _musicChannel;

        bool starthoversoundHasPlayed = false;
        bool creditshoversoundHasPlayed = false;
        bool quithoversoundHasPlayed = false;

        Sound _hover;

        public StartMenu() : base()
        {
            _hover = new Sound("hover.wav", false, false);

            startMusic();

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

            controls_button_normal = new Sprite("controls_button_normal.png");
            AddChild(controls_button_normal);

            controls_button_hover = new Sprite("controls_button_hover.png");
            AddChild(controls_button_hover);

            quit_button_normal = new Sprite("exit_button_normal.png");
            AddChild(quit_button_normal);

            quit_button_hover = new Sprite("exit_button_hover.png");
            AddChild(quit_button_hover);

            Globals.showMouseCursor = true;
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
                controls_button_normal.visible = false;
                controls_button_hover.visible = true;
            }
            else
            {
                controls_button_normal.visible = true;
                controls_button_hover.visible = false;
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
        //                                                                                                                        HandleHoverSound()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleHoverSound()
        {
            if (_start_button.HitTestPoint(Input.mouseX, Input.mouseY) && starthoversoundHasPlayed == false)
            {
                _hover.Play();
                starthoversoundHasPlayed = true;
            }
            else if (!_start_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                starthoversoundHasPlayed = false;
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

            if (_quit_button.HitTestPoint(Input.mouseX, Input.mouseY) && quithoversoundHasPlayed == false)
            {
                _hover.Play();
                quithoversoundHasPlayed = true;
            }
            else if (!_quit_button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                quithoversoundHasPlayed = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        CreateGame()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void CreateGame()
        {
            DestroyStartMenu();

            MyGame mygame = game as MyGame;
            mygame.CreateLevel1();
            mygame.startMusic();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        CreateCredits()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void CreateCredits()
        {
            DestroyStartMenu();

            MyGame mygame = game as MyGame;
            mygame.CreateControlsMenu();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        DestroyStartMenu()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void DestroyStartMenu()
        {
            stopMusic();

            LateDestroy();
            LateRemove();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        startMusic()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void startMusic()
        {
            _music = new Sound("background_music_main_menu.mp3", true, true);
            _musicChannel = _music.Play();
            _musicChannel.Volume = 0.2f;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        stopMusic()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void stopMusic()
        {
            _musicChannel.Stop();
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
