using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

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

    private void ChangeMusicValue(float value)
    {
        AudioManager.Instance.ChangeValue("Music", value);
    }

    private void ChangeSoundValue(float value)
    {
        AudioManager.Instance.ChangeSounds(value);
    }
}
