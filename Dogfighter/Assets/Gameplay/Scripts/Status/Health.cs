using UnityEngine;
using Shared.Scripts;
using Gameplay.Scripts.GameControl;

namespace Gameplay.Scripts.Status
{
    public class Health : MonoBehaviour
    {
        private float _hitPoints;
        private string _playerId;

        private Rect _displayArea;
        public Rect _barArea;
        private Color _barColour;

        public float StartingHitPoints;

        public Texture BaseImage;
        public Texture BarImage;

        private void Start()
        {
            SetUpDisplay();
        }

        private void SetUpDisplay()
        {
            StatusDisplayManager manager = transform.parent.GetComponent<StatusDisplayManager>();

            _displayArea = manager.ScaleToDisplay(BaseImage.width, BaseImage.height, TextAnchor.LowerCenter, 0.0f, 30.0f);

            _playerId = manager.PlayerId;
        }

        private void OnGUI()
        {
            GUI.color = _barColour;
            GUI.DrawTexture(_barArea, BarImage);
            GUI.DrawTexture(_displayArea, BaseImage);
        }

        public void TakeDamage(float damageValue, string sourceId)
        {
            if (_hitPoints > 0.0f)
            {
                _hitPoints -= damageValue;
                SetBarMetrics();

                SoundEffectPlayer.PlaySound("ricochet", Random.Range(0.5f, 1.5f));

                if (_hitPoints <= 0.0f)
                {
                    PlayerDeathHandler.TriggerDeath(_playerId, sourceId);
                }
            }
        }

        public void SetForNewLife()
        {
            _hitPoints = StartingHitPoints;
            SetBarMetrics();
        }

        private void SetBarMetrics()
        {
            _barArea = new Rect(_displayArea.x, _displayArea.y, (_displayArea.width / StartingHitPoints) * _hitPoints, _displayArea.height);

            if (_hitPoints > 50.0f) { _barColour = Color.Lerp(Color.yellow, Color.green, (_hitPoints - 50.0f) / 50.0f); }
            else { _barColour = Color.Lerp(Color.red, Color.yellow, _hitPoints / 50.0f); }
        }
    }
}
