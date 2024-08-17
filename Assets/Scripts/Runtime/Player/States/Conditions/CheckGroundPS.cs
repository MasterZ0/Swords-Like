namespace Z3.GMTK2024.States
{
    public class CheckGroundPS : CharacterCondition
    {
        public override bool CheckCondition() => Physics.CheckGround();
    }
}
