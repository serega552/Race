using Audio;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace SettingsGame
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        private void Start()
        {
            Load();
        }

        private void OnEnable()
        {
            _soundSlider.onValueChanged.AddListener(ChangeSoundValue);
            _musicSlider.onValueChanged.AddListener(ChangeMusicValue);
        }

        private void OnDisable()
        {
            _soundSlider.onValueChanged.RemoveListener(ChangeSoundValue);
            _musicSlider.onValueChanged.RemoveListener(ChangeMusicValue);
        }

        public void Save()
        {
            YandexGame.savesData.SoundValue = _soundSlider.value;
            YandexGame.savesData.MusicValue = _musicSlider.value;
            YandexGame.SaveProgress();
        }

        public void Load()
        {
            _soundSlider.value = YandexGame.savesData.SoundValue;
            _musicSlider.value = YandexGame.savesData.MusicValue;

            AudioManager.Instance.ChangeSounds(_soundSlider.value);
            AudioManager.Instance.ChangeValue("Music", _musicSlider.value);
            AudioManager.Instance.ChangeValue("MenuMusic", _musicSlider.value);
        }

        private void ChangeMusicValue(float value)
        {
            AudioManager.Instance.ChangeValue("Music", value);
            AudioManager.Instance.ChangeValue("MenuMusic", value);
        }

        private void ChangeSoundValue(float value)
        {
            AudioManager.Instance.ChangeSounds(value);
        }
    }
}