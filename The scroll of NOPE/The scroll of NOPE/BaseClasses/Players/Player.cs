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
    #region Jonatan
    public abstract class Player : AnimateObject
    {
        KeyboardState keyHandler = new KeyboardState();
        protected float acceleration = 1f;
        protected float jumpAcceleration = 9.82f;
        protected float maxSpeed;
        int mapHeight = 300;
        float jumpHeight = 500;
        bool hasJumped;
        float jumpTimer = 0;

        bool canJump;

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
            /*

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

            */
            #endregion

            #region Jonatans hopp, om inte den andra skulle fungera

           
         
         
            // Gravitationen vid hopp och ett stopp vid map height pixlar ner. 
            // Needs to be changed to incorporate all other collisions

            /*
            if(base.position.Y < 600)
                base.speed.Y += 1f;
            else
            {
                base.speed.Y = 0;
            }


    */

            // Spelaren trycker på hoppknappen
            if (keyHandler.IsKeyDown(Keys.Space))
            {
                // Accelleration add some time
                if (canJump)
                {
                    speed.Y -= 25;
                    canJump = false;
                }                
            }
            
            #endregion

            base.position += speed;

            // Gravitation
            speed.Y += 1;


            // Updaterar hitboxen, kan bli fel med kollisioner pga hur hitbox updateras

            hitbox.X = (int)base.position.X;
            hitbox.Y = (int)base.position.Y;
            
            hitbox.Height = texture.Height;
            hitbox.Width = texture.Width;
            base.Update();
        }


        // Anka har ingen kollision
        // Kanske kan skriva ut allting till en klass   
        
        public void Collision(List<BaseClasses.PhysicalObject> collidables)
        {
            // Komma åt sakerna som kan kollidera med player. 
            // Om kollision med vapen, ta skada beroende på vapenSkada

            // Om kollision med kula, ta skada och ta bort kulan

            //kollar varje sak i listan och gör saker beroende på typ
            foreach (PhysicalObject collidable in collidables)
            {
                //if check itself, list created in game1 contains an instance of the instance that is checking


                if (collidable is Student1)
                {
                    var student = collidable as Student1;
                    // when it collides
                    if (CheckCollision(student.Hitbox))
                    {
                        // Treat it as a student to gain access to the properties
                       

                        student.speed.X = 0;
                    }

                    //Check the bullet collisions
                    
                }
              
                else if (collidable is LevelLayout)
                {
                    // Treat as a LevelLayout to gain access to the properties
                    var layout = collidable as LevelLayout;

                    // collion with platforms
                    foreach (Platform platformObject in layout.Platforms)
                    {
                        var platform = platformObject as Platform;

                        // if it collides
                        if (CheckCollision(platform.Hitbox))
                        {
                            // Player is now touching the platform and can therefore jump
                            canJump = true;

                            // Check where it is using rectangles
                            if (Hitbox.Bottom > platform.Hitbox.Top)
                            {
                                speed.Y = 0;
                            }
                            if (Hitbox.Top < platform.Hitbox.Bottom)
                            {
                                speed.Y = 0;
                            }


                            
                            
                            // If it is on left or right stop X position
                        }

                    }

                    // Collision with the ground
                    foreach (Ground ground in layout.Grounds)
                    {
                        if (CheckCollision(ground.Hitbox))
                        {
                            
                            // reduce the speed
                            speed.Y = 0;
                            
                            // Player is now touching the ground and can therefore jump
                            canJump = true;
                        }
                    }
                }
               
            }
        }



        protected abstract void AttackBasic();            
        protected abstract void AttackH();           
        protected abstract void AttackJ();           
        protected abstract void AttackK();           
        protected abstract void AttackL();           
       


    }
    #endregion
}
