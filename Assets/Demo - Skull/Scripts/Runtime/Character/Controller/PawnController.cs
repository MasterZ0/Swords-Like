using UnityEngine;
using System;

namespace Z3.DemoSkull.Character
{
    public class PawnController : MonoBehaviour
    {
        [SerializeField] private CharacterPawn pawn;

        public virtual event Action<Vector2> OnMoveCamera;
        public virtual event Action OnJumpPressed;
        public virtual event Action OnJumpReleased;
        public virtual event Action OnSprintPressed;
        public virtual event Action OnSprintReleased;

        public virtual event Action OnPrimarySkillPressed;
        public virtual event Action OnPrimarySkillReleased;
        public virtual event Action OnSecondarySkillPressed;
        public virtual event Action OnSecondarySkillReleased;

        public virtual event Action OnDash;

        public bool IsMovePressed => Move != Vector2.zero;
        public virtual Vector2 Move { get; }
        public virtual Vector2 Look { get; }

        public virtual bool IsJumpPressed { get; }
        public virtual bool IsSprintPressed { get; }
        public virtual bool IsPrimarySkillPressed { get; }
        public virtual bool IsSecondarySkillPressed { get; }

        protected virtual void Awake()
        {
            pawn.Posses(this);
        }
    }
}