using System;
using UnityEngine;

namespace Z3.GMTK2024.BattleSystem
{
    public class BasicAttributesController : IAttributes
    {
        public int MaxHP { get; protected set; }

        public int CurrentHP { get; private set; }

        public event Action OnUpdateStatus;

        public BasicAttributesController(int maxHeath = 1)
        {
            MaxHP = maxHeath;
            CurrentHP = maxHeath;
        }

        public void SetHP(int value) 
        {
            CurrentHP = value;
            OnUpdateStatus?.Invoke();
        } 

        public void SetMaxHP(int value) => MaxHP = value;

        #region Generic
        public void UpdateMaxHP(int value)
        {
            float percentage = this.HPPercentage();
            MaxHP = value;
            SetHP(Mathf.RoundToInt(MaxHP * percentage));
        }

        public void RecoveryAllPoints()
        {
            SetHP(MaxHP);
        }
        #endregion
    }
}