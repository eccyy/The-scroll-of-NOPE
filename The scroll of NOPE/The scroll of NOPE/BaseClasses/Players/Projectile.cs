using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_scroll_of_NOPE.BaseClasses.Players
{
    #region Tommy
    public class Projectile : AnimateObject
    {
        protected float _dmg = 50;
        public float dmg
        {
            get
            {
                return _dmg;
            }
            protected set
            {
                _dmg = value;
            }
        }

        protected int decay;
        protected readonly DateTime creationTime = DateTime.Now;

        protected bool _isDead = false;
        public bool IsDead
        {
            get
            {
                return _isDead;
            }
        }

        /// <summary>
        /// Projectile shot by an entity
        /// </summary>
        /// <param name="position">The position of the projectile</param>
        /// <param name="texture">The texture to display for the projectile</param>
        /// <param name="speed">The speed the projectile flies</param>
        /// <param name="decay">How long the projectile is alive (ms)</param>
        public Projectile(Vector2 position, Texture2D texture, Vector2 speed, int decay, Vector2 spritesheetDimensions) : base()
        {
            this.texture = texture;
            this.position = position;
            this.hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.speed = speed;
            this.decay = decay;

            
        }

        public override void Update()
        {
            this.position += this.speed;

            // Important otherwise the bullet will not actually hit
            this.Hitbox = new Rectangle((int)position.X, (int)position.Y, Hitbox.Height, Hitbox.Width);

            // Check if the projectile should be killed
            if (this.creationTime.AddMilliseconds(this.decay) <= DateTime.Now)
            {
                this._isDead = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD, GameTime gameTime)
        {
            //Set destination rectangle without scaling
            Rectangle unzoomedDestination = new Rectangle(
                new Point((int)(position - camera.Position).X, (int)(position - camera.Position).Y),
                new Point(texture.Width, texture.Height));

            // Store center of screen
            Point Center = new Point(GD.Viewport.X + GD.Viewport.Width / 2, GD.Viewport.Y + GD.Viewport.Height / 2);

            // Scale position and size
            Rectangle zoomedDestination = new Rectangle(
                (int)(unzoomedDestination.X * camera.ZoomFactor),
                (int)(unzoomedDestination.Y * camera.ZoomFactor),
                (int)(unzoomedDestination.Width * camera.ZoomFactor),
                (int)(unzoomedDestination.Height * camera.ZoomFactor));

            // New center in accordance with the scaling
            Point newCenter = new Point((int)(Center.X * camera.ZoomFactor), (int)(Center.Y * camera.ZoomFactor));

            // Restore center
            zoomedDestination.Location += Center - newCenter;

            // Draw
            float angle = (float)Math.Atan2(this.speed.Y, this.speed.X); // The angle to rotate the texture depending on the mouse pointer's orientation relative to the player

            spriteBatch.Draw(
                this.texture, 
                zoomedDestination, 
                null, 
                Color.White, 
                angle, 
                new Vector2(this.texture.Width / 2, this.texture.Height / 2), 
                SpriteEffects.None, 
                0);
        }
    }
    #endregion
}