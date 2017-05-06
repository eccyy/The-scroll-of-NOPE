using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                }
            }
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
