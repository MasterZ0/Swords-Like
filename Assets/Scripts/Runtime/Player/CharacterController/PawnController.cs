﻿using UnityEngine;
using System;
using Z3.GMTK2024.Inputs;
using static UnityEngine.InputSystem.InputAction;

namespace Z3.GMTK2024
{
    public class PawnController
    {
        public event Action<Vector2> OnMoveCamera;

        public event Action OnPrimarySkillPressed { add => primarySkill.OnInputDown += value; remove => primarySkill.OnInputDown -= value; }
        public event Action OnPrimarySkillReleased { add => primarySkill.OnInputUp += value; remove => primarySkill.OnInputUp -= value; }
        public event Action OnSecondarySkillPressed { add => secondarySkill.OnInputDown += value; remove => secondarySkill.OnInputDown -= value; }
        public event Action OnSecondarySkillReleased { add => secondarySkill.OnInputUp += value; remove => secondarySkill.OnInputUp -= value; }

        public event Action OnJumpPressed { add => jump.OnInputDown += value; remove => jump.OnInputDown -= value; }
        public event Action OnJumpReleased { add => jump.OnInputUp += value; remove => jump.OnInputUp -= value; }
        public event Action OnSprintPressed { add => sprint.OnInputDown += value; remove => sprint.OnInputDown -= value; }
        public event Action OnSprintReleased { add => sprint.OnInputUp += value; remove => sprint.OnInputUp -= value; }
        public event Action OnDashPressed { add => dash.OnInputDown += value; remove => dash.OnInputDown -= value; }
        public event Action OnDashReleased { add => dash.OnInputUp += value; remove => dash.OnInputUp -= value; }
        public event Action OnSizeIncreasePressed { add => sizeIncrease.OnInputDown += value; remove => sizeIncrease.OnInputDown -= value; }
        public event Action OnSizeIncreaseReleased { add => sizeIncrease.OnInputUp += value; remove => sizeIncrease.OnInputUp -= value; }
        public event Action OnSizeDecreasePressed { add => sizeDecrease.OnInputDown += value; remove => sizeDecrease.OnInputDown -= value; }
        public event Action OnSizeDecreaseReleased { add => sizeDecrease.OnInputUp += value; remove => sizeDecrease.OnInputUp -= value; }

        public bool IsMovePressed => Move != Vector2.zero;
        public bool IsSizeIncreasedPressed => sizeIncrease.IsPressed;
        public bool IsSizeDecreasePressed => sizeDecrease.IsPressed;
        public Vector2 Move => controls.Player.Move.ReadValue<Vector2>();
        public Vector2 Look => controls.Player.Look.ReadValue<Vector2>();

        public bool IsJumpPressed => jump.IsPressed;
        public bool IsSprintPressed => sprint.IsPressed;

        private readonly InputButtonRegister jump;
        private readonly InputButtonRegister sprint;
        private readonly InputButtonRegister dash;
        private readonly InputButtonRegister sizeIncrease;
        private readonly InputButtonRegister sizeDecrease;
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
            sizeIncrease = new InputButtonRegister(controls.Player.SizeIncrease);
            sizeDecrease = new InputButtonRegister(controls.Player.SizeDecrease);
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

        public void SetActive(bool enabled)
        {
            if (enabled)
            {
                controls.Enable();
            }
            else
            {
                controls.Disable();
            }
        }

        private void OnLook(CallbackContext ctx) => OnMoveCamera?.Invoke(ctx.ReadValue<Vector2>());
    }
}