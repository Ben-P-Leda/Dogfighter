﻿using UnityEngine;

namespace Gameplay.Scripts.Status
{
    public class ArmourGauge : MonoBehaviour
    {
        private Rect _textArea;
        private GUIStyle _guiStyle;
        private string _playerId;

        private void Start()
        {
            SetUpDisplay();
        }

        private void SetUpDisplay()
        {
            StatusManager manager = transform.parent.GetComponent<StatusManager>();

            _playerId = manager.PlayerId;
            _textArea = manager.ScaleToDisplay(100, 20, TextAnchor.LowerRight, 30.0f, 30.0f);
            _guiStyle = manager.GuiStyle;
            _guiStyle.alignment = TextAnchor.LowerRight;
        }

        private void OnGUI()
        {
            GUI.Label(_textArea, string.Format("Armour: {0}%", GameController.Health(_playerId)), _guiStyle);
        }
    }
}
