using UnityEngine;
using Z3.GMTK2024.Data;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace Z3.GMTK2024.States
{
    [NodeCategory(Categories.Samples + "/Third Person")]
    public abstract class CharacterCondition : ConditionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] protected Parameter<CharacterPawn> data;

        protected CharacterPawn Pawn => data.Value;
        protected CharacterPhysics Physics => data.Value.Physics;
        protected PawnController Controller => data.Value.Controller;
        protected CharacterData Data => data.Value.Data;
    }

    [NodeCategory(Categories.Samples + "/Third Person")]
    public abstract class CharacterAction : ActionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] protected Parameter<CharacterPawn> data;

        protected CharacterPawn Pawn => data.Value;
        protected CharacterCamera Camera => data.Value.Camera;
        protected CharacterPhysics Physics => data.Value.Physics;
        protected Animator Animator => data.Value.Animator;
        protected PawnController Controller => data.Value.Controller;
        protected CharacterData Data => data.Value.Data;
        protected CharacterUI UI => data.Value.CharacterUI;
    }
}