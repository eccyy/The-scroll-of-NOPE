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
    static class GameElements
    {
        public enum _state { Menu,Run,HighScore,Quit};

        public static _state currentState;

        static Texture2D menuSprite;
        static Vector2 menuPos;
        
        public static void LoadContent(ContentManager content, GameWindow window)
        {
            //försöker att få en sprite till menuSprite
            menuSprite = content.Load<Texture2D>("images/menu/menu");

            menuPos.X = window.ClientBounds.Width/2 - menuSprite.Width/2;
            menuPos.Y = window.ClientBounds.Height/2 - menuSprite.Height/2;
        }

        public static _state MenuUpdate()
        {
            //Keyboardstates
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S))
                return _state.Run;
            if (keyboardState.IsKeyDown(Keys.H))
                return _state.HighScore;
            if (keyboardState.IsKeyDown(Keys.A))
                return _state.Quit;

            return _state.Menu;
        }

        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuSprite, menuPos, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
        }

    }
}
