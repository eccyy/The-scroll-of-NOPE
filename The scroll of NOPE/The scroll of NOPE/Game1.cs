using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_scroll_of_NOPE.Menyer;
using System.Collections.Generic;
using The_scroll_of_NOPE.BaseClasses.Players;
using The_scroll_of_NOPE.Network;
using System;

namespace The_scroll_of_NOPE
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        LevelObjects.LevelLayout levelLayout;
        ANKA anka;
        LevelObjects.LevelEditor mapEditor;
        MouseState mouseState; 

        // For drawing text
        private SpriteFont font;
        private SpriteFont winFont;

        // DEBUG PURPOISE
        //Projectile kula = new Projectile();
        Student2 testStudent;
        Camera camera;

        bool debug, alreadyExecuted = false;


        Texture2D tmpTexture;

        List<BaseClasses.PhysicalObject> collidables = new List<BaseClasses.PhysicalObject>();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(new Vector2(0,0), 1.0f);
            this.IsMouseVisible = true;
            List<BaseClasses.PhysicalObject> collidables = new List<BaseClasses.PhysicalObject>();
            GameElements.currentState = GameElements._state.Menu;
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameElements.LoadContent(Content, Window);
            mapEditor = new LevelObjects.LevelEditor();

            // FOR DEBUG PUSPOSES
            tmpTexture = Content.Load<Texture2D>("images/ANKA/ANKA");

            //  levelLayout = new LevelObjects.LevelLayout(Content);
            levelLayout = mapEditor.LoadMap("defaultMap");
            if(levelLayout == null)
            {
                levelLayout = new LevelObjects.LevelLayout(Content);
            }

            anka = new BaseClasses.Players.ANKA(1, Content.Load<Texture2D>("images/ANKA/ANKA"),new Vector2(50,50), 5,10000);
            testStudent = new Student2(Content.Load<Texture2D>("images/Students/PlayerTemp"), new Vector2(0, 0), 7, Content.Load<Texture2D>("images/Students/tempProjectile"));
            
            // TODO: use this.Content to load your game content here
            collidables.Add(anka);
            collidables.Add(levelLayout);
            collidables.Add(testStudent);

            // For drawing text
            font = Content.Load<SpriteFont>("Text/Score");
            winFont = Content.Load<SpriteFont>("Text/Win");
            // for drawing whatever

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //   Exit();


            switch (GameElements.currentState)
            {
                case GameElements._state.Quit:
                    Exit();
                    break;
                case GameElements._state.Lobby:
                    //CreateNewLobbySession();
                    GameElements.currentState = GameElements.LobbyUpdate(gameTime);
                    break;
                case GameElements._state.Menu:
                    GameElements.currentState = GameElements.MenuUpdate(gameTime);
                    if (!alreadyExecuted)
                    {
                        base.Initialize();
                        alreadyExecuted = true;
                    }

                    break;
                case GameElements._state.Run:
                    //put Game update here
                    alreadyExecuted = false;
                    KeyboardState tempHandler = Keyboard.GetState();
                    if (tempHandler.IsKeyDown(Keys.D9))
                        camera.ZoomFactor *= 0.95f;
                    if (tempHandler.IsKeyDown(Keys.D0))
                        camera.ZoomFactor *= 1.05f;

                    // Turn debug on/off
                    if (tempHandler.IsKeyDown(Keys.Z))
                    {
                        if(debug == false)
                        {
                            debug = true;
                        }                            
                        else
                        {
                            debug = false;
                        }
                    }

                    levelLayout.Update(gameTime);
                    anka.Update();
                    testStudent.Update(camera);
                    Collisions();

                    //update mouse input
                   // mouseState = Mouse.GetState();
                    // If + button is clicked while debug is on a new platforn spawns
                    if (debug)
                    {
                        
                    }
                   
                           
                    Point screenSize = GraphicsDevice.Viewport.Bounds.Size; // Gets the size of the screen
                    camera.Update(anka, new Vector2(screenSize.X, screenSize.Y)); // Updates camera
                    GameElements.currentState = GameElements.RunUpdate();
                    break;
            }




            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch (GameElements.currentState)
            {
                case GameElements._state.Run:
                    //Draws the level design
                    anka.Draw(spriteBatch, camera, GraphicsDevice);
                    levelLayout.Draw(spriteBatch, camera, GraphicsDevice);
                    testStudent.Draw(spriteBatch, camera, GraphicsDevice);


                    #region jonatans Debug Stuff, safe to remove
                    if (debug)
                    {
                        // Drawing the colission angle for debug pusposes, may be used for other things later
                        spriteBatch.DrawString(font, "Collision angle: " + anka.tempPlayerAngle, new Vector2(50, 50), Color.White);
                        spriteBatch.DrawString(font, "HP " + anka.Health, new Vector2(50, 70), Color.White);

                        // Drawing ANKA hitbox
                        spriteBatch.Draw(tmpTexture, anka.Hitbox, Color.Blue);
                        spriteBatch.DrawString(font, "Anka hitbox", new Vector2(anka.Hitbox.X, anka.Hitbox.Y - 20), Color.Black);


                        //Drawing student hitbox
                        spriteBatch.Draw(tmpTexture, testStudent.Hitbox, Color.Blue);
                        spriteBatch.DrawString(font, "student hitbox", new Vector2(testStudent.Hitbox.X, testStudent.Hitbox.Y - 20), Color.Black);

                        //Drawing bullet hitbox
                        foreach (Projectile proj in testStudent.projectiles)
                        {
                            spriteBatch.Draw(tmpTexture, proj.Hitbox, Color.Purple);
                            spriteBatch.DrawString(font, "Bullet", new Vector2(proj.Hitbox.X, proj.Hitbox.Y), Color.Black);
                        }
                        // Scroll of nope hitbox
                        spriteBatch.Draw(tmpTexture, levelLayout.theScroll.Hitbox, Color.Blue);
                        spriteBatch.DrawString(font, "The scroll of NOPE", new Vector2(levelLayout.theScroll.Hitbox.X, levelLayout.theScroll.Hitbox.Y - 10), Color.Black);

                    }

                    #endregion

                    #region jonatans winconditions
                    //Check if ANKA is dead and if so the player won
                    if (anka.Health <= 0)
                    {
                        

                        Random rng = new Random();
                        float r = rng.Next(0, 100) / 100f;
                        float g = rng.Next(0, 100) / 100f;
                        float b = rng.Next(0, 100) / 100f;
                       


                        Color randomColor = new Color(r, g, b, 1);


                        spriteBatch.DrawString(winFont, "Congratulations you get to end the lession earlier!1!1111!!1", new Vector2(0, 250), randomColor);


                       
                    }

                    #endregion

                    break;
                case GameElements._state.Menu:
                    //Draws the menu sprite
                    GameElements.MenuDraw(spriteBatch);
                    break;
                case GameElements._state.Lobby:
                    GameElements.LobbyDraw(spriteBatch);
                    break;
                case GameElements._state.Quit:
                    Exit();
                    break;
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        #region JonatansKollisioner
        private void Collisions()
        {
            // Hoppas polymorfism funkar nu
            // En lista med alla objekt som kan kollidera.
            // collidables.Add(kula);

            anka.Collision(collidables);
            testStudent.Collision(collidables);
            // testStudent.Collision(collidables);


            // Bullet collisions. -1 means no bullets hit
            int index = anka.bulletCollision(testStudent.projectiles);
            // If anka was hit
            if (index != -1)
            {
                testStudent.projectiles.RemoveAt(index);
            }



        }
        #endregion

        #region JonatansMördarMaskin
        // Trying to somehow use this method to destroy instances of things when running in other places
        public void bulletDestroy()
        {

        }

        #endregion

        #region William, lobby thingys
        /// <summary>
        /// Creates a new network session for users to join.
        /// </summary>
        /// <param name="username">The username the user wants.</param>
        /// <paramm name="port">Port to host the server on</param>
        // TODO: Take in parameters from user
        private void CreateNewLobbySession(string username, int port)
        {
            SessionHost host = new SessionHost(username, port);
            host.CreateNewSession();

        }

        /// <summary>
        /// Join a network session lobby
        /// </summary>
        /// <param name="username">The username the user wants.</param>
        /// <param name="ip">IP address</param>
        /// <paramm name="port">Port to join the host on and to host the server on</param>
        // TODO: Take in parameters from user
        private void JoinLobbySession(string username, string ip, int port)
        {
            SessionNode user = new SessionNode(username, ip, port);
            user.JoinSession();
        }
        #endregion
    }
}
