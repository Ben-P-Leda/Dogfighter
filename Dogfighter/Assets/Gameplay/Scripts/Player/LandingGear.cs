using UnityEngine;

namespace Gameplay.Scripts.Player
{
    public class LandingGear : MonoBehaviour
    {
        public string ColliderTag;

        private void OnCollisionEnter(Collision collision)
        {
            ColliderTag = collision.collider.tag;
        }
    }
}
