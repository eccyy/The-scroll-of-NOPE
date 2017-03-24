using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses.Players
{
    public abstract class Player : AnimateObject
    {
        protected float jumpAcceleration = 9.82f;

        private float _health;
        protected float health
        {
            get { return 1000f;}       
            set { _health = value; }
        }
        protected List<Projectile> projectiles { get; set; }
        
        
        public Player()
        {

        }
                

        public void Update()
        {
           
        }
    }
   
}
