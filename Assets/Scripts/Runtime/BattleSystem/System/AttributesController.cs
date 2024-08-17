using System;
using UnityEngine;

namespace Z3.GMTK2024.BattleSystem
{
    public class BasicAttributesController : IAttributes
    {
        public int MaxHP { get; protected set; } = 1;
        public int MaxMP { get; protected set; }
        public int MaxSP { get; protected set; }

        public int CurrentHP { get; private set; } = 1;
        public int CurrentMP { get; private set; }
        public int CurrentSP { get; private set; }

        public event Action OnUpdateStatus = delegate { };

        public void SetHP(int value) 
        {
            CurrentHP = value;
            OnUpdateStatus();
        } 
        public void SetMP(int value) 
        {
            CurrentMP = value;
            OnUpdateStatus();
        } 
        public void SetSP(int value) 
        {
            CurrentSP = value;
            OnUpdateStatus();
        }

        public void SetMaxHP(int value) => MaxHP = value;
        public void SetMaxMP(int value) => MaxMP = value;
        public void SetMaxSP(int value) => MaxSP = value;

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
            SetMP(MaxMP);
            SetSP(MaxSP);
        }
        #endregion
    }
}