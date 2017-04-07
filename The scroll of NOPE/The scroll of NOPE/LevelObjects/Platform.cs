using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_scroll_of_NOPE.LevelObjects
{
    #region Lucas
    class Platform : LevelObject
    {
        public Rectangle sourceRectangle;
        public Platform(Texture2D texture, Vector2 position)
        {
            //Sets this texture to the texture it gets all the way from LevelLayout
            this.texture = texture;
            //Needed for collision
            sourceRectangle = new Rectangle(sourceRectangle.X, sourceRectangle.Y, texture.Width, texture.Height);

            //Gets the position
            this.position = position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
    #endregion
}
