﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_scroll_of_NOPE.LevelObjects
{
    #region Lucas
    public class Ground : LevelObject
    {
       
        public Ground(Texture2D texture, Vector2 position, Rectangle hitbox)
        {
            //Sets this texture to the texture it gets all the way from LevelLayout
            this.texture = texture;

            this.hitbox = hitbox;
            //Gets the position
            this.position = position;
        }
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);
        }
    }
    #endregion
}
