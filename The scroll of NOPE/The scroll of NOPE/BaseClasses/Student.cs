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
        protected int chargeDelayBasicAttack_ms = 25;
        protected int cooldownBasicAttack_ms = 25;

        protected DateTime lastBasicAttack = DateTime.Now;
        protected DateTime beginOfChargeBasicAttack = DateTime.Now; // Last time the player initialized the basic attack
        protected bool chargingBasicAttack = false;

        protected Vector2 basicAttackSpritesheetDimensions = Vector2.One;
        protected Vector2 specialAttack1SpritesheetDimensions = Vector2.One;

        public Student(Texture2D texture, Vector2 position, int ppfMaxSpeed)
        {
            // Sets the necessary variables of the baseclass which are given as inputs to the contructor
            this.texture  = texture;
            this.position = position;
            this.maxSpeed = ppfMaxSpeed;

            // Sets non-input variables in the baseclass
            this.projectiles = new List<Projectile>(); // Initialize of list
            this.Health      = 1000; // Sets Health since it's not specified
        }

        // If you want to specify Health
        public Student(Texture2D texture, Vector2 position, int speed, float Health) : this(texture, position, speed)
        {
            this.Health = Health;
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

        /// <summary>
        /// Performes a ranged attack if the player tries to shoot and is permitted to do so
        /// </summary>
        /// <param name="camera">The camera in use</param>
        /// <param name="chargeDelay_ms">The number of milliseconds to charge the attack</param>
        /// <param name="cooldownAttack_ms">The number of milliseconds to wait after the attack before a new charge can be performed</param>
        /// <param name="lastAttack">The time when the last attack of the same type was performed</param>
        /// <param name="chargingAttack">A boolean indicating whether the attack is charging or not</param>
        /// <param name="beginOfChargeAttack">The time when the charging of the attack started</param>
        /// <param name="Attack">The type of attack (should be the method of which the attack will use to perform the attack)</param>
        protected void UpdateRangedAttack(Camera camera, int chargeDelay_ms, int cooldownAttack_ms, ref DateTime lastAttack, ref bool chargingAttack, ref DateTime beginOfChargeAttack, Action<Camera> Attack)
        {
            // Basic Attack
            if (Mouse.GetState().LeftButton == ButtonState.Pressed /*If the mouse is clicked*/)
            {
                // If the player can shoot
                if (lastAttack.AddMilliseconds(cooldownAttack_ms) <= DateTime.Now /*Checks if the cooldown has passed*/)
                {
                    if (!chargingAttack) // Sets the begin time of the charge
                    {
                        chargingAttack = true;
                        beginOfChargeAttack = DateTime.Now; // Starts charging
                    }
                    else if (beginOfChargeAttack.AddMilliseconds(chargeDelay_ms) <= DateTime.Now) // If the attack is fully charged
                    {
                        Attack(camera); // Perform basic attack
                        lastAttack = DateTime.Now; // Reset when the last attack was performed
                    }
                }
                else
                {
                    chargingAttack = false; // Reset charging when cooldown is active
                }
            }
            else
            {
                chargingAttack = false;
            }
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
            float absoluteSpeed = 10.0f;

            Vector2 Direction = Mouse.GetState().Position.ToVector2() - (this.Position-camera.Position); // Gets the position of the mouse relative to the player
            Direction.Normalize();

            this.projectiles.Add(
                new Projectile(
                    this.position, 
                    projectileTexture1, 
                    absoluteSpeed * Direction, 
                    10 * 1000 /*10 Seconds*/, 
                    basicAttackSpritesheetDimensions)); // Create new projectile
        }

        public override void Update(Camera camera)
        {
            // Basic Attack
            UpdateRangedAttack(
                camera, 
                chargeDelayBasicAttack_ms, 
                cooldownBasicAttack_ms, 
                ref lastBasicAttack, 
                ref chargingBasicAttack, 
                ref beginOfChargeBasicAttack, 
                AttackBasic);

            base.Update();
        }
        

        public override void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD, GameTime gameTime)
        {
            base.Draw(spriteBatch, camera, GD, gameTime);

            if (chargingBasicAttack)
                spriteBatch.Draw(texture, new Rectangle(0,0,10,10), Color.Red);
        }
    }

    public class Student1 : Melee
    {
        Texture2D textureAttackSpecial1;

        public Student1(Texture2D texture, Vector2 position, int speed, Microsoft.Xna.Framework.Content.ContentManager _content, Vector2 spritesheetDimensions) : base(texture, position, speed)
        {
            this.textureAttackSpecial1 = _content.Load<Texture2D>("Animation/Explosion");
        }

        protected override void AttackBasic(Camera camera)
        {
            base.AttackBasic(camera);
        }

        protected int chargeDelaySpecialAttack1_ms = 100;
        protected int cooldownSpecialAttack1_ms = 100;
        protected DateTime lastSpecialAttack1 = DateTime.Now;
        protected bool chargingSpecialAttack1 = false;
        protected DateTime beginOfChargeSpecialAttack1 = DateTime.Now;
        protected void AttackSpecial1(Camera camera)
        {
            float absoluteSpeed = 10.0f;

            Vector2 Direction = Mouse.GetState().Position.ToVector2() - (this.Position - camera.Position); // Gets the position of the mouse relative to the player
            Direction.Normalize();

            this.projectiles.Add(
                new Projectile(
                    this.position, 
                    this.textureAttackSpecial1, 
                    absoluteSpeed * Direction, 10 * 1000 /*10 Seconds*/, 
                    specialAttack1SpritesheetDimensions)); // Create new projectile
        }

        public override void Update(Camera camera)
        {
            // Special Attack 1
            UpdateRangedAttack(camera, chargeDelaySpecialAttack1_ms, cooldownSpecialAttack1_ms, ref lastSpecialAttack1, ref chargingSpecialAttack1, ref beginOfChargeSpecialAttack1, AttackSpecial1);

            base.Update();
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
