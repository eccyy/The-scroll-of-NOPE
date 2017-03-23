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
    }

    public abstract class PhysicalObject : Object
    {
        // En hitbox för objektet, används för att kolla kollisioner
        protected Rectangle Hitbox;

        // Metod för att kolla kollisioner
        public bool CheckCollision()
        {
            return true; // :^)
        }
    }

    public abstract class NonPhysicalObject : Object
    {
        // tom klass under första sprinten
    }
}
