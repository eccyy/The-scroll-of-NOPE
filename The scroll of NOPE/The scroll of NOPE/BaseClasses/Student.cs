using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses.Players
{
#region Tommy
    public abstract class Student : Player
    {
        public Student(Texture2D texture, Vector2 position, int ppfMaxSpeed)
        {
            // Sets the necessary variables of the baseclass which are given as inputs to the contructor
            this.texture  = texture;
            this.position = position;
            this.maxSpeed = ppfMaxSpeed;

            // Sets non-input variables in the baseclass
            this.projectiles = new List<Projectile>(); // Initialize of list
            this.health      = 1000; // Sets health since it's not specified
        }

        // If you want to specify health
        public Student(Texture2D texture, Vector2 position, int speed, float health) : this(texture, position, speed)
        {
            this.health = health;
        }

        public override void Update()
        {
            base.Update();
        }

        protected override void AttackBasic()
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
    }

    public abstract class Melee : Student
    {
        public Melee(Texture2D texture, Vector2 position, int speed) : base(texture, position, speed)
        {
        }

        protected override void AttackBasic()
        {

        }
    }

    public abstract class Ranged : Student
    {
        public Ranged(Texture2D texture, Vector2 position, int speed) : base(texture, position, speed)
        {
        }
        
        protected override void AttackBasic()
        {

        }
    }

    public class Student1 : Melee
    {
        public Student1(Texture2D texture, Vector2 position, int speed) : base(texture, position, speed)
        {

        }
    }

    public class Student2 : Ranged
    {
        public Student2(Texture2D texture, Vector2 position, int speed) : base(texture, position, speed)
        {
            
        }
    }
    #endregion
}
