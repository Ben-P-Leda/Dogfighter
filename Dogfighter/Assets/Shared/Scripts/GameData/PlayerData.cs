namespace Shared.Scripts.GameData
{
    public class PlayerData
    {
        public bool IsActive { get; set; }
        public int SelectedAvatar { get; set; }
        public bool Ready { get; set; }
        public int PlayerKills { get; set; }
        public int TargetKills { get; set; }
        public int Deaths { get; set; }

        public void Reset()
        {
            IsActive = false;
            Ready = false;
            PlayerKills = 0;
            TargetKills = 0;
            Deaths = 0;
        }

        public int TotalScore
        {
            get
            {
                return PlayerKills + (TargetKills * Target_Kill_Multiplier);
            }
        }

        private int Target_Kill_Multiplier = 2;
    }
}
