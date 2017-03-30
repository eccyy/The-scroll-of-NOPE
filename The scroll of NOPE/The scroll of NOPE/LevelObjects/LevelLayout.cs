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

        List<Vector2> platformPositions;
        List<Vector2> groundPositions;
        
        public LevelLayout(ContentManager content /*, List<Vector2> platformPositions, List<Vector2> groundPositions*/)
        {   
            //Creates a list of platforms
            platforms = new List<Platform>();
            grounds = new List<Ground>();

            //All the positions for all objects
            platformPositions = new List<Vector2>() { new Vector2(50, 50), new Vector2(200, 50), new Vector2(50, 200), new Vector2(100, 220), new Vector2(150, 220), new Vector2(200, 200) };
            groundPositions = new List<Vector2>() { new Vector2(300, 50) };

            //Creates all the objects needed
            platform(content, platformPositions);
            ground(content, groundPositions);  
        }

        //In order for objects to have separate textures i put them in methods so they get them by them selves when called.
        #region Methods For Drawing Objects
        public void platform(ContentManager content, List<Vector2> platformPositions)
        {
            base.texture = content.Load<Texture2D>("images/FillerPlatform");
            //Adds one platform for each Vector2 position in the positions list
            for (int n = 0; n < platformPositions.Count; n++)
            {
                platforms.Add(new Platform(texture, platformPositions[n]));
            }
        }

        public void ground(ContentManager content, List<Vector2> groundPositions)
        {
            base.texture = content.Load<Texture2D>("images/FillerGround");

            //Adds one platform for each Vector2 position in the positions list
            for (int n = 0; n < groundPositions.Count; n++)
            {
                grounds.Add(new Ground(texture, groundPositions[n]));
            }
        }
        #endregion

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
