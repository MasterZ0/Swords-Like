using UnityEngine;
using System;
using Z3.NodeGraph.Sample.ThirdPerson.Data;
using Z3.NodeGraph.Core;

namespace Z3.GMTK2024
{
    public enum CharacterEvent
    {
        Injury,
        Death
    }

    public sealed class CharacterPawn : MonoBehaviour
    {
        [Header("Character Controller")]
        [SerializeField] private CharacterData data;
        [SerializeField] private GraphRunner graphRunner;
        [SerializeField] private Animator animator;

        [Header("Sub Modules")]
        [SerializeField] private CharacterPhysics characterPhysics;
        [SerializeField] private CharacterCamera characterCamera;

        public event Action<CharacterEvent> OnCharacterEvent = delegate { };
        public CharacterData Data => data;
        public CharacterPhysics Physics => characterPhysics;
        public CharacterCamera Camera => characterCamera;
        public Animator Animator => animator;
        public PawnController Controller { get; private set; }

        private void Awake()
        {
            Controller = new PawnController();
            characterPhysics.Init(this);
            characterCamera.Init(this);
        }

        private void FixedUpdate()
        {
            graphRunner.ManualFixedUpdate();
            characterPhysics.Update();
            characterCamera.Update();
        }
    }
}