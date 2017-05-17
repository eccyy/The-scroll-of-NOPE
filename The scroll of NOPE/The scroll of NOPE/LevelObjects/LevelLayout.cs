using System;
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
    //This class handles objects on the map, where they are and makes em appear at start
    public class LevelLayout : BaseClasses.PhysicalObject
    {
        //Lists
        List<Platform> platforms;
        List<Ground> grounds;
        List<HeartPickup> hearts;

        //Variables
        int heartRespRate = 5000;
        double HeartElapsed;

        //One time objects
        public TheScroll theScroll;

        //To be retrieved from another class
        public List<Platform> Platforms
        {
            get { return platforms; }
        }
        public List<Ground> Grounds
        {
            get { return grounds; }
        }
        public List<HeartPickup> Hearts
        {
            get { return hearts; }
        }

        //For the ground
        Array groundAmmount;
        Vector2 groundPosition;

        //Needed thing
        ContentManager content;

        public LevelLayout(ContentManager content)
        {
            this.content = content;
            
            //Spawns the neccessary objects
            platforms = new List<Platform>();
            grounds = new List<Ground>();
            hearts = new List<HeartPickup>();

            //Runs the methoods for each object
            scroll(content);
            platform(content);
            ground(content);
            heart(content, platforms);

        }

        public void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD, GameTime gameTime)
        {
            //Draws all objects, in the correct order
            foreach (Platform platform in platforms)
            {
                platform.Draw(spriteBatch, camera, GD, gameTime);
            }

            foreach (Ground ground in grounds)
            {
                ground.Draw(spriteBatch, camera, GD, gameTime);
            }
            
            foreach (HeartPickup heart in hearts)
            {
                heart.Draw(spriteBatch, camera, GD, gameTime);
            }

            theScroll.Draw(spriteBatch, camera, GD, gameTime);
        }
        
        //Counts how many hearts are on the map, max ammount is 15
        public int heartCounter;

        public void Update(GameTime gameTime)
        {
            #region PickupHearts
            HeartElapsed += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (HeartElapsed >= heartRespRate && heartCounter <= 15) //If heartElapsed reaches above 5000ms (heartRespRate), it spawns a new heart.
            {
                heart(content, platforms);
                HeartElapsed = 0;
                heartCounter++;
            }
            #endregion
        }

        //In order for objects to have separate textures i put them in methods so they get them by them selves when called.
        #region Methods For Drawing Objects
        public void platform(ContentManager content)
        {

            Random rng = new Random();
            //Positions for all platforms
            #region Layout for platforms, (Alot of numbers)
            List<Vector2> platformPositions = new List<Vector2>() {
                  new Vector2(0,-500), new Vector2(0,400), new Vector2(3900,-500), new Vector2(3900, 400) //Boundaries kinda
                , new Vector2(100, 300), new Vector2(200,100), new Vector2(200,400), new Vector2(0, 200), new Vector2(), new Vector2(), new Vector2() 
                , new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2() 
                , new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2() 
                , new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2() 
                , new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2() 
            };
            #endregion

            //Adds one platform for each Vector2 position in the positions list
            for (int n = 0; n < platformPositions.Count; n++)
            {
                
                int i = rng.Next(1, 4);

                if (i == 1) base.texture = content.Load<Texture2D>("images/Objects/platform1");
                if (i == 2) base.texture = content.Load<Texture2D>("images/Objects/platform2");
                if (i == 3) base.texture = content.Load<Texture2D>("images/Objects/platform3");

                hitbox = new Rectangle((int)platformPositions[n].X, (int)platformPositions[n].Y, texture.Width, texture.Height);
                platforms.Add(new Platform(texture, platformPositions[n], hitbox));
            }
        }

        public void ground(ContentManager content)
        {
            base.texture = content.Load<Texture2D>("images/FillerGround");

            //Set this number to the ammount of grounds you want I.E Length of the map, Each ground is 100px atm
            groundAmmount = new Array[40];

            //Sets position for start groundPlatform
            groundPosition.X = -100;
            groundPosition.Y = 450;

            //Adds a new platform after every platform for as many as you want 
            for (int n = 0; n < groundAmmount.Length; n++)
            {
                groundPosition.X += texture.Width;
                hitbox = new Rectangle((int)groundPosition.X, (int)groundPosition.Y, texture.Width, texture.Height);
                grounds.Add(new Ground(texture, groundPosition, hitbox));
            }
        }

        public void scroll(ContentManager content)
        {
            base.texture = content.Load<Texture2D>("images/Objects/Scroll");

            position = new Vector2(1000, 400);

            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            theScroll = new TheScroll(texture, position, hitbox);
        }
        
        public void heart(ContentManager content,List<Platform> platforms)
        {
            base.texture = content.Load<Texture2D>("images/Objects/pixelHeart");

            Random rng = new Random();

            //i is used to find which platform to spawn a heart at.
            int i = rng.Next(0, platforms.Count);

            //Sets the position so that it spawns above the platform of origin
            position = new Vector2(platforms[i].Position.X + 34, platforms[i].Position.Y - 32);

            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //May be temporary solution, but it works..
            int n = 1;
            if (n == 1)
            {
                hearts.Add(new HeartPickup(texture, position, hitbox));
                n++;
            }
        }
        #endregion
    }
    #endregion
}
