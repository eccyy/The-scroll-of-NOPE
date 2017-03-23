using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses
{
    public abstract class Object
    {
        // Variabler
        protected Texture2D texture; // objektes grafik
        protected Vector2 position; // objektets position

        // Draw(), metod för att rita ut object
        // tar en  parameter: SpriteBatch
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position);
        }

        // getter och setter för texture (är det nödvändigt när den är protected och klassen är abstract??)
        public Texture2D Texture
        {
            get { return texture; }
            set { this.texture = value; }
        }

        // getter och setter för position (är det nödvändigt när den är protected och klassen är abstract??)
        public Vector2 Position
        {
            get { return position; }
            set { this.position = value; }
        }
    }

    public abstract class PhysicalObject : Object
    {
        protected Rectangle Hitbox;

        public bool CheckCollision(PhysicalObject other)
        {
            return true; // :^)
        }
    }

    public abstract class NonPhysicalObject : Object
    {
        // tom klass under första sprinten
    }
}
