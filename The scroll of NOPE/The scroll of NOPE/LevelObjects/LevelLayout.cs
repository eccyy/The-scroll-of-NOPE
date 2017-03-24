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
    class LevelLayout
    {
        List<Platform> platforms;
        Texture2D texture;

        
        public LevelLayout(ContentManager content, List<Vector2> positions)
        {
            //Sets the texture for every platform
            this.texture = content.Load<Texture2D>("");

            platforms = new List<Platform>();

            for (int n = 0; n < positions.Count; n++)
            {
                platforms.Add(new Platform(texture, positions[n]));
            }
        }


    }
}
