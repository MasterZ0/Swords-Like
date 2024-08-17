using System;
using UnityEngine;
using Z3.DemoSkull.Inputs;
using Z3.DemoSkull.Character;

namespace Z3.DemoSkull.Player
{
    public class PlayerController : PawnController
    {
        public override event Action OnJumpPressed { add => playerInputs.OnJumpPressed += value; remove => playerInputs.OnJumpPressed -= value; }
        public override event Action OnJumpReleased { add => playerInputs.OnJumpReleased += value; remove => playerInputs.OnJumpReleased -= value; }
        public override event Action OnSprintPressed { add => playerInputs.OnSprintPressed += value; remove => playerInputs.OnSprintPressed -= value; }
        public override event Action OnSprintReleased { add => playerInputs.OnSprintReleased += value; remove => playerInputs.OnSprintReleased -= value; }

        public override event Action OnPrimarySkillPressed { add => playerInputs.OnPrimarySkillPressed += value; remove => playerInputs.OnPrimarySkillPressed -= value; }
        public override event Action OnPrimarySkillReleased { add => playerInputs.OnPrimarySkillReleased += value; remove => playerInputs.OnPrimarySkillReleased -= value; }
        public override event Action OnSecondarySkillPressed { add => playerInputs.OnSecondarySkillPressed += value; remove => playerInputs.OnSecondarySkillPressed -= value; }
        public override event Action OnSecondarySkillReleased { add => playerInputs.OnSecondarySkillReleased += value; remove => playerInputs.OnSecondarySkillReleased -= value; }

        public override event Action OnDash { add => playerInputs.OnDash += value; remove => playerInputs.OnDash -= value; }

        public override Vector2 Move => playerInputs.Move;
        public override Vector2 Look => playerInputs.Look; // TODO: Include Controller.Sensitivity
        public override bool IsJumpPressed => playerInputs.IsJumpPressed;
        public override bool IsSprintPressed => playerInputs.IsSprintPressed;
        public override bool IsPrimarySkillPressed => playerInputs.IsPrimarySkillPressed;
        public override bool IsSecondarySkillPressed => playerInputs.IsSecondarySkillPressed;

        private PlayerInputs playerInputs;

        protected override void Awake()
        {
            playerInputs = new PlayerInputs();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;

            base.Awake();
        }

        private void OnDestroy()
        {
            playerInputs.Dispose();
        }
    }
}