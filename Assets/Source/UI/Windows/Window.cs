using Audio;
using UnityEngine;

namespace UI.Windows
{
    [RequireComponent(typeof(CanvasGroup))]

    public class Window : MonoBehaviour
    {
        [SerializeField] protected SoundSwitcher AudioManager;
        [SerializeField] private CanvasGroup _canvasGroup;

        public virtual void Open()
        {
            AudioManager.Play("ClickOpen"); 
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
        }

        public virtual void Close()
        {
            AudioManager.Play("ClickClose");
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0f;
        }

        public virtual void OpenWithoutSound()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
        }

        public virtual void CloseWithoutSound()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0f;
        }

        public virtual void ControlOpenWithoutSound(float value)
        {
            _canvasGroup.alpha = value;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }
    }
}