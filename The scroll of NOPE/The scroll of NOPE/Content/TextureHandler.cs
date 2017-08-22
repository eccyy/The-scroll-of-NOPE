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
    // The class makes the program take up less processor power and memory due to not loading textures again and again from content but just copying them from here.
   public class TextureHandler
    {
        public enum texture { platform1,platform2,platform3,scroll,heart};

        ContentManager content;

        Texture2D platform1;

        Texture2D platform2;

        Texture2D platform3;

        Texture2D scrollOfNope;

        Texture2D heart;


        public TextureHandler(ContentManager content)
        {
            this.content = content;

            // Load all textures
            platform1 = content.Load<Texture2D>("images/Objects/platform1");
            platform2 = content.Load<Texture2D>("images/Objects/platform2");
            platform3 = content.Load<Texture2D>("images/Objects/platform3");
            scrollOfNope = content.Load<Texture2D>("images/Objects/scroll");
            heart = content.Load<Texture2D>("images/Objects/heart");

        }
       
     

        // Take the selected texture and return it
        public Texture2D getTexture(texture selectedTexture)
        {
            switch (selectedTexture)
            {
                case texture.platform1:
                    return platform1;
                case texture.platform2:
                    return platform2;
                case texture.platform3:
                    return platform3;
                case texture.scroll:
                    return scrollOfNope;
                case texture.heart:
                    return heart;
                default:
                    return null;
            }

        }


    }
#endregion
}
