using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] _sounds;

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
            sond.source = gameObject.AddComponent<AudioSource>();
            sond.source.playOnAwake = false;
            sond.source.clip = sond.clip;
            sond.source.volume = sond.volume;
            sond.source.pitch = sond.pitch;
            sond.source.loop = sond.loop;
        }
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
            s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
            s.source.Stop();
    }

    public void SlowPause(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
            StartCoroutine(PauseTime(s));
    } 
    
    public void SlowUnPause(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
            StartCoroutine(UnpauseTime(s));
    } 

    public void SlowPlay(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
            StartCoroutine(PlayTime(s));
    }

    public IEnumerator PauseTime(Sound sound)
    {
        while (sound.source.volume > 0.02f)
        {
            sound.source.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }

        sound.source.Pause();
    }

    public IEnumerator UnpauseTime(Sound sound)
    {
        sound.source.UnPause();

        while (sound.source.volume < sound.volume)
        {
            sound.source.volume += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator PlayTime(Sound sound)
    {
        sound.source.Stop();
        sound.source.Play();

        while (sound.source.volume < sound.volume)
        {
            sound.source.volume += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Pause(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
            s.source.Pause();
    }

    public void UnPause(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
            s.source.UnPause();
    }

    public void ChangePitch(string sound, float count)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
        {
            s.source.pitch = Mathf.Clamp(s.pitch += count * Time.deltaTime, 0.8f, 2.5f);
            if (s.source.pitch >= 2.5f)
                s.pitch = 2.5f;
            else if (s.source.pitch <= 0.8f)
                s.pitch = 0.8f;
        }
    }

    public void ResetPitch(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
        {
            s.source.pitch = 1f;
            s.pitch = 1f;
        }
    }

    public void ChangeValue(string sound, float value)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
        {
            s.source.volume = value;
            s.volume = value;
        }
    }

    public void ChangeSounds(float value)
    {
        foreach (var sound in _sounds)
        {
            if (sound.name != "Music" && sound.name != "MenuMusic")
            {
                sound.source.volume = value;
                sound.volume = value;
            }
        }
    }
}