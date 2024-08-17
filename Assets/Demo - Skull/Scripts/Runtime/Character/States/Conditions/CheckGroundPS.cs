namespace Z3.DemoSkull.Character.States
{
    public class CheckGroundPS : CharacterCondition
    {
        public override bool CheckCondition() => Physics.CheckGround();
    }
}
