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
    public class HeartPickup : LevelObject
    {
        public HeartPickup(Texture2D texture, Vector2 position, Rectangle hitbox)
        {
            //content = new Microsoft.Xna.Framework.Content.ContentManager(content.ServiceProvider, content.RootDirectory);

            this.texture = texture;
            this.hitbox = hitbox;
            this.position = position;
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD, GameTime gameTime, float angle)
        {
            base.Draw(spriteBatch, camera, GD, gameTime, angle);
        } 
    }
    #endregion
}
