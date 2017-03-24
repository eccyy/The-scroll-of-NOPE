using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses.Players
{
    public abstract class Student : Player
    {
        public Student(Texture2D texture, Vector2 position, Vector2 speed)
        {
            // Sets the necessary variables of the baseclass which are given as inputs to the contructor
            this.texture  = texture;
            this.position = position;
            this.speed    = speed;

            // Sets non-input variables in the baseclass
            this.projectiles = new List<Projectile>(); // Initialize of list
            this.health      = 1000; // Sets health since it's not specified
        }

        // If you want to specify health
        public Student(Texture2D texture, Vector2 position, Vector2 speed, float health) : this(texture, position, speed)
        {
            this.health = health;
        }

        protected override void Update()
        {
            throw new NotImplementedException();
        }

        protected override abstract void BasicAttack();
    }

    public class Melee : Student
    {
        public Melee(Texture2D texture, Vector2 position, Vector2 speed) : base(texture, position, speed)
        {
        }

        protected override void BasicAttack()
        {
            throw new NotImplementedException();
        }
    }

    public class Ranged : Student
    {
        public Ranged(Texture2D texture, Vector2 position, Vector2 speed) : base(texture, position, speed)
        {
        }

        protected override void BasicAttack()
        {
            throw new NotImplementedException();
        }
    }
}
