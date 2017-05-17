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
    class Menu
    {
        List<MenuItem> menu;
        int selected = 0;

        float currentHeight = 0;
        double lastChange = 0;
        int defaultMenuState;
        bool isKeyReady = false;

        public Menu(int defaultMenuState)
        {
            menu = new List<MenuItem>();
            this.defaultMenuState = defaultMenuState;
        }

        public void AddItem(Texture2D itemTexture, int state)
        {
            float X = 290;
            float Y = 160 + currentHeight;

            currentHeight += itemTexture.Height + 20;

            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            menu.Add(temp);
        }

        public int Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyUp(Keys.Enter))
            {
                isKeyReady = true;
            }
            if (lastChange + 200 < gameTime.TotalGameTime.TotalMilliseconds)
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    selected++;
                    if (selected > menu.Count - 1)
                    {
                        selected = 0;
                    }

                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    selected--;
                    if (selected < 0)
                    {
                        selected = menu.Count - 1;
                    }

                }
                lastChange = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (isKeyReady)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    return menu[selected].State;
                }
            }
            return defaultMenuState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i< menu.Count; i++)
            {
                if(i == selected)
                {
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.BlueViolet);
                }
                else
                {
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.AntiqueWhite);
                }
            }
        }


    }
}
