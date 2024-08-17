using UnityEngine;
using System;
using Z3.GMTK2024.Inputs;
using static UnityEngine.InputSystem.InputAction;

namespace Z3.GMTK2024
{
    public class PawnController
    {
        public event Action<Vector2> OnMoveCamera;
        public event Action OnJumpPressed { add => jump.OnInputDown += value; remove => jump.OnInputDown -= value; }
        public event Action OnJumpReleased { add => jump.OnInputUp += value; remove => jump.OnInputUp -= value; }
        public event Action OnSprintPressed { add => sprint.OnInputDown += value; remove => sprint.OnInputDown -= value; }
        public event Action OnSprintReleased { add => sprint.OnInputUp += value; remove => sprint.OnInputUp -= value; }
        public event Action OnDashPressed { add => dash.OnInputDown += value; remove => dash.OnInputDown -= value; }
        public event Action OnDashReleased { add => dash.OnInputUp += value; remove => dash.OnInputUp -= value; }

        public bool IsMovePressed => Move != Vector2.zero;
        public Vector2 Move => controls.Player.Move.ReadValue<Vector2>();
        public Vector2 Look => controls.Player.Look.ReadValue<Vector2>();

        public bool IsJumpPressed => jump.IsPressed;
        public bool IsSprintPressed => sprint.IsPressed;

        private readonly InputButtonRegister jump;
        private readonly InputButtonRegister sprint;
        private readonly InputButtonRegister dash;
        private readonly InputButtonRegister primarySkill;
        private readonly InputButtonRegister secondarySkill;

        private readonly Controls controls;

        public PawnController()
        {
            // Controller
            controls = new Controls();

            jump = new InputButtonRegister(controls.Player.Jump);
            sprint = new InputButtonRegister(controls.Player.Sprint);
            dash = new InputButtonRegister(controls.Player.Dash);
            primarySkill = new InputButtonRegister(controls.Player.PrimarySkill);
            secondarySkill = new InputButtonRegister(controls.Player.SecondarySkill);

            controls.Player.Look.started += OnLook;
            controls.Enable();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void Dispose()
        {
            controls.Dispose();
        }

        private void OnLook(CallbackContext ctx) => OnMoveCamera?.Invoke(ctx.ReadValue<Vector2>());
    }
}