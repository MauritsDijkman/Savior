using GXPEngine;                                // GXPEngine contains the engine

static class Globals
{
    public static int health_player;
    public static int health_boss;
    public static float playerX;
    public static float countFramesAttackPlayer;
    public static float countFramesAttackEnemy;
    public static float FPS_Game;
    public static bool aIsPressed;
    public static bool dIsPressed;
    public static bool EnemyIsDead;
    public static bool playerIsAttacking;
    public static bool enemyIsAttacking;
    public static bool bossIsAttacking;
    public static bool playerIsDead;
    public static bool bossIsDead;
    public static bool MCfacingRight;
    public static bool MCfacingLeft;
    public static bool EnemyGoToRight;
    public static bool EnemyGoToLeft;
    public static bool showMouseCursor;
    public static bool bossDeathSoundHasPlayed;
    public static bool levelBossIsActive;
    public static bool GrenadeDoesDamage;
}

public class MyGame : Game
{
    private StartMenu _menu;
    private CreditsMenu _creditsmenu;
    private GameOver _gameover;
    private VictoryScreen _victoryscreen;
    private Level1 _level1;
    private Level2 _level2;
    private Level_Boss _levelBoss;
    private HUD _HUD_player;

    Sound _quitSound;

    Sound _backgroundmusic;
    SoundChannel _musicchannel;

    public static float timeSince;

    public bool quitIsPressed;

    public bool level1IsActive;
    public bool level2IsActive;

    bool quitsoundHasPlayed;
    bool gameIsPaused;
    bool unpauseIsPressed;

    Button_Credits _buttonunpause;
    Button_Back _button_backmain;

    Sprite pause_menu;

    Sprite unpause_button_normal;
    Sprite unpause_button_hover;

    Sprite back_main_button_normal;
    Sprite back_main_button_hover;

    public MyGame() : base(1440, 1080, false, false)    // Create a window that's 1440x1080 and NOT fullscreen and V-Sync turned OFF
    {
        targetFps = 60;

        _quitSound = new Sound("QuitSound.mp3", false, true);

        quitIsPressed = false;
        quitsoundHasPlayed = false;

        level1IsActive = false;
        level2IsActive = false;
        Globals.levelBossIsActive = false;

        gameIsPaused = false;

        CreateMenu();
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreateMenu()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreateMenu()
    {
        _menu = new StartMenu();
        AddChild(_menu);
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreatePauseMenu()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreatePauseMenu()
    {
        pause_menu = new Sprite("PauseMenu.png");
        AddChild(pause_menu);
        pause_menu.visible = false;

        _buttonunpause = new Button_Credits();
        AddChild(_buttonunpause);

        _button_backmain = new Button_Back();
        AddChild(_button_backmain);

        unpause_button_normal = new Sprite("unpause_button_normal.png");
        AddChild(unpause_button_normal);
        unpause_button_normal.visible = false;

        unpause_button_hover = new Sprite("unpause_button_hover.png");
        AddChild(unpause_button_hover);
        unpause_button_hover.visible = false;

        back_main_button_normal = new Sprite("back_main_button_normal.png");
        AddChild(back_main_button_normal);
        back_main_button_normal.visible = false;

        back_main_button_hover = new Sprite("back_main_button_hover.png");
        AddChild(back_main_button_hover);
        back_main_button_hover.visible = false;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreateCreditsMenu()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreateCreditsMenu()
    {
        _creditsmenu = new CreditsMenu();
        AddChild(_creditsmenu);
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreateHUD()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void CreateHUD()
    {
        _HUD_player = new HUD();
        AddChild(_HUD_player);
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreateLevel1()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreateLevel1()
    {
        _level1 = new Level1();
        AddChild(_level1);

        CreateHUD();
        CreatePauseMenu();

        Globals.playerIsDead = false;
        Globals.showMouseCursor = false;

        level1IsActive = true;
        level2IsActive = false;
        Globals.levelBossIsActive = false;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreateLevel2()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreateLevel2()
    {
        _level2 = new Level2();
        AddChild(_level2);

        CreateHUD();
        CreatePauseMenu();

        Globals.playerIsDead = false;
        Globals.showMouseCursor = false;

        level1IsActive = false;
        level2IsActive = true;
        Globals.levelBossIsActive = false;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreateBossLevel()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreateBossLevel()
    {
        _levelBoss = new Level_Boss();
        AddChild(_levelBoss);

        CreateHUD();
        CreatePauseMenu();

        Globals.playerIsDead = false;
        Globals.showMouseCursor = false;
        Globals.bossIsDead = false;
        Globals.bossDeathSoundHasPlayed = false;

        level1IsActive = false;
        level2IsActive = false;
        Globals.levelBossIsActive = true;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        GameOver()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void GameOver()
    {
        if (level1IsActive == true)
        {
            _level1.Destroy();
            _level1.Remove();
        }

        if (level2IsActive == true)
        {
            _level2.Destroy();
            _level2.Remove();
        }

        if (Globals.levelBossIsActive == true)
        {
            _levelBoss.Destroy();
            _levelBoss.Remove();
        }

        stopMusic();

        _gameover = new GameOver();
        AddChild(_gameover);
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreateVictoryScreen()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreateVictoryScreen()
    {
        if (level1IsActive == true)
        {
            _level1.Destroy();
            _level1.Remove();
        }

        if (level2IsActive == true)
        {
            _level2.Destroy();
            _level2.Remove();
        }

        if (Globals.levelBossIsActive == true)
        {
            _levelBoss.Destroy();
            _levelBoss.Remove();
        }

        stopMusic();

        _victoryscreen = new VictoryScreen();
        AddChild(_victoryscreen);
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        HandleQuitButton()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void HandleQuitButton()
    {
        if (quitIsPressed == true)
        {
            if (quitsoundHasPlayed == false)
            {
                _quitSound.Play();
                quitsoundHasPlayed = true;
            }

            if (Time.now >= timeSince + 950f)
            {
                Destroy();
                quitIsPressed = false;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        HandlePause()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void HandlePause()
    {
        if (Input.GetKeyDown(Key.P) && gameIsPaused == false)
        {
            Globals.showMouseCursor = true;

            if (level1IsActive == true)
            {
                _level1.Pause();
                pauseMusic();
                gameIsPaused = true;
                pause_menu.visible = true;
            }

            if (level2IsActive == true)
            {
                _level2.Pause();
                pauseMusic();
                gameIsPaused = true;
                pause_menu.visible = true;
            }

            if (Globals.levelBossIsActive == true)
            {
                _levelBoss.Pause();
                pauseMusic();
                gameIsPaused = true;
                pause_menu.visible = true;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        HandleUnpause()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void HandleUnpause()
    {
        if (unpauseIsPressed == true)
        {
            if (level1IsActive == true)
            {
                _level1.Unpause();
            }

            if (level2IsActive == true)
            {
                _level2.Unpause();
            }

            if (Globals.levelBossIsActive == true)
            {
                _levelBoss.Unpause();
            }

            gameIsPaused = false;

            pause_menu.visible = false;

            unpause_button_normal.visible = false;
            unpause_button_hover.visible = false;

            back_main_button_normal.visible = false;
            back_main_button_hover.visible = false;

            unpauseIsPressed = false;
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        HandlePauseButton()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void HandlePauseButton()
    {
        if (gameIsPaused == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_buttonunpause.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    unpauseMusic();
                    unpauseIsPressed = true;
                    Globals.showMouseCursor = false;
                }

                if (_button_backmain.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    unpauseIsPressed = true;
                    stopMusic();
                    Globals.showMouseCursor = true;

                    if (level1IsActive == true)
                    {
                        _level1.Destroy();
                        _level1.Remove();

                        level1IsActive = false;
                    }

                    if (level2IsActive == true)
                    {
                        _level2.Destroy();
                        _level2.Remove();

                        level2IsActive = false;
                    }

                    if (Globals.levelBossIsActive == true)
                    {
                        _levelBoss.Destroy();
                        _levelBoss.Remove();

                        Globals.levelBossIsActive = false;
                    }

                    CreateMenu();
                }
            }

            if (_buttonunpause.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                unpause_button_normal.visible = false;
                unpause_button_hover.visible = true;
            }
            else
            {
                unpause_button_normal.visible = true;
                unpause_button_hover.visible = false;
            }

            if (_button_backmain.HitTestPoint(Input.mouseX, Input.mouseY))
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
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        HandleMouseCursor()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void HandleMouseCursor()
    {
        if (Globals.showMouseCursor == true)
        {
            ShowMouse(true);
        }

        if (Globals.showMouseCursor == false)
        {
            ShowMouse(false);
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        startMusic()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void startMusic()
    {
        _backgroundmusic = new Sound("Background_music_level1.mp3", true, true);
        _musicchannel = _backgroundmusic.Play();
        _musicchannel.Volume = 0.2f;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        stopMusic()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void stopMusic()
    {
        _musicchannel.Stop();
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        pauseMusic()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void pauseMusic()
    {
        _musicchannel.IsPaused = true;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        unpauseMusic()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void unpauseMusic()
    {
        _musicchannel.IsPaused = false;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        Update()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        HandleQuitButton();

        HandlePause();
        HandleUnpause();

        HandlePauseButton();

        HandleMouseCursor();

        Globals.FPS_Game = targetFps;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        Main()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private static void Main()                  // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}
