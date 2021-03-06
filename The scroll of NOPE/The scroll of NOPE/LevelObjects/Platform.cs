﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace The_scroll_of_NOPE.LevelObjects
{
    #region Lucas
    public class Platform : LevelObject
    {
        public Rectangle sourceRectangle;
        

        public Platform(Texture2D texture, Vector2 position, Rectangle hitbox)
        {
           
            
            //Sets this texture to the texture it gets all the way from LevelLayout
            this.texture = texture;
            textureName = texture.Name;
            if (texture == null)
            {
              
            }
           
            
            

            this.hitbox = hitbox;
            //Needed for collision
            sourceRectangle = new Rectangle(sourceRectangle.X, sourceRectangle.Y, texture.Width, texture.Height);

            //Gets the position
            this.position = position;
        }
        public override void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD, GameTime gameTime, float angle)
        {
            base.Draw(spriteBatch, camera, GD, gameTime, angle);
        }
    }
    #endregion
}
