using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_scroll_of_NOPE.Menyer;
using System.Collections.Generic;
using The_scroll_of_NOPE.BaseClasses.Players;
using The_scroll_of_NOPE.Network;

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

        // DEBUG PURPOISE
        Projectile kula = new Projectile();
        Student1 testStudent;
        Camera camera;

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
            camera = new Camera(new Vector2(0,0), 1f);

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

            levelLayout = new LevelObjects.LevelLayout(Content);
            anka = new BaseClasses.Players.ANKA(1, Content.Load<Texture2D>("images/ANKA/ANKA"),new Vector2(50,50), 5,1000);
            testStudent = new Student1(Content.Load<Texture2D>("images/ANKA/ANKA"), new Vector2(300, 300), 7);
            // TODO: use this.Content to load your game content here
            collidables.Add(anka);
            collidables.Add(levelLayout);
            collidables.Add(testStudent);
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
                    GameElements.currentState = GameElements.LobbyUpdate();
                    break;
                case GameElements._state.Menu:
                    GameElements.currentState = GameElements.MenuUpdate();
                    break;
                case GameElements._state.Run:
                    //put Game update here
                    anka.Update();
                    testStudent.Update();
                    Collisions();

                    Point screenSize = GraphicsDevice.Viewport.Bounds.Size; // Gets the size of the screen
                    camera.Update(testStudent, new Vector2(screenSize.X, screenSize.Y)); // Updates camera
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
                    anka.Draw(spriteBatch, camera);
                    levelLayout.Draw(spriteBatch, camera);
                    testStudent.Draw(spriteBatch, camera);
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

            anka.Collision(collidables, levelLayout);

        }
        #endregion
    }
}
