using Audio;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace SettingsGame
{
    public class VolumeAdjustment : MonoBehaviour
    {
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private SoundSwitcher _audioManager;

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

            _audioManager.ChangeSounds(_soundSlider.value);
            _audioManager.ChangeMusicSound("Music", _musicSlider.value);
            _audioManager.ChangeMusicSound("MenuMusic", _musicSlider.value);
        }

        private void ChangeMusicValue(float value)
        {
            _audioManager.ChangeMusicSound("Music", value);
            _audioManager.ChangeMusicSound("MenuMusic", value);
        }

        private void ChangeSoundValue(float value)
        {
            _audioManager.ChangeSounds(value);
        }
    }
}