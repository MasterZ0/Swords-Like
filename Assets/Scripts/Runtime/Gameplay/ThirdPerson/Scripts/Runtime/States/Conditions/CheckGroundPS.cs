namespace Z3.NodeGraph.Sample.ThirdPerson.Character.States
{
    public class CheckGroundPS : CharacterCondition
    {
        public override bool CheckCondition() => Physics.CheckGround();
    }
}
