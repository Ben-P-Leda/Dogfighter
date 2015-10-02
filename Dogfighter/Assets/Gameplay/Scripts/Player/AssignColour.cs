using UnityEngine;

namespace Gameplay.Scripts.Player
{
    public class AssignColour : MonoBehaviour
    {
        public Color Colour;

        private void Awake()
        {
            AssignColourToModel();
        }

        private void AssignColourToModel()
        {
            Transform modelTransform = transform.FindChild("Plane").FindChild("Plane Model");

            for (int i = 0; i < modelTransform.childCount; i++)
            {
                MeshRenderer renderer = modelTransform.GetChild(i).GetComponent<MeshRenderer>();
                if (renderer != null) { renderer.material.color = Colour; }
            }
        }
    }
}
