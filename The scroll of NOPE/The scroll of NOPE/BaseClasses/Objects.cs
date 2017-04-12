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
    // check the multiplayer branch for more stuff I've done.
    public abstract class Object
    {
        // Variables
        protected Texture2D texture; // the objects graphics
        protected Vector2 position; // the objects position

        public Vector2 Position { get { return position; } }

        // Draw(), method to draw graphics
        // Takes one parameter: SpriteBatch
        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            #region Tommy
            // spriteBatch.Draw(texture, position - camera.Position);
            Rectangle unzoomedDestination = new Rectangle(
                new Point((int)(position - camera.Position).X, (int)(position - camera.Position).Y), 
                new Point(texture.Width, texture.Height));

            Rectangle zoomedDestination = unzoomedDestination;
            zoomedDestination.Inflate(
                zoomedDestination.Width * (camera.ZoomFactor - 1), //Zoom X
                zoomedDestination.Height * (camera.ZoomFactor - 1)); //Zoom Y

            spriteBatch.Draw(texture, new Rectangle(), null/*Entire texture*/, Color.White);
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
