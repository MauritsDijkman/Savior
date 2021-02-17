﻿using System;
using GXPEngine;

namespace GXPEngine
{
    class Grenade : AnimationSprite
    {
        float grenadeX;
        float grenadeY;

        float maximalY;

        float step;
        float animationDrawsBetweenFrames;
        float countFrames;


        public Grenade(float grenadeX, float grenadeY, float maximalY) : base("grenade_tile.png", 10, 1)
        {
            Spawn();

            x = grenadeX;
            y = grenadeY;

            this.maximalY = maximalY;

            animationDrawsBetweenFrames = 5;

            Globals.GrenadeDoesDamage = false;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Spawn()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Spawn()
        {
            SetOrigin(width / 2, height / 2);
            SetXY(grenadeX, grenadeY);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        MoveGrenades()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void MoveGrenades()
        {
            y = y + 15;

            if (y >= maximalY)
            {
                y = maximalY;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleExplosion()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleExplosion()
        {
            if (y == maximalY)
            {
                step = step + 1;

                if (step > animationDrawsBetweenFrames)
                {
                    NextFrame();
                    step = 0;
                    countFrames = countFrames + 1;
                }

                if (countFrames >= 7)
                {
                    Globals.GrenadeDoesDamage = true;
                }

                if (countFrames >= 10)
                {
                    Globals.GrenadeDoesDamage = false;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleRemove()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleRemove()
        {
            if (countFrames >= 10)
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
            MoveGrenades();

            HandleExplosion();

            HandleRemove();
        }
    }
}
