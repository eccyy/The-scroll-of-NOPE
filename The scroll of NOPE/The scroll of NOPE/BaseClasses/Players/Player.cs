using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_scroll_of_NOPE.LevelObjects;
using System.Windows;
using Microsoft.Xna.Framework.Graphics;

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

        

        public double tempPlayerAngle;

        bool canJump;

        public Player()
        {
            projectiles = new List<Projectile>();
        }

        private float _Health;

        public float Health
        {
            get { return _Health; }
            protected set { _Health = value; }
        }

        private List<Projectile> _projectiles;

        public List<Projectile> projectiles {
            get {return _projectiles; }
            protected set { _projectiles = value; }
        }


        public override void Update()
        {
            #region Tommy
            // Update projectiles
            foreach (Projectile projectile in projectiles)
            {
                projectile.Update();
            }

            // Remove dead (decayed) projectiles
            List<Projectile> tempList = new List<Projectile>(projectiles);
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i].IsDead)
                    projectiles.RemoveAt(i);
            }
            #endregion

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

            /*
            hitbox.X = (int)base.position.X -1;
            hitbox.Y = (int)base.position.Y -1;
            
            hitbox.Height = texture.Height +2;
            hitbox.Width = texture.Width +2;
            */

            hitbox.X = (int)base.position.X - 1;
            hitbox.Y = (int)base.position.Y - 1;
            
            hitbox.Height = sourceRectangle.Height + 2;
            hitbox.Width = sourceRectangle.Width + 2;

            base.Update();
        }
        
        public virtual void Collision(List<BaseClasses.PhysicalObject> collidables)
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

                    foreach (HeartPickup heart in new List<HeartPickup>(layout.Hearts))
                    {
                        if (CheckCollision(heart.Hitbox))
                        {
                            Health += 1000;
                            layout.Hearts.Remove(heart);
                            layout.heartCounter -= 1;
                        }
                    }

                    // collion with platforms
                    foreach (Platform platformObject in layout.Platforms)
                    {
                        var platform = platformObject as Platform;



                        // if it collides
                        if (CheckCollision(platform.Hitbox))
                        {


                            // Check where it is colliding using trigonometry? the angle decides where it collided. if greater than arctan(o/a)
                            double playerAngle = Math.Atan2((double)(platform.Hitbox.Center.Y - this.Hitbox.Center.Y), (double)(platform.Hitbox.Center.X - this.hitbox.Center.X));


                            double cornerAngle = Math.Atan2(platform.Hitbox.Height / 2 + hitbox.Height / 2, platform.Hitbox.Width / 2 + Hitbox.Width);

                            tempPlayerAngle = playerAngle;

                            double a = -Math.PI + cornerAngle;
                            double b = Math.PI - cornerAngle;

                            //  double platformAngle = Math.Atan2(Hitbox.Center.Y-platform.Hitbox.Center.Y,Hitbox.Center.X-platform.Hitbox.Center.X);

                            //    double relativeAngle = Math.Atan2(hitbox.Center.Y - platform.Hitbox.Center.Y, hitbox.Center.X - platform.Hitbox.Center.X);
                            //    double anotherRelativeAngle = Math.Atan2(platform.Hitbox.Center.Y - Hitbox.Center.Y, platform.Hitbox.Center.X - Hitbox.Center.X);

                            // Above
                            if (playerAngle > cornerAngle && playerAngle < (Math.PI - cornerAngle))
                            {
                                // Set position of player to the left of the platform so that it is not inside of the platform once it draws
                                // +2: player hitbox is bigger than the texture because collision is when hitboxes intersects which would make the textures intersect
                                position.Y = platform.Position.Y - Hitbox.Height + 2;
                                // Set speed to 0 so that the player does not move into the platform 
                                speed.Y = 0;

                                // Player is now touching the platform and can therefore jump
                                decimal aasd = Math.Round((decimal)playerAngle, 4);
                                if(Math.Round(playerAngle,4) != 0.4412)
                                {
                                    canJump = true;
                                }
                               
                            }
                            // Below
                            if (playerAngle < (-cornerAngle) && playerAngle > (-Math.PI + cornerAngle))
                            {

                                position.Y = platform.Position.Y + platform.Hitbox.Height + 1;
                                speed.Y = 0;
                            }
                            // Right

                            
                        

                        else if (playerAngle < a || playerAngle > b)
                        {
                            position.X = (platform.Position.X + platform.Hitbox.Width);
                            speed.X = 0;
                        }
                        // Left
                        else if (playerAngle > (-cornerAngle) && playerAngle < cornerAngle)
                        {
                            this.position.X = (platform.Position.X - this.Hitbox.Width) + 2;
                            speed.X = 0;
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
                            // Move the player onto the ground(actually 1 pixel in)
                            this.position.Y = ground.Position.Y - this.Hitbox.Height +2;
                            
                            // Player is now touching the ground and can therefore jump
                            canJump = true;
                        }
                    }
                    
                }
               
            }
        }

        protected abstract void AttackBasic(Camera camera);            
        protected abstract void AttackH();           
        protected abstract void AttackJ();           
        protected abstract void AttackK();           
        protected abstract void AttackL();

        public override void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD, GameTime gameTime)
        {
            
            // Draw projectiles
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch, camera, GD, gameTime);
            }
            
            // Draw this
            base.Draw(spriteBatch, camera, GD, gameTime);
        }
    }
    #endregion
}
