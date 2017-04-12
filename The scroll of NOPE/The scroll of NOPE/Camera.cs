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
    #region Tommy
    public class Camera
    {
        private Vector2 position;
        private float zoomFactor;

        public Vector2 Position { get { return position; } }
        public float ZoomFactor { get { return zoomFactor; } }

        /// <summary>
        /// Creates a camera with a default zoomFactor of 1
        /// </summary>
        /// <param name="position"></param>
        public Camera(Vector2 position)
        {
            this.position = position;
            this.zoomFactor = 1f;
        }

        /// <summary>
        /// Creates a camera at specified position with specified zoomFactor
        /// </summary>
        /// <param name="position">The camera's position on the map</param>
        /// <param name="zoomFactor">The factor of which to zoom from the default state. A value larger than 1 gets close up and personal, whereas a value smaller than 1 respects your personal space</param>
        public Camera(Vector2 position, float zoomFactor)
        {
            this.position = position;
            this.zoomFactor = zoomFactor;
        }

        public void Update(Player player, Vector2 screenSize)
        {
            position = player.Position - screenSize / 2; // Centers the camera on the player (No acceleration or boundaries)
        }
    }
#endregion
}
