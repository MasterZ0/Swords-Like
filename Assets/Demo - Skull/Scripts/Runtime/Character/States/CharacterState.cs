using UnityEngine;
using Z3.DemoSkull.Data;
using Z3.DemoSkull.Shared;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace Z3.DemoSkull.Character.States
{
    [NodeCategory(MenuPath.CharacterStates)]
    public abstract class CharacterCondition : ConditionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] protected Parameter<CharacterPawn> data;

        #region Character Components
        protected CharacterPhysics Physics => data.Value.Physics;
        protected PawnController Controller => data.Value.Controller;
        #endregion

        #region Settings
        protected CharacterData Data => data.Value.Data;
        #endregion
    }


    /// <summary>
    /// Note to developers: Please describe what this class does.
    /// </summary>

    [NodeCategory(MenuPath.CharacterStates)]
    public abstract class CharacterAction : ActionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] protected Parameter<CharacterPawn> data;

        #region Character Components
        protected CharacterCamera Camera => data.Value.Camera;
        protected CharacterPhysics Physics => data.Value.Physics;
        protected CharacterAnimator Animator => data.Value.Animator;
        protected PawnController Controller => data.Value.Controller;
        #endregion

        #region Settings
        protected CharacterData Data => data.Value.Data;
        #endregion


        protected sealed override void StartAction()
        {
            data.Value.EnterState(this);
            EnterState();
        }

        protected sealed override void StopAction()
        {
            data.Value.ExitState(this);
            ExitState();
        }

        // There could exist a history with the class and exit time
        //private bool GetPreviousState<T>() => (Agent.FSM.GetPreviousState() as ActionState).actionList.actions.Any(s => s.GetType() == typeof(T));

        protected virtual void EnterState() { }
        protected virtual void ExitState() { }

    }
}