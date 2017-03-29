using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace The_scroll_of_NOPE.LevelObjects
{
    class LevelLayout : BaseClasses.PhysicalObject
    {
        List<Platform> platforms;
        
        public LevelLayout(ContentManager content, List<Vector2> positions)
        {
            //Sets the texture for every platform
            base.texture = content.Load<Texture2D>("images/FillerPlatform");

            platforms = new List<Platform>();

            for (int n = 0; n < positions.Count; n++)
            {
                platforms.Add(new Platform(texture, positions[n]));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Platform platform in platforms)
            {
                
                platform.Draw(spriteBatch);
            }
        }
        //Draw metod som overridear den gammla.
        //Där man loopar platformar och ritar ut dem


    }
}
