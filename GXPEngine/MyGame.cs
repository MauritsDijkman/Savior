using GXPEngine;                                // GXPEngine contains the engine

static class Globals
{
    public static int health;
}

public class MyGame : Game
{
    private StartMenu _menu;
    private CreditsMenu _creditsmenu;
    private Level1 _level1;
    private GameOver _gameover;

    Sound _quitSound;

    public static float timeSince;

    public bool quitIsPressed;
    public bool level1IsActive;

    bool quitsoundHasPlayed;
    bool gameIsPaused;

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
        LateAddChild(pause_menu);
        pause_menu.visible = false;

        unpause_button_normal = new Sprite("unpause_button_normal.png");
        LateAddChild(unpause_button_normal);
        unpause_button_normal.visible = false;

        unpause_button_hover = new Sprite("unpause_button_hover.png");
        LateAddChild(unpause_button_hover);
        unpause_button_hover.visible = false;

        back_main_button_normal = new Sprite("back_main_button_normal.png");
        LateAddChild(back_main_button_normal);
        back_main_button_normal.visible = false;

        back_main_button_hover = new Sprite("back_main_button_hover.png");
        LateAddChild(back_main_button_hover);
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
    //                                                                                                                        CreateGame()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreateGame()
    {
        _level1 = new Level1();
        AddChild(_level1);
        level1IsActive = true;

        CreatePauseMenu();
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        GameOver()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void GameOver()
    {
        _level1.Destroy();
        _level1.Remove();

        _gameover = new GameOver();
        AddChild(_gameover);
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
            if (level1IsActive == true)
            {
                _level1.Pause();
                gameIsPaused = true;
                pause_menu.visible = true;
                unpause_button_normal.visible = true;
                back_main_button_normal.visible = true;
            }                        
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        HandleUnpause()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void HandleUnpause()
    {
        if (Input.GetKeyDown(Key.U) && gameIsPaused == true)
        {
            if (level1IsActive == true)
            {
                _level1.Unpause();
                gameIsPaused = false;
                pause_menu.visible = false;
                unpause_button_normal.visible = false;
                back_main_button_normal.visible = false;
            }
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
                if (unpause_button_normal.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    //stopMusic();
                    if (level1IsActive)
                    {
                        gameIsPaused = false;
                        //game.GetChildren().ForEach(ResetGame);
                    }
                                        
                    //CreateMenu();
                    //return;
                }
            }

            if (unpause_button_normal.HitTestPoint(Input.mouseX, Input.mouseY) && gameIsPaused == true)
            {
                unpause_button_hover.visible = true;
                unpause_button_normal.visible = false;
            }
            else
            {
                unpause_button_hover.visible = false;
                unpause_button_normal.visible = true;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        Update()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        HandleQuitButton();
        HandlePauseButton();
        HandlePause();
        HandleUnpause();        
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        Main()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private static void Main()                  // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}
