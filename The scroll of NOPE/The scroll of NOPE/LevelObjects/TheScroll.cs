using System;
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
    public class TheScroll : LevelObject
    {
        public TheScroll(Texture2D texture, Vector2 position, Rectangle hitbox, ContentManager content)
        {
            
            // If it is being deserialized 
            // TODO: Magic, set texture
            if (texture == null)
            {
                // Not implemented yet
                // TODO: IMplement a texture handler to use when serializing

            }
            else
            {
                this.texture = texture;
            }
           
            
            this.hitbox = hitbox;
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD, GameTime gameTime, float angle)
        {
            base.Draw(spriteBatch, camera, GD, gameTime, angle);
        }
    }
    #endregion
}
