using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses.Players
{
    public class Student : Player
    {
        public Student(Texture2D texture, Vector2 position, Vector2 speed)
        {
            this.texture  = texture;
            this.position = position;
            this.speed    = speed;

            this.projectiles = new List<Projectile>();
            this.health  = 1000;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }

    public class Mele
    {

    }

    public class Ranged
    {
        
    }
}
