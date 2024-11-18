using SettingsGame;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class SettingsWindow : Window
    {
        [SerializeField] private Button _openSettingsButton;
        [SerializeField] private Button _closeButton;

        private VolumeAdjustment _settings;
        private bool _isSettingsOpen = false;

        public bool IsSettingsOpen => _isSettingsOpen;

        private void Awake()
        {
            _settings = GetComponent<VolumeAdjustment>();
            CloseWithoutSound();
        }

        private void OnEnable()
        {
            _openSettingsButton.onClick.AddListener(Open);
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _openSettingsButton.onClick.RemoveListener(Open);
            _closeButton.onClick.RemoveListener(Close);
        }

        public override void Open()
        {
            base.Open();
            _isSettingsOpen = true;
        }

        public override void Close()
        {
            base.Close();
            _isSettingsOpen = false;
            _settings.Save();
        }
    }
}
