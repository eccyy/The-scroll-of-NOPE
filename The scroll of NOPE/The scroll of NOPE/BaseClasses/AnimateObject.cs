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

        protected Rectangle sourceRectangle;

        public AnimateObject()
        {

        }
        
        public virtual void Update()
        {
           // position += speed;
            if(speed.X > 0 || speed.X < 0)
            {
                speed.X *= 0.9f;
            }
            if (speed.Y > 0 || speed.Y < 0)
            {
                speed.Y *= 0.9f;
            }
            
                              
        }

        double elapsed;

        public override void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD, GameTime gameTime)
        {
            #region Lucas
            //Animations
            elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= 1000)
            {
                if (sourceRectangle.X >= sourceRectangle.Width * 3)
                {
                    sourceRectangle.X = 0;
                    elapsed = 0;
                }
                else
                {
                    sourceRectangle.X += sourceRectangle.Width;
                    elapsed = 0;
                }
            }
            #endregion

            #region Tommy
            //Set destination rectangle without scaling
            Rectangle unzoomedDestination = new Rectangle(
                new Point((int)(position - camera.Position).X, (int)(position - camera.Position).Y),
                new Point(sourceRectangle.Width, sourceRectangle.Height));

            // Store center of screen
            Point Center = new Point(GD.Viewport.X + GD.Viewport.Width / 2, GD.Viewport.Y + GD.Viewport.Height / 2);

            // Scale position and size
            Rectangle zoomedDestination = new Rectangle(
                (int)(unzoomedDestination.X * camera.ZoomFactor ),
                (int)(unzoomedDestination.Y * camera.ZoomFactor ),
                (int)(unzoomedDestination.Width * camera.ZoomFactor),
                (int)(unzoomedDestination.Height * camera.ZoomFactor));
            
            // New center in accordance with the scaling
            Point newCenter = new Point((int)(Center.X * camera.ZoomFactor), (int)(Center.Y * camera.ZoomFactor));

            // Restore center
            zoomedDestination.Location += Center - newCenter;



            // Draw
            spriteBatch.Draw(texture, zoomedDestination, sourceRectangle, Color.White, 0f, new Vector2(0,0), SpriteEffects.None, 0);

            #endregion
        }

    }
}
