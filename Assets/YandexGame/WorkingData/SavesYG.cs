using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        public int Money = 0;
        public int RecordScore = 0;
        public List<int> BoughtSkins = new List<int>();
        public int SelectedSkin;
        public float MusicValue = 0.3f;
        public float SoundValue = 0.5f;
        public int CountWatchedBonusAd = 0;
        public bool IsBonusUse = false;
        public List<float> AmountDailyProgreses = new List<float>();
        public int StartDailyTime;

        public SavesYG()
        {
        }
    }
}
