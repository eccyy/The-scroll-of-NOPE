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
            base.Update();

            if (keyHandler.IsKeyDown(Keys.Left))
                speed.X -= 10;
            if (keyHandler.IsKeyDown(Keys.Right))
                speed.X += 10;
            if (keyHandler.IsKeyDown(Keys.Up))
                speed.Y += 10;
            if (keyHandler.IsKeyDown(Keys.Down))
                speed.Y -= 10;

           
        }



        protected abstract void BasicAttack();
        
           
    }
   
}
