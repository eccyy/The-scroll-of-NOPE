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

        public Menu(int defaultMenuState)
        {
            menu = new List<MenuItem>();
            this.defaultMenuState = defaultMenuState;
        }
        public void AddItem(Texture2D itemTexture, int state)
        {
            float X = 0;
            float Y = 0 + currentHeight;

            currentHeight += itemTexture.Height + 20;

            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            menu.Add(temp);
        }
    }
}
