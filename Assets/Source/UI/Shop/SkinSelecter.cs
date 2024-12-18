using Cars;
using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace UI.Shop
{
    public class SkinSelecter : MonoBehaviour
    {
        private readonly List<Skin> _boughtSkins = new List<Skin>();

        [SerializeField] private Skin _firstSkin;

        private Skin _selectedSkin;

        public CarMovement Movement { get; private set; }

        public event Action<CarMovement> SKinChanging;

        public void InitFirstSkin()
        {
            _selectedSkin = _firstSkin;
            _selectedSkin.Unlock();
            _selectedSkin.ChangeStatus();
            AddSkin(_selectedSkin);
            InitSkin();
        }

        public void AddSkin(Skin skin)
        {
            if (_boughtSkins.Contains(skin) == false)
            {
                _boughtSkins.Add(skin);
            }
        }

        public void SelectSkin(Skin skin)
        {
            if (skin != _selectedSkin)
            {
                if (_selectedSkin != null)
                    _selectedSkin.ChangeStatus();

                _selectedSkin = skin;

                _selectedSkin.ChangeStatus();
                InitSkin();
            }
        }

        private void InitSkin()
        {
            foreach (Skin skin in _boughtSkins)
            {
                if (skin.IsSelected == false)
                {
                    skin.TurnOffSkin();
                }
            }

            Movement = _selectedSkin.GetView();
            SKinChanging?.Invoke(Movement);

            Save();
        }

        private void Save()
        {
            for (int i = 0; i < _boughtSkins.Count; i++)
            {
                if (YandexGame.savesData.BoughtSkins.Contains(_boughtSkins[i].Id) == false)
                    YandexGame.savesData.BoughtSkins.Add(_boughtSkins[i].Id);
            }

            YandexGame.savesData.SelectedSkin = _selectedSkin.Id;
            YandexGame.SaveProgress();
        }
    }
}