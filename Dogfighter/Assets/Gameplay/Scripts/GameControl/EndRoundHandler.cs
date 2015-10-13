using UnityEngine;
using System.Collections.Generic;

namespace Gameplay.Scripts.GameControl
{
    public class EndRoundHandler : MonoBehaviour
    {
        private List<GameObject> _players;
        private GameObject _postGameController;
        private bool _roundOver;

        private void Awake()
        {
            _players = new List<GameObject>();
            _postGameController = transform.FindChild("Post Game Controller").gameObject;
            _roundOver = false;
        }

        public void RegisterPlayer(GameObject player)
        {
            _players.Add(player);
        }

        private void Update()
        {
            if (!_roundOver) { CheckForRoundEnd(); }
        }

        private void CheckForRoundEnd()
        {
            if (RoundTimer.TimeRemaining <= 0.0f)
            {
                _roundOver = true;

                for (int i=0; i<_players.Count; i++) { _players[i].SetActive(false); }
                _postGameController.SetActive(true);
            }
        }
    }
}
