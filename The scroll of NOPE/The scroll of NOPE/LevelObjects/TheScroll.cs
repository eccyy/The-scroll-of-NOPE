﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace The_scroll_of_NOPE.LevelObjects
{
    class TheScroll : LevelObject
    {
        public TheScroll(Texture2D texture, Vector2 position, Rectangle hitbox)
        {
            this.texture = texture;
            this.hitbox = hitbox;
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD)
        {
            base.Draw(spriteBatch, camera, GD);
            //Add animation for scroll, floating in the air, up and down.
        }
    }
}
