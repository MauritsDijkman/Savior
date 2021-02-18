using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public enum BossState
    {
        None,
        Idle,
        AttackGrenade,
        AttackShotgun,
        AttackSpawnEnemies,
        Vulnerable
    }

    public class Enemy_Boss : AnimationSprite
    {
        BossState currentState = BossState.None;

        Grenade _grenade;
        Bullet _bullet;

        float stepIdle;
        float animationDrawsBetweenFramesIdle;
        float countFramesIdle;

        float stepAG;
        float animationDrawsBetweenFramesAG;
        float countFramesAG;

        float stepAS;
        float animationDrawsBetweenFramesAS;
        float countFramesAS;

        float stepSP;
        float animationDrawsBetweenFramesSP;
        float countFramesSP;

        float stepV;
        float animationDrawsBetweenFramesV;
        float countFramesV;

        float bossX;
        float bossY;

        float AllFrames;

        bool granadesHaveSpawned;
        bool shotgunHasAttacked;
        bool enemiesHasSpawned;
        bool bossIsVulnerable;

        bool shootGrenades;
        bool shootBullets;
        bool spawnEnemies;

        Hitbox_Boss _hitbox_boss;
        Enemy _enemy;

        public Enemy_Boss(float bossX, float bossY) : base("enemy_boss_tile.png", 7, 6)
        {
            Spawn();

            x = bossX;
            y = bossY;

            animationDrawsBetweenFramesIdle = 10;
            animationDrawsBetweenFramesAG = 10;
            animationDrawsBetweenFramesAS = 10;
            animationDrawsBetweenFramesSP = 10;
            animationDrawsBetweenFramesV = 8;

            AllFrames = 0;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        SetState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void SetState(BossState newState)
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
        void HandleStateTransition(BossState oldState, BossState newState)
        {
            if (newState == BossState.Idle)
            {
                SetFrame(0);
            }

            if (newState == BossState.AttackGrenade)
            {
                granadesHaveSpawned = false;
                SetFrame(7);
            }

            if (newState == BossState.AttackShotgun)
            {
                shotgunHasAttacked = false;
                SetFrame(14);
            }

            if (newState == BossState.AttackSpawnEnemies)
            {
                enemiesHasSpawned = false;
                SetFrame(21);
            }

            if (newState == BossState.Vulnerable)
            {
                bossIsVulnerable = true;
                SetFrame(28);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleState()
        {
            if (currentState == BossState.Idle)
            {
                HandleIdleState();
            }
            if (currentState == BossState.AttackGrenade)
            {
                HandleAttackGrenadeState();
            }
            if (currentState == BossState.AttackShotgun)
            {
                HandleAttackShotgunState();
            }
            if (currentState == BossState.AttackSpawnEnemies)
            {
                HandleAttackSpawnEnemieState();
            }
            if (currentState == BossState.Vulnerable)
            {
                HandleVulnerableState();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleIdleState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleIdleState()
        {
            stepIdle = stepIdle + 1;

            if (stepIdle > animationDrawsBetweenFramesIdle)
            {
                NextFrame();
                stepIdle = 0;
                countFramesIdle = countFramesIdle + 1;

                AllFrames = AllFrames + 1;
            }

            if (countFramesIdle == 7)
            {
                countFramesIdle = 0;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttackGrenadeState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAttackGrenadeState()
        {
            if (granadesHaveSpawned == false)
            //if (countFramesAG == 0 && currentState == BossState.AttackGrenade)
            {
                HandleGrenades();

                stepAG = stepAG + 1;

                if (stepAG > animationDrawsBetweenFramesAG)
                {
                    NextFrame();
                    stepAG = 0;
                    countFramesAG = countFramesAG + 1;

                    AllFrames = AllFrames + 1;
                }

                if (countFramesAG == 7)
                {
                    shootGrenades = true;
                    countFramesAG = 0;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttackShotgunState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAttackShotgunState()
        {
            if (shotgunHasAttacked == false)
            {
                HandleBullets();

                stepAS = stepAS + 1;

                if (stepAS > animationDrawsBetweenFramesAS)
                {
                    NextFrame();
                    stepAS = 0;
                    countFramesAS = countFramesAS + 1;

                    AllFrames = AllFrames + 1;
                }

                if (countFramesAS == 7)
                {
                    shootBullets = true;
                    countFramesAS = 0;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttackSpawnEnemieState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAttackSpawnEnemieState()
        {
            if (enemiesHasSpawned == false)
            {
                HandleSpawnEnemies();

                stepSP = stepSP + 1;

                if (stepSP > animationDrawsBetweenFramesSP)
                {
                    NextFrame();
                    stepSP = 0;
                    countFramesSP = countFramesSP + 1;

                    AllFrames = AllFrames + 1;
                }

                if (countFramesSP == 7)
                {
                    spawnEnemies = true;
                    countFramesSP = 0;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleVulnerableState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleVulnerableState()
        {
            if (bossIsVulnerable == true)
            {
                //_enemy.LateDestroy();
                //_enemy.LateRemove();

                stepV = stepV + 1;

                if (stepV > animationDrawsBetweenFramesV)
                {
                    NextFrame();
                    stepV = 0;
                    countFramesV = countFramesV + 1;

                    AllFrames = AllFrames + 1;
                }

                if (countFramesV == 14)
                {
                    bossIsVulnerable = false;
                    countFramesV = 0;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleGrenades()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleGrenades()
        {
            if (shootGrenades == true)
            //if (countFramesAG == 7 && currentState == BossState.AttackGrenade)
            {
                _grenade = new Grenade(-1075, -100, 490);
                AddChild(_grenade);

                _grenade = new Grenade(-800, -100, 790);
                AddChild(_grenade);

                _grenade = new Grenade(-1300, -100, 790);
                AddChild(_grenade);

                granadesHaveSpawned = true;
                shootGrenades = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleBullets()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleBullets()
        {
            if (shootBullets == true)
            {
                _bullet = new Bullet(-450, 650, -1460, 70);
                AddChild(_bullet);

                _bullet = new Bullet(-450, 650, -1460, 390);
                AddChild(_bullet);

                _bullet = new Bullet(-450, 650, -1460, 610);
                AddChild(_bullet);

                _bullet = new Bullet(-450, 650, -1460, 900);
                AddChild(_bullet);

                _bullet = new Bullet(-450, 650, -1460, 1150);
                AddChild(_bullet);

                shotgunHasAttacked = true;
                shootBullets = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleSpawnEnemies()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleSpawnEnemies()
        {
            if (spawnEnemies == true)
            {
                _enemy = new Enemy(-1075, 530, -1140, -1000);
                AddChild(_enemy);

                enemiesHasSpawned = true;
                spawnEnemies = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        ChechIfBossIsVulnerable()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void ChechIfBossIsVulnerable()
        {
            if (currentState == BossState.Vulnerable)
            {
                Globals.bossIsAttacking = false;
            }
            else
            {
                Globals.bossIsAttacking = true;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Spawn()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Spawn()
        {
            SetFrame(0);

            SetXY(bossX, bossY);
            SetOrigin(width, 0);

            _hitbox_boss = new Hitbox_Boss();
            AddChild(_hitbox_boss);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleDeath()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleDeath()
        {
            if (Globals.bossIsDead == true)
            {
                LateDestroy();
                LateRemove();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttacks()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAttacks()
        {
            if (Globals.bossIsDead == false)
            {
                if (AllFrames == 0)
                {
                    SetState(BossState.Idle);
                }

                if (AllFrames == 7)
                {
                    SetState(BossState.AttackGrenade);
                }

                if (AllFrames == 14)
                {
                    SetState(BossState.Idle);
                }

                if (AllFrames == 21)
                {
                    SetState(BossState.AttackShotgun);
                }

                if (AllFrames == 28)
                {
                    SetState(BossState.Idle);
                }

                if (AllFrames == 35)
                {
                    SetState(BossState.AttackSpawnEnemies);
                }

                if (AllFrames == 42)
                {
                    SetState(BossState.Idle);
                }

                if (AllFrames == 49)
                {
                    SetState(BossState.Vulnerable);
                }

                if (AllFrames == 63)
                {
                    AllFrames = 0;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            HandleState();
            HandleDeath();
            ChechIfBossIsVulnerable();

            HandleAttacks();
        }
    }
}
