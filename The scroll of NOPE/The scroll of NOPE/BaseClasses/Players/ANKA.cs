using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_scroll_of_NOPE.LevelObjects;

namespace The_scroll_of_NOPE.BaseClasses.Players
{
    #region Jonatan
    class ANKA : Player
    {
        float buffMultiplier;
       

        public ANKA(int studentCount,Texture2D texture, Vector2 position, float maxSpeed, float Health):base()
        {
            base.Health = Health;
            base.maxSpeed = maxSpeed;
            base.position = position;
            // Tar antalet spelare och räknar ut hur mycket starkare ANKA ska vara. Sedan ökar balanseringsvärden som Health.
            BuffCalculator(studentCount);
            base.texture = texture;
        }

        public override void Update()
        {
            base.Update();

        }

      
        // Override to be able to do ANKA specific collision
        public override void Collision(List<BaseClasses.PhysicalObject> collidables)
        {
            base.Collision(collidables);

            // Anka specific collisions
            foreach (PhysicalObject collidable in collidables)
            {

                // Level collisions
                if (collidable is LevelObjects.LevelLayout)
                {
                    LevelObjects.LevelLayout levelLayout = collidable as LevelObjects.LevelLayout;

                    // Collision with The scroll of NOPE / ANKA wins
                    if (CheckCollision(levelLayout.theScroll.Hitbox))
                    {
                        throw new Exception("Anka won, not implemented yet so we crash, neat huh?");
                    }
                    //Anka picks up heart, should be made for player aswell.
                    try
                    {
                        foreach (HeartPickup heart in new List<HeartPickup>(levelLayout.Hearts))
                        {
                            if (CheckCollision(heart.Hitbox))
                            {
                                Health += 1000;
                                levelLayout.Hearts.Remove(heart);
                            }
                        }
                    }
                    catch
                    {

                    }
                }

                // Attack collisions with student1
                if(collidable is Student2)
                {
                    var student = collidable as Student2;


/* Bullet collisions now in another method                   
                    // Don't check if there's no bullets
                    if(student.projectiles.Count != 0)
                    {
                        foreach (Projectile projectile in student.projectiles)
                        {
                            // If it collides deal damage and remove the bullet
                            if (CheckCollision(projectile.Hitbox))
                            {
                                this.Health -= projectile.dmg;
                                
                                
                            }
                        }
                    }
 */                   
                }
            }

            
        }
        public int bulletCollision(List<Projectile> projectiles)
        {
            if(projectiles != null)
            {
                int bulletIndex = 0;
                foreach(Projectile projectile in projectiles)
                {
                    // if hit deal damage
                    if (CheckCollision(projectile.Hitbox))
                    {
                        Health -= projectile.dmg;

                        // return bullet with bulletIndex for destruction
                        return bulletIndex;
                    }
                    bulletIndex++;
                }
            }

            // if no bullet collision
            return -1;
        }

        protected override void AttackBasic(Camera camera)
        {

        }
        protected override void AttackH()
        {

        }
        protected override void AttackJ()
        {

        }
        protected override void AttackK()
        {

        }
        protected override void AttackL()
        {

        }

        private void BuffCalculator(int studentCount)
        {
            Health *= (studentCount * 1f);

        }
    }
    #endregion
}
