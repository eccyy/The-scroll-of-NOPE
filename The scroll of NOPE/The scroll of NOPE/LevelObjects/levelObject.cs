using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace The_scroll_of_NOPE.LevelObjects
{
    #region Lucas
    public abstract class LevelObject : BaseClasses.PhysicalObject
    {
        ContentManager content;
        protected string textureName;

        protected Texture2D texture
        {
            get
            {
                return content.Load<Texture2D>(textureName);
            }
            set
            {
               
                
            }
        }
    }
    #endregion
}
