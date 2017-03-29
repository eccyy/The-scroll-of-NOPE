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
        List<Ground> grounds;
        
        public LevelLayout(ContentManager content, List<Vector2> platformPositions, List<Vector2> groundPositions)
        {
            //Sets the texture for every platform
            base.texture = content.Load<Texture2D>("images/FillerPlatform");
            base.texture = content.Load<Texture2D>("images/FillerGround");

            //Creates a list of platforms
            platforms = new List<Platform>();
            grounds = new List<Ground>();

            //Adds a platform for every position that exists
            for (int n = 0; n < platformPositions.Count; n++)
            {
                platforms.Add(new Platform(texture, platformPositions[n]));
            }
            for (int n = 0; n < groundPositions.Count; n++)
            {
                grounds.Add(new Ground(texture, groundPositions[n]));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws each platform
            foreach (Platform platform in platforms)
            {
                platform.Draw(spriteBatch);
            }
            foreach (Ground ground in grounds)
            {
                ground.Draw(spriteBatch);
            }
        }
    }
}
