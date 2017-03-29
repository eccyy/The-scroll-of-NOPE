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

        protected float jumpAcceleration = 9.82f;

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

            if (keyHandler.IsKeyDown(Keys.A) && speed.X > -5)
                base.speed.X -= 1f;
            if (keyHandler.IsKeyDown(Keys.D) && speed.X <  5)
                base.speed.X += 1f;
            if (keyHandler.IsKeyDown(Keys.W) && speed.Y > -5)
                base.speed.Y -= 1f;
            if (keyHandler.IsKeyDown(Keys.S) && speed.Y <  5)
                base.speed.Y += 1f;


            base.position += speed;
            base.Update();

        }



        protected abstract void BasicAttack();
        
           
    }
   
}
