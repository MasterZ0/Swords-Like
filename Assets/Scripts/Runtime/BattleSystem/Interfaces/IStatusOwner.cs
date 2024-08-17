namespace Z3.GMTK2024.BattleSystem
{
    public interface IStatusOwner : IBattleEntity 
    {
        IStatusController Status { get; }
    }
}