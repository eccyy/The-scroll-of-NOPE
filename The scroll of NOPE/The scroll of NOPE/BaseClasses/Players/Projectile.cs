using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_scroll_of_NOPE.BaseClasses.Players
{
   public class Projectile : AnimateObject
    {
        public Projectile(Vector2 position, Texture2D texture, Vector2 speed) : base()
        {
            this.texture = texture;
            this.position = position;
            this.hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.speed = speed;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
