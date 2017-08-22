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
    public class End
    {
        List<MenuItem> end;

        int selected;
        int defaultState;
        bool isKeyReady = false;
        double lastChange;
        int currentHeight;

        SpriteFont font;

        public End(int defaultState)
        {
            end = new List<MenuItem>();
            this.defaultState = defaultState;
        }

        public void AddItem(Texture2D itemTexture, int state)
        {
            float X = 290;
            float Y = 160 + currentHeight;

            currentHeight += itemTexture.Height + 20;

            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            end.Add(temp);
        }

        public int Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyUp(Keys.Enter))
            {
                isKeyReady = true;
            }
            if (lastChange + 100 < gameTime.TotalGameTime.TotalMilliseconds)
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    selected++;
                    if (selected > end.Count - 1)
                    {
                        selected = 0;
                    }

                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    selected--;
                    if (selected < 0)
                    {
                        selected = end.Count - 1;
                    }

                }
                lastChange = gameTime.TotalGameTime.TotalMilliseconds;
            }
            if (isKeyReady && keyboardState.IsKeyDown(Keys.Enter))
            {

                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    isKeyReady = false;
                    return end[selected].State;
                }
            }

            return defaultState;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "Anka has won", new Vector2(0, 250), Color.AntiqueWhite);
            for (int i = 0; i < end.Count; i++)
            {
                if (i == selected)
                {
                    spriteBatch.Draw(end[i].Texture, end[i].Position, Color.BlueViolet);
                }
                else
                {
                    spriteBatch.Draw(end[i].Texture, end[i].Position, Color.AntiqueWhite);
                }
            }
        }
    }
    #endregion
}
