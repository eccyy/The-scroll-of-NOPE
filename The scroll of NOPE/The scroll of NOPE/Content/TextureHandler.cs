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
#region Jonatan
    // Class that loads all textures and makes them acceecible from the getTexture method. This was supposed to be a fix for serialization issues.
    class TextureHandler
    {
        public enum texture { platform1,platform2,platform3,scroll,heart};

        ContentManager content;

        public TextureHandler(ContentManager content)
        {
            this.content = content;
        }
        Texture2D platform1;
       
        Texture2D platform2;
     
        Texture2D platform3;

        Texture2D scrollOfNope;

        Texture2D heart;
     


        public Texture2D getTexture(texture selectedTexture)
        {
            switch (selectedTexture)
            {
                case texture.platform1:
                    return content.Load<Texture2D>("images/Objects/platform1");
                case texture.platform2:
                    return content.Load<Texture2D>("images/Objects/platform2");
                case texture.platform3:
                    return content.Load<Texture2D>("images/Objects/platform3");
                case texture.scroll:
                    return content.Load<Texture2D>("images/Objects/scroll");
                case texture.heart:
                    return content.Load<Texture2D>("images/Objects/heart");
                default:
                    return null;
            }

        }


    }
#endregion
}
