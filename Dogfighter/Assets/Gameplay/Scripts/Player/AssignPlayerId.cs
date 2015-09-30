using UnityEngine;
using Gameplay.Scripts.Status;

namespace Gameplay.Scripts.Player
{
    public class AssignPlayerId : MonoBehaviour
    {
        public string PlayerId;

        private void Awake()
        {
            GameController.RegisterPlayer(PlayerId);

            transform.FindChild("Plane").GetComponent<Motion>().PlayerId = PlayerId;
            transform.FindChild("Plane").GetComponent<Firing>().PlayerId = PlayerId;

            transform.FindChild("Status").GetComponent<StatusManager>().PlayerId = PlayerId;

            Debug.Log(gameObject.name + ": assigning player ID of " + PlayerId);
        }
    }
}