using UnityEngine;

namespace Z3.DemoSkull.BattleSystem
{
    public interface IBattleEntity // Center and Head could be a humanoid interface
    {
        // Name? 
        /// <summary> Used to get components </summary>
        Transform Pivot { get; }
        Transform Head { get; }
        Transform Center { get; }
    }
}