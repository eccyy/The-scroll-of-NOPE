using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses
{
    public abstract class Object // Allt här i är skrivet av William
    {
        // Variabler
        protected Texture2D texture; // objektes grafik
        protected Vector2 position; // objektets position

        // Draw(), metod för att rita ut object
        // Tar en  parameter: SpriteBatch
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position);
        }
    }

    public abstract class PhysicalObject : Object
    {
        // Metod för att kolla kollisioner
        // extremt simplifierad
        public bool CheckCollision(Rectangle source, Rectangle other)
        {
            // om de kolliderar med varandra returnera true annars returnera false
            return source.Intersects(other);
        }
    }

    public abstract class NonPhysicalObject : Object
    {
        // tom klass under första sprinten
    }
}
