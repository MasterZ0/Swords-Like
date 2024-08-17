namespace Z3.DemoSkull.BattleSystem
{
    public interface IStatusOwner : IBattleEntity 
    {
        IStatusController Status { get; }
    }
}