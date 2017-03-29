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
            if(speed.X > 0 && speed.X > 5)
            {
                speed.X *= 0.9f;
            }
            if (speed.Y > 0 && speed.Y > 5)
            {
                speed.Y *= 0.9f;
            }
            
                              
        }
        public override void Draw(SpriteBatch sb)
        {
            Rectangle size = new Rectangle(0, 0, texture.Width, texture.Height);
            sb.Draw(this.texture, position,size,Color.Wheat,0f,position,0.25f,SpriteEffects.None,0) ;
        }
          
    }
}
