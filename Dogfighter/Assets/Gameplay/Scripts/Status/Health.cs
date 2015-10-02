﻿using UnityEngine;
using Shared.Scripts;

namespace Gameplay.Scripts.Status
{
    public class Health : MonoBehaviour
    {
        private float _hitPoints;

        private Rect _textArea;
        private GUIStyle _guiStyle;

        private void Awake()
        {
            _hitPoints = 100.0f;
        }

        private void Start()
        {
            SetUpDisplay();
        }

        private void SetUpDisplay()
        {
            StatusManager manager = transform.parent.GetComponent<StatusManager>();

            _textArea = manager.ScaleToDisplay(100, 20, TextAnchor.LowerRight, 30.0f, 30.0f);
            _guiStyle = manager.GuiStyle;
            _guiStyle.alignment = TextAnchor.LowerRight;
        }

        private void OnGUI()
        {
            GUI.Label(_textArea, string.Format("Armour: {0}%", _hitPoints), _guiStyle);
        }

        public void TakeDamage(float damageValue, string sourceId)
        {
            _hitPoints -= damageValue;
            SoundEffectPlayer.PlaySound("ricochet", Random.Range(0.5f, 1.5f));
        }
    }
}
