﻿using Microsoft.Xna.Framework;
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
#region Jonatan
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
#endregion
        double elapsed;

        public override void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice GD, GameTime gameTime, float angle)
        {
            #region Lucas
            //Animations, this can be used by all classes that use this class, as long as they have a  - 
            // - Rectangle sourceRectangle with measurements for the sprites.
            elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            //Switches frame every 100 ms (should be decided by the using class)
            if (elapsed >= 100)
            {
                if (sourceRectangle.X >= sourceRectangle.Width * (texture.Width/sourceRectangle.Width - 1))
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


            zoomedDestination.Location += new Point(this.texture.Width / 2, this.texture.Height / 2);

            // Draw
            spriteBatch.Draw(texture, zoomedDestination, sourceRectangle, Color.White, angle, new Vector2(this.texture.Width / 2, this.texture.Height / 2), SpriteEffects.None, 0);

            #endregion
        }

    }
}
