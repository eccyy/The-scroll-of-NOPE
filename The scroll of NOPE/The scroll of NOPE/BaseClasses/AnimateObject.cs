using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses
{
   public class AnimateObject : PhysicalObject
    {
        protected Vector2 speed;
        
        public void Update()
        {
            position += speed;

                              
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(this.texture, position, Color.White);
        }
          
    }
}
