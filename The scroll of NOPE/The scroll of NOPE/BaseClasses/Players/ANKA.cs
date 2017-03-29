using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.BaseClasses.Players
{
    class ANKA : Player
    {
        float buffMultiplier;

        public ANKA(int studentCount,Texture2D texture, Vector2 position):base()
        {

            base.position = this.position;
            // Tar antalet spelare och räknar ut hur mycket starkare ANKA ska vara. Sedan ökar balanseringsvärden som health.
            BuffCalculator(studentCount);
            base.texture = texture;                       
        }

        public void Update()
        {
            base.Update();
            
        }
        
        protected override void BasicAttack()
        {

        }

        private void BuffCalculator(int studentCount)
        {
            health *= (studentCount * 1f);

        }
    }
}
