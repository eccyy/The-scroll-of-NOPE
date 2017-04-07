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

        public Vector2 Position { get => position; }

        // Draw(), metod för att rita ut object
        // Tar en  parameter: SpriteBatch
        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(texture, position - camera.Position);
        }
    }

    public abstract class PhysicalObject : Object
    {
        // Method to check collisions
        // extremely simplified to handle spritesheet animations
        public bool CheckCollision(Rectangle source, Rectangle other)
        {
            return source.Intersects(other);
        }
    }

    public abstract class NonPhysicalObject : Object
    {
        // empty class for the first sprint (not needed yet)
    }
    #endregion
}
