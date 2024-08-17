using System;
using UnityEngine.InputSystem;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Z3.DemoSkull.Inputs
{
    public class InputButtonRegister
    {
        public event Action OnInputDown;
        public event Action OnInputUp;

        public bool IsPressed { get; private set; }

        public InputButtonRegister(InputAction action)
        {
            action.started += SendInputDown;
            action.canceled += SendInputUp;
        }

        private void SendInputDown(CallbackContext _)
        {
            IsPressed = true;
            OnInputDown?.Invoke();
        }

        private void SendInputUp(CallbackContext _)
        {
            IsPressed = false;
            OnInputUp?.Invoke();
        }
    }
}