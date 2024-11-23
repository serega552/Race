using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Audio
{
    public class SoundSwitcher : MonoBehaviour
    {
        [SerializeField] private Sound[] _sounds;

        private readonly string _musicName = "Music";
        private readonly string _menuMusicName = "MenuMusic";
        private readonly float _minPitch = 0.8f;
        private readonly float _maxPitch = 2.5f;

        private void Start()
        {
            foreach (Sound sond in _sounds)
            {
                sond.Source = gameObject.AddComponent<AudioSource>();
                sond.Source.playOnAwake = false;
                sond.Source.clip = sond.Clip;
                sond.Source.volume = sond.Volume;
                sond.Source.pitch = sond.Pitch;
                sond.Source.loop = sond.Loop;
            }
        }

        public void Play(string sound)
        {
            Sound s = FindSound(sound);
            s?.Source.Play();
        }

        public void Stop(string sound)
        {
            Sound s = FindSound(sound);
            s?.Source.Stop();
        }

        public void Pause(string sound)
        {
            Sound s = FindSound(sound);
            s?.Source.Pause();
        }

        public void UnPause(string sound)
        {
            Sound s = FindSound(sound);
            s?.Source.UnPause();
        }

        public void ChangePitch(string sound, float count)
        {
            Sound s = FindSound(sound);

            if (s != null)
            {
                s.Source.pitch = Mathf.Clamp(s.Pitch += count * Time.deltaTime, _minPitch, _maxPitch);
                if (s.Source.pitch >= _maxPitch)
                    s.Pitch = _maxPitch;
                else if (s.Source.pitch <= _minPitch)
                    s.Pitch = _minPitch;
            }
        }

        public void ChangeMusicSound(string sound, float value)
        {
            Sound s = FindSound(sound);

            if (s != null)
                ChangeVolumeSound(s, value);
        }

        public void ChangeSounds(float value)
        {
            foreach (var sound in _sounds)
            {
                if (sound.Name != _musicName && sound.Name != _menuMusicName)
                    ChangeVolumeSound(sound, value);
            }
        }

        private void ChangeVolumeSound(Sound sound, float value)
        {
            sound.Source.volume = value;
            sound.Volume = value;
        }

        private Sound FindSound(string soundName)
        {
            return Array.Find(_sounds, item => item.Name == soundName);
        }
    }
}