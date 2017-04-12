using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_scroll_of_NOPE.LevelObjects;

namespace The_scroll_of_NOPE.BaseClasses.Players
{
    #region Jonte
    public abstract class Player : AnimateObject
    {
        KeyboardState keyHandler = new KeyboardState();
        protected float acceleration = 1f;
        protected float jumpAcceleration = 9.82f;
        protected float maxSpeed;
        int mapHeight = 300;
        float jumpHeight = 5;
        bool hasJumped;

        public Player()
        {
            
        }

        protected float health;
              
        protected List<Projectile> projectiles { get; set; }


        public override void Update()
        {
            // Rörelsen på spelaren och acceleration på rörelsen.
            keyHandler = Keyboard.GetState();

            if (keyHandler.IsKeyDown(Keys.A) && speed.X > -maxSpeed)
                base.speed.X -= acceleration;
            if (keyHandler.IsKeyDown(Keys.D) && speed.X <  maxSpeed)
                base.speed.X += acceleration;


            if (keyHandler.IsKeyDown(Keys.H))
                AttackH();
            if (keyHandler.IsKeyDown(Keys.J))
                AttackJ();
            if (keyHandler.IsKeyDown(Keys.K))
                AttackK();
            if (keyHandler.IsKeyDown(Keys.L))
                AttackL();
            if (keyHandler.IsKeyDown(Keys.H))
                AttackH();

            #region Lucas JumpLogic
            //Om man trycker space och hasJumped är false (man är inte i luften) så hoppar man.
            if ((keyHandler.IsKeyDown(Keys.Space) || keyHandler.IsKeyDown(Keys.W)) && hasJumped == false)
            {
                position.Y -= 10f;
                base.speed.Y = -12f;
                hasJumped = true;
            }

            //Om man har hoppat så är detta gravitationen, kan ändras genom att ändra float i.
            if (hasJumped == true)
            {
                float i = 3;
                base.speed.Y += 0.15f * i;
            }

            //Om man landar på marken så är hasJumped = false och man kan nu hoppa igen
            if (position.Y + texture.Height >= 450)
                hasJumped = false;
            //Är man på marken finns inget som drar en ner genom mappen
            if (hasJumped == false)
                base.speed.Y = 0f;
            //Ska läggas till
            //hasJumped = false om man kolliderar med en platform
            //Går man av en platform ska hasJumped sättas till true
            #endregion

            #region Jontes hopp, om inte den andra skulle fungera
            /* Jontes hopp
            // Spelaren hoppar
            if (keyHandler.IsKeyDown(Keys.Space))
                if (speed.Y == 0 && jumpTimer < jumpHeight)
                {
                    base.speed.Y -= acceleration * 5;
                    jumpTimer++;
                }
                else if (speed.Y == 0)
                {
                    jumpTimer = 0;
                }
            */

            /* Jontes Gravitation
            // Gravitationen vid hopp och ett stopp vid map height pixlar ner. 
            if(base.position.Y < mapHeight)
                base.speed.Y += 1f;
            else
            {
                base.speed.Y = 0;
            }
            */
            #endregion

            base.position += speed;

            // Updaterar hitboxen
            hitbox.X = (int)base.position.X;
            hitbox.Y = (int)base.position.Y;

            hitbox.Height = texture.Height;
            hitbox.Width = texture.Width;
            base.Update();
        }




       public void Collision(List<BaseClasses.PhysicalObject> collidables, LevelLayout levelLayout)
        {
            // Komma åt sakerna som kan kollidera med player. 
            // Om kollision med vapen, ta skada beroende på vapenSkada

            // Om kollision med kula, ta skada och ta bort kulan

            //kollar varje sak i listan och gör saker beroende på typ
            foreach(PhysicalObject collidable in collidables)
            {
               
               if (collidable is Student1)
                {
                    if (CheckCollision(collidable.Hitbox))
                    {

                    }
                }
            }

            //level collisions
           // foreach(Platform platform in levelLayout.)
            
            #region Kollision med plattformar
           
              

            #endregion



        }



        protected abstract void AttackBasic();            
        protected abstract void AttackH();           
        protected abstract void AttackJ();           
        protected abstract void AttackK();           
        protected abstract void AttackL();           
       


    }
    #endregion
}
