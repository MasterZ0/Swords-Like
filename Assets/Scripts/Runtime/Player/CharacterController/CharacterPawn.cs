using UnityEngine;
using System;
using Z3.GMTK2024.Data;
using Z3.NodeGraph.Core;
using Z3.GMTK2024.BattleSystem;

namespace Z3.GMTK2024
{
    public enum CharacterEvent
    {
        Injury,
        Death
    }

    public sealed class CharacterPawn : MonoBehaviour, IStatusOwner
    {
        [Header("Character Controller")]
        [SerializeField] private CharacterData data;
        [SerializeField] private GraphRunner graphRunner;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform center;

        [Header("Sub Modules")]
        [SerializeField] private CharacterPhysics characterPhysics;
        [SerializeField] private CharacterCamera characterCamera;
        [SerializeField] private CharacterUI characterUI;
        [SerializeField] private CharacterStatus characterStatus;

        public event Action<CharacterEvent> OnCharacterEvent = delegate { };
        public CharacterData Data => data;
        public CharacterPhysics Physics => characterPhysics;
        public CharacterCamera Camera => characterCamera;
        public Animator Animator => animator;
        public PawnController Controller { get; private set; }
        public CharacterStatus CharacterStatus => characterStatus;
        public CharacterUI CharacterUI => characterUI;

        public IStatusController Status => CharacterStatus;
        public Transform Pivot => transform;
        public Transform Center => center;

        private void Awake()
        {
            Controller = new PawnController();
            characterStatus.Init(this);
            characterPhysics.Init(this);
            characterCamera.Init(this);
            characterUI.Init(this);
        }

        private void OnEnable()
        {
            characterStatus.Reset();
            Controller.SetActive(true);
        }

        private void OnDisable()
        {
            Controller.SetActive(false);
        }

        private void OnDestroy()
        {
            Controller.Dispose();
        }

        private void FixedUpdate()
        {
            graphRunner.ManualFixedUpdate();
            characterPhysics.Update();
            characterCamera.Update();
            characterStatus.Update();
        }

        internal void SendEvent(CharacterEvent eventType) => OnCharacterEvent?.Invoke(eventType);
    }
}