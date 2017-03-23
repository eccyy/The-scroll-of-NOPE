﻿using Microsoft.Xna.Framework;
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
        // Tar en  parameter: SpriteBatch
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position);
        }
    }

    public abstract class PhysicalObject : Object
    {
        // Metod för att kolla kollisioner
        // Tar en parameter: PhysicalObject (polymorphism, skicka in t.ex ANKA, en elev eller ett level objekt)
        public bool CheckCollision(PhysicalObject other)
        {
            Rectangle firstRect = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), Convert.ToInt32(texture.Width), Convert.ToInt32(texture.Height));
            Rectangle otherRect = new Rectangle(Convert.ToInt32(other.position.X), Convert.ToInt32(other.position.Y), Convert.ToInt32(other.texture.Width), Convert.ToInt32(other.texture.Height));

            return firstRect.Intersects(otherRect);
        }
    }

    public abstract class NonPhysicalObject : Object
    {
        // tom klass under första sprinten
    }
}
