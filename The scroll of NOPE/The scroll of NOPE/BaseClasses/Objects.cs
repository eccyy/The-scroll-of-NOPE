using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses
{
    #region William
    public abstract class Object
    {
        // Variables
        protected Texture2D texture; // the objects graphics
        protected Vector2 position; // the objects position

        public Vector2 Position { get { return position; } }

        // Draw(), method to draw graphics
        // Takes one parameter: SpriteBatch
        public virtual void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD)
        {
            #region Tommy
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

            //Draw
            spriteBatch.Draw(texture, zoomedDestination, null/*Entire texture*/, Color.White);
            #endregion
        }
    }

    public abstract class PhysicalObject : Object
    {
        protected Rectangle hitbox;
        public Rectangle Hitbox { get { return this.hitbox; } set { this.hitbox = value; } }

        // Method to check collisions
        // extremely simplified to handle spritesheet animations
        public bool CheckCollision(Rectangle other)
        {
            return hitbox.Intersects(other);
        }
    }

    public abstract class NonPhysicalObject : Object
    {
        // empty class for the first sprint (not needed yet)
    }
    #endregion
}
