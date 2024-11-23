using Cars;
using UnityEngine;
using YG;

namespace PlayerInputSystem
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private ControlButton _leftButton;
        [SerializeField] private ControlButton _rightButton;
        [SerializeField] private ControlButton _upButton;
        [SerializeField] private ControlButton _downButton;

        private bool _isMobile = false;
        
        public float HorizonInput {  get; private set; }
        public float VerticalInput {  get; private set; }

        private void Start()
        {
            _isMobile = YandexGame.EnvironmentData.isMobile;
        }

        public void UseCurrentInput()
        {
            if(_isMobile == false)
            {
                HorizonInput = EnterHorizonDescktop();
                VerticalInput = EnterVerticalDescktop();
            }
            else
            {
                HorizonInput = EnterMobileHorizon();
                VerticalInput = EnterMobileVertical();
            }
        }

        private float EnterMobileHorizon()
        {
            if (_leftButton.IsHold)
                return -1f;
            else if (_rightButton.IsHold)
                return 1f;
            else
                return 0f;
        }

        private float EnterMobileVertical()
        {
            if (_upButton.IsHold)
                return 1f;
            else if (_downButton.IsHold)
                return -1f;
            else
                return 0f;
        }

        private float EnterHorizonDescktop()
        {
            return Input.GetAxis("Horizontal");
        }

        private float EnterVerticalDescktop()
        {
            return Input.GetAxis("Vertical");
        }
    }
}