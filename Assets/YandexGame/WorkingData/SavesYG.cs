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
        public int Money = 500;
        public int RecordScore = 0;
        public List<Skin> BoughtSkins;
        public Skin SelectedSkin;

        public SavesYG()
        {
        }
    }
}
