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
{   //Anton
    static class GameElements
    {
        public enum _state { Menu,Run,Lobby,Quit};

        public static _state currentState;
        //update keyboard function
        static KeyboardState oldKeyboardState;

        static Texture2D menuSprite, lobbySprite;
        static Vector2 menuPos, lobbyPos;
        
        public static void LoadContent(ContentManager content, GameWindow window)
        {
            menuSprite = content.Load<Texture2D>("images/menu/menu");
            //Centeres the image
            menuPos.X = window.ClientBounds.Width - menuSprite.Width/2 + 80;
            menuPos.Y = window.ClientBounds.Height - menuSprite.Height/2 + 20;

            //lobbySprite = content.Load<Texture2D>("images/menu/lobby");
        }

        public static _state MenuUpdate()
        {
            //Keyboardstates
            //Logic for new and oldstate
            KeyboardState newKeyboardState = Keyboard.GetState();
            if (newKeyboardState != oldKeyboardState)
            {
                oldKeyboardState = newKeyboardState;
                if (newKeyboardState.IsKeyDown(Keys.S))
                {
                    return _state.Run;
                }
                if (newKeyboardState.IsKeyDown(Keys.H))
                {
                    return _state.Lobby;
                }
                if (newKeyboardState.IsKeyDown(Keys.Escape))
                {
                    //if()
                    return _state.Quit;
                }
            }

            return _state.Menu;
        }

        public static _state LobbyUpdate()
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
            return _state.Lobby;
        }

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


        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            //scaling
            spriteBatch.Draw(menuSprite, menuPos, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
        }

        public static void LobbyDraw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw();
        }
    }
}
