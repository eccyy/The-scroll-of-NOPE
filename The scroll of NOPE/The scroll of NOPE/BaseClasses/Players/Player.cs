using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses.Players
{
    public abstract class Player : AnimateObject
    {
        KeyboardState keyHandler = new KeyboardState();
        protected float acceleration = 1f;
        protected float jumpAcceleration = 9.82f;
        protected float maxSpeed;
        int mapHeight = 300;

        public Player()
        {
            
        }

        
        private float _health;
        protected float health
        {
            get { return 1000f;}       
            set { _health = value; }
        }
        protected List<Projectile> projectiles { get; set; }


        protected virtual void Update()
        {
            // Rörelsen på spelaren och acceleration på rörelsen.
            keyHandler = Keyboard.GetState();

            if (keyHandler.IsKeyDown(Keys.A) && speed.X > -maxSpeed)
                base.speed.X -= acceleration;
            if (keyHandler.IsKeyDown(Keys.D) && speed.X <  maxSpeed)
                base.speed.X += acceleration;
            // Spelaren hoppar
            if (keyHandler.IsKeyDown(Keys.W) && speed.Y == 0)
                base.speed.Y -= acceleration*15;
            if (keyHandler.IsKeyDown(Keys.S) && speed.Y <  maxSpeed)
                base.speed.Y += acceleration;
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
          

        


            base.position += speed;

            // Gravitationen vid hopp och ett stopp vid map height pixlar ner. 
            if(base.position.Y < mapHeight)
                base.speed.Y += 1f;
            else
            {
                base.speed.Y = 0;
            }

            base.Update();

        }

                                                     
                                                     
        protected abstract void AttackBasic();            
        protected abstract void AttackH();           
        protected abstract void AttackJ();           
        protected abstract void AttackK();           
        protected abstract void AttackL();           
       


    }
   
}
