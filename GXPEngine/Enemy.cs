﻿using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public enum EnemyState
    {
        None,
        Walk,
        Attack
    }
    public class Enemy : AnimationSprite
    {
        EnemyState currentState = EnemyState.None;

        int animationDrawsBetweenFramesWalk;
        int stepWalk;

        int animationDrawsBetweenFramesAttack;
        int stepAttack;

        int speed;

        bool goToLeft;
        bool goToRight;

        float countFramesWalk;
        float countFramesAttack;

        Hitbox_Enemy _hitbox_enemy;

        Sound _deadSound;

        float enemyX;
        float enemyY;

        float minimalX;
        float maximalX;

        public Enemy(float enemyX, float enemyY, float minimalX, float maximalX) : base("enemy_tile.png", 4, 2)
        {
            _deadSound = new Sound("Dead_sound_enemy.wav", false, false);

            Spawn();

            x = enemyX;
            y = enemyY;

            this.minimalX = minimalX;
            this.maximalX = maximalX;

            animationDrawsBetweenFramesAttack = 10;
            speed = 5;

            SetState(EnemyState.Walk);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        SetState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void SetState(EnemyState newState)
        {
            if (currentState != newState)
            {
                HandleStateTransition(currentState, newState);
                currentState = newState;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleStateTransition()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleStateTransition(EnemyState oldState, EnemyState newState)
        {
            if (newState == EnemyState.Walk)
            {
                SetFrame(0);
            }

            if (newState == EnemyState.Attack)
            {
                SetFrame(4);
                Globals.countFramesAttackEnemy = 4;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleState()
        {
            if (currentState == EnemyState.Walk)
            {
                HandleWalkState();
            }
            if (currentState == EnemyState.Attack)
            {
                HandleAttackState();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleWalkState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void HandleWalkState()
        {
            stepWalk = stepWalk + 1;

            if (stepWalk > animationDrawsBetweenFramesWalk)
            {
                NextFrame();
                stepWalk = 0;
                countFramesWalk = countFramesWalk + 1;
            }

            if (countFramesWalk >= 4)
            {
                SetFrame(0);
                countFramesWalk = 0;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttackState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void HandleAttackState()
        {
            stepAttack = stepAttack + 1;

            if (stepAttack > animationDrawsBetweenFramesAttack)
            {
                NextFrame();
                stepAttack = 0;
                Globals.countFramesAttackEnemy = Globals.countFramesAttackEnemy + 1;
                Globals.enemyIsAttacking = true;
            }

            if (Globals.countFramesAttackEnemy >= 7)
            {
                Globals.enemyIsAttacking = false;
                SetState(EnemyState.Walk);

            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Spawn()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Spawn()
        {
            SetFrame(1);

            SetXY(enemyX, enemyY);
            SetOrigin(width / 2, height);

            goToRight = true;

            _hitbox_enemy = new Hitbox_Enemy();
            AddChild(_hitbox_enemy);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAnimation()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //void HandleAnimation()
        //{
        //    step = step + 1;

        //    if (step > animationDrawsBetweenFrames)
        //    {
        //        NextFrame();
        //        step = 0;

        //        countFrames = countFrames + 1;
        //    }

        //    if (countFrames >= 3)
        //    {
        //        SetFrame(0);
        //        countFrames = 0;
        //    }
        //}

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleMovement()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleMovement()
        {
            if (x >= maximalX)
            {
                goToRight = false;
                goToLeft = true;
            }

            if (x <= minimalX)
            {
                goToLeft = false;
                goToRight = true;
            }

            if (goToLeft == true)
            {
                x = x - speed;
                Mirror(false, false);
            }

            if (goToRight == true)
            {
                x = x + speed;
                Mirror(true, false);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Dead()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Dead()
        {
            if (Globals.EnemyIsDead == true)
            {
                _deadSound.Play();

                LateDestroy();
                LateRemove();

                Globals.EnemyIsDead = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        OnCollision()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                SetState(EnemyState.Attack);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            Dead();
            //HandleAnimation();
            HandleMovement();

        }
    }
}
