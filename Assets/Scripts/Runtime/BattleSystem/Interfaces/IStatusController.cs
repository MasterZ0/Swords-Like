﻿using System;

namespace Z3.GMTK2024.BattleSystem
{
    /// <summary> Basic Status </summary>
    public interface IStatusController
    {
        IAttributes Attributes { get; }

        // OnAttack(melee/range) // Where: on enabled hitbox -> Used to: Enemy dodge
        event Action<DamageInfo> OnTakeDamage;
        event Action OnDeath;

        void TakeDamage(Damage damage);
        void DamageDealt(DamageInfo info);
        void SetInvincible(float duration);
    }
}