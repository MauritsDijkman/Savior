using System;
using GXPEngine;

namespace GXPEngine
{
    class Hitbox_Player : Sprite
    {
        Sound _damageSound;

        bool MCDamagetake;
        float Damagecounter;

        bool MCDamagetakeGrenade;
        float DamagecounterGrenade;

        bool bulletDidDamage;
        float timer;

        public Hitbox_Player() : base("hitbox_player.png")
        {
            _damageSound = new Sound("damage_sound_player.wav", false, false);

            visible = false;

            MCDamagetakeGrenade = true;
            bulletDidDamage = false;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HitboxUpdate()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HitboxUpdate()
        {
            if (Globals.MCfacingLeft == false && Globals.MCfacingRight == true)
            {
                SetOrigin(width / 2, height / 2);
                SetXY(5, -100);
            }

            if (Globals.MCfacingLeft == true && Globals.MCfacingRight == false)
            {
                SetOrigin(width / 2, height / 2);
                SetXY(-5, -100);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        OnCollision()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void OnCollision(GameObject other)
        {
            if (MCDamagetake == false)
            {
                Damagecounter = Damagecounter + 1;
            }

            if (Damagecounter == 200)
            {
                MCDamagetake = true;
                Damagecounter = 0;
            }

            if (other is Hitbox_Enemy && Globals.countFramesAttackEnemy == 6 && MCDamagetake == true)
            {
                Globals.health_player = Globals.health_player - 1;
                _damageSound.Play();
                MCDamagetake = false;
            }

            if (MCDamagetakeGrenade == false)
            {
                DamagecounterGrenade = DamagecounterGrenade + 1;
            }

            if (DamagecounterGrenade == 10000)
            {
                MCDamagetakeGrenade = true;
                DamagecounterGrenade = 0;
            }

            if (other is Grenade && Globals.GrenadeDoesDamage == true && MCDamagetakeGrenade == true)
            {
                Globals.health_player = Globals.health_player - 2;
                _damageSound.Play();
                MCDamagetakeGrenade = false;
            }

            if (bulletDidDamage == true)
            {
                timer = timer + 1;
            }

            if (timer == 60)
            {
                bulletDidDamage = false;
                timer = 0;
            }

            if (other is Bullet && bulletDidDamage == false)
            {
                Globals.health_player = Globals.health_player - 1;
                _damageSound.Play();
                bulletDidDamage = true;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            HitboxUpdate();
        }
    }
}
