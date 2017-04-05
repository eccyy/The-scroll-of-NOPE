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

        public ANKA(int studentCount,Texture2D texture, Vector2 position, float maxSpeed, float health):base()
        {
            base.health = health;
            base.maxSpeed = maxSpeed;
            base.position = position;
            // Tar antalet spelare och räknar ut hur mycket starkare ANKA ska vara. Sedan ökar balanseringsvärden som health.
            BuffCalculator(studentCount);
            base.texture = texture;                       
        }

        protected override void Update()
        {
            base.Update();
                                           
        }
        
        protected override void AttackBasic()
        {

        }
        protected override void AttackH()
        {

        }
        protected override void AttackJ()
        {

        }
        protected override void AttackK()
        {

        }
        protected override void AttackL()
        {

        }
       


        private void BuffCalculator(int studentCount)
        {
            health *= (studentCount * 1f);

        }
    }
}
