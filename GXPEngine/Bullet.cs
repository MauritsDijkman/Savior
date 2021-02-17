﻿using System;
using GXPEngine;

namespace GXPEngine
{
    class Bullet : AnimationSprite
    {
        float bulletX;
        float bulletY;

        float GoToX;
        float GoToY;

        public Bullet(float bulletX, float bulletY, float GoToX, float GoToY) : base("bullet_tile.png", 1, 1)
        {
            Spawn();

            x = bulletX;
            y = bulletY;

            this.GoToX = GoToX;
            this.GoToY = GoToY;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Spawn()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Spawn()
        {
            SetOrigin(width / 2, height / 2);
            SetXY(bulletX, bulletY);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        MoveBullets()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void MoveBullets()
        {

        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleRemove()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleRemove()
        {
            if (x == GoToX && y == GoToY)
            {
                LateDestroy();
                LateRemove();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            MoveBullets();

            HandleRemove();
        }
    }
}
