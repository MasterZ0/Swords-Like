using System;
using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.Character.States
{
    [NodeDescription("Check the current state of the input")]
    public class CheckControllerMovePS : CharacterCondition
    {
        [Flags]
        public enum Axis2D
        {
            Up = 1,
            Down = 2,
            Left = 4,
            Right = 8,
        }

        [SerializeField] private Parameter<Axis2D> moveController;

        public override string Info => $"CheckControllerMove: {moveController}";

        public override bool CheckCondition()
        {
            Vector2 move = Controller.Move;

            if (move.x < 0 && moveController.Value.HasFlag(Axis2D.Left))
            {
                return true;
            }

            if (move.x > 0 && moveController.Value.HasFlag(Axis2D.Right))
            {
                return true;
            }

            if (move.y < 0 && moveController.Value.HasFlag(Axis2D.Down))
            {
                return true;
            }

            if (move.y > 0 && moveController.Value.HasFlag(Axis2D.Up))
            {
                return true;
            }

            return false;
        }
    }
}