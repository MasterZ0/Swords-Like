namespace Z3.DemoSkull.Character.States
{
    public class IsClimbingHorizontally : CharacterCondition
    {
        public override bool CheckCondition()
        {
            float x = Controller.Move.x;
            return x != 0f && Physics.CanClimbHorizontal(x > 0);
        }
    }
}