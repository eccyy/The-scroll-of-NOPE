using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace The_scroll_of_NOPE.Menyer
{
    #region Anton
    static class GameElements
    {
        public enum _state { Run, Lobby, Menu,Quit,End };

        public static _state currentState;
        //update keyboard function
        static KeyboardState oldKeyboardState;

        static Menu menu;
        static MenuLobby lobbymenu;
        static End end;
        
        public static void LoadContent(ContentManager content, GameWindow window)
        {
            //menuSprite = content.Load<Texture2D>("images/menu/menu");
            //Centeres the image
            //menuPos.X = window.ClientBounds.Width - menuSprite.Width/2 + 80;
            //menuPos.Y = window.ClientBounds.Height - menuSprite.Height/2 + 20;

            //lobbySprite = content.Load<Texture2D>("images/menu/lobby");

            menu = new Menu((int)_state.Menu);

            //menu.AddItem(content.Load<Texture2D>("images/menu/start"), (int)_state.Run);
            menu.AddItem(content.Load<Texture2D>("images/menu/lobby"), (int)_state.Lobby);
            menu.AddItem(content.Load<Texture2D>("images/menu/exit"), (int)_state.Quit);

            lobbymenu = new MenuLobby((int)_state.Lobby);

            lobbymenu.AddItem(content.Load<Texture2D>("images/menu/start"), (int)_state.Run);
            lobbymenu.AddItem(content.Load<Texture2D>("images/menu/exit"), (int)_state.Menu);

            end = new End((int)_state.End);

            end.AddItem(content.Load<Texture2D>("images/menu/restart"), (int)_state.Menu);
            end.AddItem(content.Load<Texture2D>("images/menu/exit"), (int)_state.Quit);
        }
        //looping menustate
        public static _state MenuUpdate(GameTime gameTime)
        {
            //Keyboardstates
            //Logic for new and oldstate
            KeyboardState newKeyboardState = Keyboard.GetState();
            if (newKeyboardState != oldKeyboardState)
            {
                oldKeyboardState = newKeyboardState;
                if (newKeyboardState.IsKeyDown(Keys.Escape))
                {
                    return _state.Quit;
                }
            }

            //return _state.Menu;

            return (_state)menu.Update(gameTime);
        }
        //looping lobbystate
        public static _state LobbyUpdate(GameTime gameTime)
        {
            KeyboardState newKeyboardState = Keyboard.GetState();
            if (newKeyboardState != oldKeyboardState)
            {
                oldKeyboardState = newKeyboardState;
                if (newKeyboardState.IsKeyDown(Keys.Escape))
                {
                    return _state.Menu;
                }
            }
            return (_state)lobbymenu.Update(gameTime);
        }
        //looping runstate
        public static _state RunUpdate()
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            if (newKeyboardState != oldKeyboardState)
            {
                oldKeyboardState = newKeyboardState;
                if (newKeyboardState.IsKeyDown(Keys.Escape))
                {
                    return _state.Menu;
                }
            }
            return _state.Run;
        }

        public static _state EndUpdate(GameTime gameTime)
        {

            return (_state)end.Update(gameTime);
        }


        //draw method for menu
        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            //scaling
            menu.Draw(spriteBatch);
        }
        //draw method for lobby
        public static void LobbyDraw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw();
            lobbymenu.Draw(spriteBatch);
        }
        public static void EndDraw(SpriteBatch spriteBatch, SpriteFont font)
        {
            end.Draw(spriteBatch,font);
        }
    }
    #endregion
}
