using Audio;
using UnityEngine;

namespace UI.Windows
{
    [RequireComponent(typeof(CanvasGroup))]

    public class Window : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private ParticleSystem _effectButtonClick;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _effectButtonClick = GetComponentInChildren<ParticleSystem>();
        }

        public virtual void Open()
        {
            if (_animator != null)
                _animator.SetTrigger("open");

            AudioManager.Instance.Play("ClickOpen");
            _effectButtonClick?.Play();
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
        }

        public virtual void Close()
        {
            AudioManager.Instance.Play("ClickClose");
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