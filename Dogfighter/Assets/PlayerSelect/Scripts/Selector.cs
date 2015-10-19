using UnityEngine;
using Shared.Scripts.GameData;

namespace PlayerSelect.Scripts
{
    public class Selector : MonoBehaviour
    {
        private string _playerId;
        private SelectionHandler[] _avatars;
        private float _lastStepValue;
        private int _selected;
        private bool _buttonResetRequired;
        private bool _selectionMade;

        public Color Tint;

        private void Awake()
        {
            _playerId = transform.parent.tag;
;
            _selected = CurrentGame.Players[_playerId].SelectedAvatar;
            _selectionMade = false;

            _avatars = new SelectionHandler[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                _avatars[i] = transform.GetChild(i).GetComponent<SelectionHandler>();
                _avatars[i].Tint = Tint;

                if (CurrentGame.Players[_playerId].IsActive) { _avatars[i].Selected = (i == _selected); }
            }

            _lastStepValue = 0.0f;
            _buttonResetRequired = false;
        }

        private void Update()
        {
            if (CurrentGame.Players[_playerId].IsActive) 
            {
                CheckForButtonReset();

                if (!_selectionMade)
                {
                    HandleSelectionUpdate();
                    HandleSelectionConfirmation();
                }
            }
            else 
            { 
                HandleSelectorActivation(); 
            }
        }

        private void CheckForButtonReset()
        {
            if (Input.GetAxis(_playerId + " Fire1") <= 0.0f)
            {
                _buttonResetRequired = false;
            }
        }

        private void HandleSelectionUpdate()
        {
            float currentStepValue = Input.GetAxis(_playerId + " Horizontal");
            int step = 0;

            if ((currentStepValue < 0.0f) && (_lastStepValue >= 0.0f)) { step = -1; }
            if ((currentStepValue > 0.0f) && (_lastStepValue <= 0.0f)) { step = 1; }

            _selected = Mathf.Clamp(_selected + step, 0, _avatars.Length - 1);

            for (int i=0; i<_avatars.Length; i++)
            {
                _avatars[i].Selected = (i == _selected);
            }

            _lastStepValue = currentStepValue;

            if (step != 0.0f)
            {
                Debug.Log(_playerId + " changed to avatar " + _selected.ToString());
            }
        }

        private void HandleSelectionConfirmation()
        {
            if ((!_buttonResetRequired) && (Input.GetAxis(_playerId + " Fire1") > 0.0f))
            {
                _selectionMade = true;
                CurrentGame.Players[_playerId].SelectedAvatar = _selected;
                CurrentGame.Players[_playerId].Ready = true;
            }
        }

        private void HandleSelectorActivation()
        {
            if (Input.GetAxis(_playerId + " Fire1") > 0.0f)
            {
                CurrentGame.Players[_playerId].IsActive = true;
                _selected = CurrentGame.Players[_playerId].SelectedAvatar;
                _buttonResetRequired = true;
            }
        }
    }
}
