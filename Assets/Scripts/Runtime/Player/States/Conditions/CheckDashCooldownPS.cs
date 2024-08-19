using UnityEngine;
using Z3.GMTK2024.States;

namespace Z3.GMTK2024
{
    public class CheckDashCooldownPS : CharacterCondition
    {
        private float lastDashedTime = float.NegativeInfinity;

        public override bool CheckCondition()
        {
            var dashCooldown = Data.DashCooldown * Pawn.Size * Data.SizeData.DashCooldownMultiplier;
            return Time.time - lastDashedTime > dashCooldown;
        }

        public override void StopCondition()
        {
            base.StopCondition();
            // Dash Duration might be changed in Dash action?
            lastDashedTime = Time.time + Data.DashDuration;
        }
    }
}