﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses
{
    abstract class Object
    {
        // Variabler
        protected Texture2D Texture;
        protected Vector2 Position;

        // Draw(), metod för att rita ut object
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position);
        }
    }

    abstract class PhysicalObject
    {
        protected Rectangle Hitbox;

        
    }

    abstract class NonPhysicalObject
    {
        // empty class for the first sprint
    }
}
