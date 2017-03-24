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

        public ANKA(int studentCount,Texture2D texture):base()
        {
            // Tar antalet spelare och räknar ut hur mycket starkare ANKA ska vara. Sedan ökar balanseringsvärden som health.
            buffMultiplier = (float)(studentCount * 2f);
            health *= buffMultiplier;



        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
        private float buffCalculator(int studentCount)
        {
            return 0f;
        }
    }
}
