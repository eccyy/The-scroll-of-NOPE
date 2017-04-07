using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using The_scroll_of_NOPE;
using The_scroll_of_NOPE.BaseClasses.Players;

namespace The_scroll_of_NOPE
{
    class Camera
    {
        private Vector2 position;

        public Vector2 Position { get { return position; } }

        public Camera(Vector2 position)
        {
            this.position = position;
        }

        public void Update(Player player, Vector2 screenSize)
        {
            position = player.Position - screenSize / 2; // Centers the camera on the player (No acceleration or boundaries)
        }
    }
}
