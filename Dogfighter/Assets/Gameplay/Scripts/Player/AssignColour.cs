using UnityEngine;

namespace Gameplay.Scripts.Player
{
    public class AssignColour : MonoBehaviour
    {
        public Color Colour;

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                MeshRenderer renderer = transform.GetChild(i).GetComponent<MeshRenderer>();
                if (renderer != null) { renderer.material.color = Colour; }
            }
        }
    }
}
