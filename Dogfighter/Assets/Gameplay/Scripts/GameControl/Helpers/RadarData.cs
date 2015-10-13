using UnityEngine;
using Gameplay.Scripts.Status;

namespace Gameplay.Scripts.GameControl.Helpers
{
    public class RadarData
    {
        private Transform _transform;

        public string PlayerId { get; private set; }
        public Vector3 Position { get { return _transform.position; } }
        public Color Colour { get; private set; }
        public bool Visible { get; set; }

        public RadarData(string playerId, Transform transform, Color colour)
        {
            PlayerId = playerId;
            Colour = new Color(colour.r, colour.g, colour.b, 1.0f);
            Visible = true;

            _transform = transform;
        }
    }
}
