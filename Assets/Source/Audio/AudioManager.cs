using System;
using System.Collections;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private Sound[] _sounds;

        private string _musicName = "Music";
        private string _menuMusicName = "MenuMusic";

        private void Start()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

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
            Sound s = Array.Find(_sounds, item => item.Name == sound);

            if (s != null)
                s.Source.Play();
        }

        public void Stop(string sound)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);

            if (s != null)
                s.Source.Stop();
        }

        public void SlowPause(string sound)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);

            if (s != null)
                StartCoroutine(PauseTime(s));
        }

        public void SlowUnPause(string sound)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);

            if (s != null)
                StartCoroutine(UnpauseTime(s));
        }

        public void SlowPlay(string sound)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);

            if (s != null)
                StartCoroutine(PlayTime(s));
        }

        public IEnumerator PauseTime(Sound sound)
        {
            while (sound.Source.volume > 0.02f)
            {
                sound.Source.volume -= 0.05f;
                yield return new WaitForSeconds(0.1f);
            }

            sound.Source.Pause();
        }

        public IEnumerator UnpauseTime(Sound sound)
        {
            sound.Source.UnPause();

            while (sound.Source.volume < sound.Volume)
            {
                sound.Source.volume += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }

        public IEnumerator PlayTime(Sound sound)
        {
            sound.Source.Stop();
            sound.Source.Play();

            while (sound.Source.volume < sound.Volume)
            {
                sound.Source.volume += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void Pause(string sound)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);

            if (s != null)
                s.Source.Pause();
        }

        public void UnPause(string sound)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);

            if (s != null)
                s.Source.UnPause();
        }

        public void ChangePitch(string sound, float count)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);

            if (s != null)
            {
                s.Source.pitch = Mathf.Clamp(s.Pitch += count * Time.deltaTime, 0.8f, 2.5f);
                if (s.Source.pitch >= 2.5f)
                    s.Pitch = 2.5f;
                else if (s.Source.pitch <= 0.8f)
                    s.Pitch = 0.8f;
            }
        }

        public void ResetPitch(string sound)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);

            if (s != null)
            {
                s.Source.pitch = 1f;
                s.Pitch = 1f;
            }
        }

        public void ChangeValue(string sound, float value)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);

            if (s != null)
            {
                s.Source.volume = value;
                s.Volume = value;
            }
        }

        public void ChangeSounds(float value)
        {
            foreach (var sound in _sounds)
            {
                if (sound.Name != _musicName && sound.Name != _menuMusicName)
                {
                    sound.Source.volume = value;
                    sound.Volume = value;
                }
            }
        }
    }
}