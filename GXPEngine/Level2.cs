using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Level2 : GameObject
    {
        Player player;
        Level2_Background background;

        Block_Jump _block_jump;
        Block_Jump2 _block_jump2;

        Enemy enemy;

        public Level2() : base()
        {
            StartLevel();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        StartLevel()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void StartLevel()
        {
            background = new Level2_Background();
            AddChild(background);

            player = new Player();
            AddChild(player);

            _block_jump2 = new Block_Jump2(5000, 709);
            AddChild(_block_jump2);

            _block_jump = new Block_Jump(5500, 550);
            AddChild(_block_jump);

            enemy = new Enemy(6000, 830, 5150, 6850);
            AddChild(enemy);

            _block_jump = new Block_Jump(6500, 550);
            AddChild(_block_jump);

            enemy = new Enemy(7000, 585, 6900, 7100);
            AddChild(enemy);

            _block_jump2 = new Block_Jump2(7000, 709);
            AddChild(_block_jump2);

            enemy = new Enemy(9000, 830, 8150, 9500);
            AddChild(enemy);

            enemy = new Enemy(9200, 830, 8500, 9500);
            AddChild(enemy);

            enemy = new Enemy(9400, 830, 8500, 9500);
            AddChild(enemy);

            _block_jump = new Block_Jump(10000, 550);
            AddChild(_block_jump);

            enemy = new Enemy(10000, 830, 10150, 11850);
            AddChild(enemy);

            enemy = new Enemy(11000, 830, 10150, 11850);
            AddChild(enemy);

            _block_jump = new Block_Jump(11000, 550);
            AddChild(_block_jump);

            _block_jump2 = new Block_Jump2(12000, 709);
            AddChild(_block_jump2);

            enemy = new Enemy(12250, 830, 12150, 12850);
            AddChild(enemy);

            enemy = new Enemy(12750, 830, 12150, 12850);
            AddChild(enemy);

            _block_jump2 = new Block_Jump2(13000, 709);
            AddChild(_block_jump2);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            if (player.x + x > 800) x = 800 - player.x;
            if (player.x + x < 500) x = 500 - player.x;
        }
    }
}
