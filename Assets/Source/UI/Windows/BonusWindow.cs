﻿using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    class BonusWindow : Window
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            CloseWithoutSound();
        }

        private void OnEnable()
        {
            _openButton.onClick.AddListener(Open);
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(Open);
            _closeButton.onClick.AddListener(Close);
        }
    }
}