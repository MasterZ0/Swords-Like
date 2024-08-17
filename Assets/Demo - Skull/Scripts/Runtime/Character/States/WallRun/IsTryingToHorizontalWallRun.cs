using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.Character.States
{
    public class IsTryingToHorizontalWallRun : CharacterCondition
    {
        [SerializeField] private Parameter<bool> running;
        [SerializeField] private Parameter<float> lastWallRun;

        public override bool CheckCondition()
        {
            return running.Value
                && Time.time - lastWallRun.Value > Data.HorizontalWallRunCooldown
                && Physics.CanHorizontalWallRun()
                && !Physics.CheckGround();
        }   
    }
}