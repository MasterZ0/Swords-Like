using System;
using UnityEngine;
using UnityEngine.UI;
using Z3.GMTK2024.BattleSystem;

namespace Z3.GMTK2024
{
    [Serializable]
    public sealed class CharacterUI : CharacterControllerComponent
    {
        [field: SerializeField] public Slider SizeChangeUI { get; private set; }
        [SerializeField] private Slider healthBar;

        public override void Init(CharacterPawn controller)
        {
            base.Init(controller);

            controller.CharacterStatus.Attributes.OnUpdateStatus += OnUpdateStatus; 
            OnUpdateStatus();
        }

        private void OnUpdateStatus()
        {
            healthBar.value = Controller.CharacterStatus.Attributes.HPPercentage();
        }
    }
}