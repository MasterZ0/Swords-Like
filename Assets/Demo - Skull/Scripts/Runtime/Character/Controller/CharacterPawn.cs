using UnityEngine;
using System;
using Z3.DemoSkull.Data;
using Z3.NodeGraph.Core;
using Z3.DemoSkull.Character.States;
using Z3.DemoSkull.BattleSystem;

namespace Z3.DemoSkull.Character
{
    public enum CharacterEvent
    {
        Injury,
        Death
    }

    public sealed class CharacterPawn : MonoBehaviour, IBattleEntity
    {
        [Header("Character Controller")]
        [SerializeField] private GraphRunner runner;
        [SerializeField] private CharacterData data;
        [SerializeField] private Transform head;
        [SerializeField] private Transform center;

        [Header("Sub Modules")]
        [SerializeField] private CharacterPhysics characterPhysics;
        [SerializeField] private CharacterAnimator characterAnimator;
        [SerializeField] private CharacterCamera characterCamera; // Player only
        [SerializeField] private bool debug;

        public event Action<CharacterEvent> OnCharacterEvent = delegate { };
        public CharacterData Data => data;
        public CharacterPhysics Physics => characterPhysics;
        public CharacterAnimator Animator => characterAnimator;
        public CharacterCamera Camera => characterCamera;
        public PawnController Controller { get; private set; }

        public Transform Pivot => Physics.Transform;
        public Transform Head => head;
        public Transform Center => center;

        private void Awake()
        {
            characterPhysics.Init(this);
            characterAnimator.Init(this);
            characterCamera.Init(this);
        }

        private void FixedUpdate()
        {
            // TODO: Implement manual update
            //runner.UpdateGraph();
            characterPhysics.Update();
            characterCamera.Update();
            characterAnimator.Update();
        }

        public void Posses(PawnController characterController)
        {
            Controller = characterController;
        }

        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
                return;

            characterPhysics.DrawGizmos();
        }

        internal void EnterState(CharacterAction characterAction)
        {
            if (!debug)
                return;

            Debug.Log(characterAction);
        }

        internal void ExitState(CharacterAction characterAction)
        {
            if (!debug)
                return;

            Debug.Log(characterAction);
        }
    }
}