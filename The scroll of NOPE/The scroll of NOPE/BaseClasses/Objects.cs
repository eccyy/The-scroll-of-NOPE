using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses
{
    public abstract class Object // Everything in this file is written by William
    {
        // Variables
        protected Texture2D texture; // the objects graphics
        protected Vector2 position; // the objects position

        // Draw(), method to draw the object
        // Takes one parameter: SpriteBatch
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position);
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
}
