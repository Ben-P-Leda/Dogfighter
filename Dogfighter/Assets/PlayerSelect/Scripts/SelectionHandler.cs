using UnityEngine;

namespace PlayerSelect.Scripts
{
    public class SelectionHandler : MonoBehaviour
    {
        private Transform _transform;
        private SpriteRenderer _clothesRenderer;

        private Color _selectedTint;
        private Color _unselectedTint;

        private float _highlighterStep;

        public bool Selected { get; set; }

        public Color Tint
        {
            set
            {
                _selectedTint = value;
                _unselectedTint = Color.Lerp(value, Color.black, 0.5f);
            }
        }

        private void Awake()
        {
            _transform = transform;
            _clothesRenderer = _transform.FindChild("Clothes").GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            UpdateHighlighterStep();
            UpdateTint();
            UpdateScale();
        }

        private void UpdateHighlighterStep()
        {
            float direction = Selected ? 1.0f : -1.0f;
            _highlighterStep = Mathf.Clamp01(_highlighterStep + Time.deltaTime * direction * Highlighter_Step_Size);
        }

        private void UpdateTint()
        {
            if (_selectedTint != null)
            {
                _clothesRenderer.color = Color.Lerp(_unselectedTint, _selectedTint, _highlighterStep);
            }
        }

        private void UpdateScale()
        {
            float scaler = 1.0f + _highlighterStep * Scale_Multiplier;
            _transform.localScale = new Vector3(scaler, scaler, 1.0f);
        }

        private const float Highlighter_Step_Size = 5.0f;
        private const float Scale_Multiplier = 0.2f;
    }
}
