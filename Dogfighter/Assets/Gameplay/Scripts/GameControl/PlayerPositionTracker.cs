using UnityEngine;
using System.Collections.Generic;
using Gameplay.Scripts.Status;
using Gameplay.Scripts.GameControl.Helpers;
using Gameplay.Scripts.Player;

namespace Gameplay.Scripts.GameControl
{
    public class PlayerPositionTracker : MonoBehaviour
    {
        private Dictionary<string, Radar> _playerRadars;
        private List<RadarData> _radarData;

        public void RegisterPlayer(GameObject player)
        {
            if (_playerRadars == null) { _playerRadars = new Dictionary<string, Radar>(); }
            if (_radarData == null) { _radarData = new List<RadarData>(); }

            _playerRadars.Add(player.tag, player.transform.FindChild("Status Manager").FindChild("Radar").GetComponent<Radar>());
            _radarData.Add(new RadarData(player.tag, player.transform.FindChild("Plane"), player.GetComponent<AssignColour>().Colour));
        }

        public void WireUpRadars()
        {
            foreach (KeyValuePair<string, Radar> kvp in _playerRadars)
            {
                kvp.Value.PlayersToTrack = CreateRadarDataForPlayer(kvp.Key);
            }
        }

        private RadarData[] CreateRadarDataForPlayer(string playerIdToCreateFor)
        {
            List<RadarData> data = new List<RadarData>();
            for (int i=0; i<_radarData.Count; i++)
            {
                if (_radarData[i].PlayerId != playerIdToCreateFor)
                {
                    data.Add(_radarData[i]);
                }
            }

            return data.ToArray();
        }
    }
}
