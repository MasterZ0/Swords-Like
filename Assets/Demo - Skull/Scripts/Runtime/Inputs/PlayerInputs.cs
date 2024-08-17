using System;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Z3.DemoSkull.Inputs
{
    public class PlayerInputs : BaseInput
    {
        public event Action<Vector2> OnMoveCamera;
        public event Action OnJumpPressed { add => Jump.OnInputDown += value; remove => Jump.OnInputDown -= value; }
        public event Action OnJumpReleased { add => Jump.OnInputUp += value; remove => Jump.OnInputUp -= value; }
        public event Action OnSprintPressed { add => Sprint.OnInputDown += value; remove => Sprint.OnInputDown -= value; }
        public event Action OnSprintReleased { add => Sprint.OnInputUp += value; remove => Sprint.OnInputUp -= value; }

        public event Action OnPrimarySkillPressed { add => PrimarySkill.OnInputDown += value; remove => PrimarySkill.OnInputDown -= value; }
        public event Action OnPrimarySkillReleased { add => PrimarySkill.OnInputUp += value; remove => PrimarySkill.OnInputUp -= value; }
        public event Action OnSecondarySkillPressed { add => SecondarySkill.OnInputDown += value; remove => SecondarySkill.OnInputDown -= value; }
        public event Action OnSecondarySkillReleased { add => SecondarySkill.OnInputUp += value; remove => SecondarySkill.OnInputUp -= value; }

        public event Action OnDash;

        public Vector2 Move => controls.Player.Move.ReadValue<Vector2>();
        public Vector2 Look => controls.Player.Look.ReadValue<Vector2>();

        public bool IsJumpPressed => Jump.IsPressed;
        public bool IsSprintPressed => Sprint.IsPressed;
        public bool IsPrimarySkillPressed => PrimarySkill.IsPressed;
        public bool IsSecondarySkillPressed => SecondarySkill.IsPressed;

        private readonly InputButtonRegister Jump;
        private readonly InputButtonRegister Sprint;
        private readonly InputButtonRegister PrimarySkill;
        private readonly InputButtonRegister SecondarySkill;

        public PlayerInputs(bool enable = true) : base(enable)
        {
            Jump = new InputButtonRegister(controls.Player.Jump);
            Sprint = new InputButtonRegister(controls.Player.Sprint);
            PrimarySkill = new InputButtonRegister(controls.Player.PrimarySkill);
            SecondarySkill = new InputButtonRegister(controls.Player.SecondarySkill);

            controls.Player.Dash.performed += OnDashPerformed;
            controls.Player.Look.started += OnLook;
        }

        private void OnDashPerformed(CallbackContext _) => OnDash?.Invoke();

        private void OnLook(CallbackContext ctx) => OnMoveCamera?.Invoke(ctx.ReadValue<Vector2>());
    }
}