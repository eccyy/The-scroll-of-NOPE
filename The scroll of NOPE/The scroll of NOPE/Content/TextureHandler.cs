using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace The_scroll_of_NOPE.Content
{
    class TextureHandler
    {

        ContentManager content;

        public TextureHandler(ContentManager content)
        {
            this.content = content;
        }
        Texture2D platform1
        {
            get
            {
                return content.Load<Texture2D>("images/Objects/platform1");
            }
        }
        Texture2D platform2
        {
            get
            {
                return content.Load<Texture2D>("images/Objects/platform2");
            }
        }
        Texture2D platform3
        {
            get
            {
                return content.Load<Texture2D>("images/Objects/platform3");
            }
        }
        Texture2D scrollOfNope
        {
            get
            {
                return content.Load<Texture2D>("images/Objects/Scroll");
            }
        }
        Texture2D heart
        {
            get
            {
                return content.Load<Texture2D>("images/Objects/pixelHeart");
            }
        }

        

    }
}
