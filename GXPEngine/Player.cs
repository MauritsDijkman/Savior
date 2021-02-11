﻿using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public enum PlayerState
    {
        None,
        Walk,
        Jump,
        Attack
    }

    public class Player : AnimationSprite
    {
        PlayerState currentState = PlayerState.None;

        int animationDrawsBetweenFramesWalk;
        int stepWalk;

        int animationDrawsBetweenFramesAttack;
        int stepAttack;

        float speedX;
        float speedY;

        bool isLanded;

        int countFramesWalk;
        int countFramesAttack;

        Healthbar _healthbar;
        Hitbox_Player _hitbox_player;

        public Player() : base("player_tile.png", 8, 1)
        {
            Spawn();

            animationDrawsBetweenFramesWalk = 5;
            animationDrawsBetweenFramesAttack = 5;

            speedX = 5;
            speedY = 0;

            SetState(PlayerState.Walk);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        SetState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void SetState(PlayerState newState)
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
        void HandleStateTransition(PlayerState oldState, PlayerState newState)
        {
            if (newState == PlayerState.Jump)
            {
                speedY = -32;
                isLanded = false;
                SetFrame(7);
            }

            if (newState == PlayerState.Walk)
            {
                SetFrame(0);
            }

            if (newState == PlayerState.Attack)
            {
                SetFrame(4);
                countFramesAttack = 4;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleState()
        {
            if (currentState == PlayerState.Walk)
            {
                HandleWalkState();
            }
            if (currentState == PlayerState.Jump)
            {
                HandleJumpState();
            }
            if (currentState == PlayerState.Attack)
            {
                HandleAttackState();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleWalkState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void HandleWalkState()
        {
            HandleHorizontalControls();

            if (Input.GetKey(Key.D) || Input.GetKey(Key.A))
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

            if (Input.GetKey(Key.W))
            {
                SetState(PlayerState.Jump);
            }

            if (Input.GetKey(Key.SPACE))
            {
                SetState(PlayerState.Attack);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleJumpState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void HandleJumpState()
        {
            HandleHorizontalControls();

            if (isLanded)
            {
                SetState(PlayerState.Walk);
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
                countFramesAttack = countFramesAttack + 1;
            }

            if (countFramesAttack >= 7)
            {
                SetState(PlayerState.Walk);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Spawn()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Spawn()
        {
            SetFrame(0);

            SetXY(game.width / 2, game.height / 2);
            SetOrigin(width / 2, 0);

            _healthbar = new Healthbar();
            AddChild(_healthbar);

            _hitbox_player = new Hitbox_Player();
            AddChild(_hitbox_player);

        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleHorizontalControls()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleHorizontalControls()
        {
            if (Input.GetKey(Key.A))
            {
                speedX -= 1f;
            }
            if (Input.GetKey(Key.D))
            {
                speedX += 1f;
            }

            if (Input.GetKeyDown(Key.D))
            {
                Mirror(false, false);
                Globals.dIsPressed = true;
            }

            if (Input.GetKeyDown(Key.A))
            {
                Mirror(true, false);
                Globals.aIsPressed = true;
            }

            if (Input.GetKeyUp(Key.D))
            {
                Globals.dIsPressed = false;
            }

            if (Input.GetKeyUp(Key.A))
            {
                Globals.aIsPressed = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleHorizontalMovement()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleHorizontalMovement()
        {
            MoveWithCollision(speedX, 0f);
            speedX *= 0.6f;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleVerticalMovement()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleVerticalMovement()
        {
            speedY = speedY + 2f;
            if (MoveWithCollision(0f, speedY) == false)
            {
                speedY = 0f;
                isLanded = true;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleBorders()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleBorders()
        {
            x = Mathf.Clamp(x, (0 + (width / 4 + 100)), (1440 - (width / 4 + 100)));
            y = Mathf.Clamp(y, (0), (1080 - height - 260));

            if (y >= (1080 - height - 260))
            {
                isLanded = true;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        OnCollision()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                Globals.health = Globals.health - 1;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        MoveWithCollision()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        bool MoveWithCollision(float moveX, float moveY)
        {
            float previousX = x;
            float previousY = y;

            x += moveX;
            y += moveY;

            bool hasCollided = false;

            foreach (GameObject other in GetCollisions())
            {
                if (other is Block_Jump)
                {
                    hasCollided = true;
                }
            }
            if (hasCollided == true)
            {
                x = previousX;
                y = previousY;
            }
            return (hasCollided == false);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            HandleState();

            HandleHorizontalMovement();
            HandleVerticalMovement();

            HandleBorders();

            Globals.playerX = x;
        }
    }
}
