using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace The_scroll_of_NOPE.BaseClasses.Players
{
#region Tommy
    public abstract class Student : Player
    {
        protected int chargeDelayBasicAttack_ms = 500;
        protected int cooldownBasicAttack_ms = 1000;

        protected DateTime lastBasicAttack = DateTime.Now;
        protected DateTime beginOfChargeBasicAttack = DateTime.Now; // Last time the player initialized the basic attack
        protected bool chargingBasicAttack = false;

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

        public virtual void Update(Camera camera)
        {
            base.Update();
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
    }

    public abstract class Melee : Student
    {
        public Melee(Texture2D texture, Vector2 position, int speed) : base(texture, position, speed)
        {
        }

        protected override void AttackBasic(Camera camera)
        {

        }
    }

    public abstract class Ranged : Student
    {
        Texture2D projectileTexture1;

        public Ranged(Texture2D texture, Vector2 position, int speed, Texture2D projectileTexture1) : base(texture, position, speed)
        {
            this.projectileTexture1 = projectileTexture1;
        }

        // Is called to make a basic attack
        protected override void AttackBasic(Camera camera)
        {
            float absoluteSpeed = 4.0f;

            Vector2 Direction = Mouse.GetState().Position.ToVector2() - (this.Position-camera.Position); // Gets the position of the mouse relative to the player
            Direction.Normalize();

            this.projectiles.Add(new Projectile(this.position, projectileTexture1, absoluteSpeed * Direction)); // Create new projectile
        }

        public override void Update(Camera camera)
        {
            // Basic Attack
            if (Mouse.GetState().LeftButton == ButtonState.Pressed /*If the mouse is clicked*/)
            {
                // If the player can shoot
                if (lastBasicAttack.AddMilliseconds(cooldownBasicAttack_ms) <= DateTime.Now /*Checks if the cooldown has passed*/)
                {
                    if (!chargingBasicAttack) // Sets the begin time of the charge
                    {
                        chargingBasicAttack = true;
                        beginOfChargeBasicAttack = DateTime.Now; // Starts charging
                    }
                    else if (beginOfChargeBasicAttack.AddMilliseconds(chargeDelayBasicAttack_ms) <= DateTime.Now) // If the attack is fully charged
                    {
                        AttackBasic(camera); // Perform basic attack
                        lastBasicAttack = DateTime.Now; // Reset when the last attack was performed
                    }
                }
                else
                {
                    chargingBasicAttack = false; // Reset charging when cooldown is active
                }
            }
            else
            {
                chargingBasicAttack = false;
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD)
        {
            base.Draw(spriteBatch, camera, GD);

            if (chargingBasicAttack)
                spriteBatch.Draw(texture, new Rectangle(0,0,10,10), Color.Red);
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
        public Student2(Texture2D texture, Vector2 position, int speed, Texture2D projectileTexture1) : base(texture, position, speed, projectileTexture1)
        {

        }
    }
    #endregion
}
