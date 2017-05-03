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
        public Projectile(Vector2 position, Texture2D texture, Vector2 speed) : base()
        {
            this.texture = texture;
            this.position = position;
            this.hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.speed = speed;
        }

        public override void Update()
        {
            this.position += this.speed;
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD)
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