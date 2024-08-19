using System;

namespace Z3.GMTK2024.BattleSystem
{
    public interface IAttributes
    {
        event Action OnUpdateStatus;
        int CurrentHP { get; }
        int MaxHP { get; }
    }
}