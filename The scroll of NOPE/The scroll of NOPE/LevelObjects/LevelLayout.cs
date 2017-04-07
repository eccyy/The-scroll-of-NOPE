﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace The_scroll_of_NOPE.LevelObjects
{
    #region Lucas
    public class LevelLayout : BaseClasses.PhysicalObject
    {
        List<Platform> platforms;
        List<Ground> grounds;

        Array groundAmmount;
        Vector2 groundPosition;

        public LevelLayout(ContentManager content)
        {   
            //Creates a list of platforms
            platforms = new List<Platform>();
            grounds = new List<Ground>();

            //Creates all the objects needed
            platform(content);
            ground(content);  
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            //Draws each platform
            foreach (Platform platform in platforms)
            {
                platform.Draw(spriteBatch, camera);
            }
            foreach (Ground ground in grounds)
            {
                ground.Draw(spriteBatch, camera);
            }
        }

        //In order for objects to have separate textures i put them in methods so they get them by them selves when called.
        #region Methods For Drawing Objects
        public void platform(ContentManager content)
        {
            
            base.texture = content.Load<Texture2D>("images/FillerPlatform");
            
            //Positions for all platforms
            List<Vector2> platformPositions = new List<Vector2>() { new Vector2(50, 300), new Vector2(200, 200), new Vector2(400, 300), new Vector2(300, 400) };
            
            //Adds one platform for each Vector2 position in the positions list
            for (int n = 0; n < platformPositions.Count; n++)
            {
                platforms.Add(new Platform(texture, platformPositions[n]));
            }
        }

        public void ground(ContentManager content)
        {
            base.texture = content.Load<Texture2D>("images/FillerGround");

            //Set this number to the ammount of grounds you want
            groundAmmount = new Array[10];

            //Sets position for start groundPlatform
            groundPosition.X = -100;
            groundPosition.Y = 450;

            //Adds a new platform after every platform for as many as you want 
            for (int n = 0; n < groundAmmount.Length; n++)
            {
                groundPosition.X += texture.Width;
                grounds.Add(new Ground(texture, groundPosition));
            }
        }
        #endregion
    }
    #endregion
}
