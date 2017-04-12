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
        
        public virtual void Update()
        {
            position += speed;
            if(speed.X > 0 || speed.X < 0)
            {
                speed.X *= 0.9f;
            }
            if (speed.Y > 0 || speed.Y < 0)
            {
                speed.Y *= 0.9f;
            }
            
                              
        }
        public override void Draw(SpriteBatch sb, Camera camera)
        {

            #region Tommy
            Rectangle unzoomedDestination = new Rectangle(
                new Point((int)(position - camera.Position).X, (int)(position - camera.Position).Y),
                new Point(texture.Width, texture.Height));

            // Scale the size
            Rectangle zoomedDestination = unzoomedDestination;
            zoomedDestination.Inflate(
                zoomedDestination.Width * (camera.ZoomFactor - 1) / 2/*since it's for each side*/, //Zoom Width
                zoomedDestination.Height * (camera.ZoomFactor - 1) / 2); //Zoom Height

            // Scale the position
            zoomedDestination = new Rectangle(new Point((int)(zoomedDestination.X * camera.ZoomFactor), (int)(zoomedDestination.Y * camera.ZoomFactor)), zoomedDestination.Size);

            // Draw
            sb.Draw(texture, zoomedDestination, null/*Entire texture*/, Color.White, 0f, new Vector2(texture.Width / 2, texture.Width / 2), SpriteEffects.None, 0);
            #endregion
        }
          
    }
}
