using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using The_scroll_of_NOPE.BaseClasses.Players;

namespace The_scroll_of_NOPE.Menyer
{
    static class GameElements
    {
        public enum _state { Menu,Run,HighScore,Quit};

        public static _state currentState;

        static Texture2D menuSprite;
        static Vector2 menuPos;
        static Player player;
        
        public static void Initialize()
        {
            //do stuff
        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            //do stuff
        }

        public static _state MenuUpdate()
        {
            //do stuff
        }
    }
}
