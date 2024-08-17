namespace Z3.DemoSkull.BattleSystem
{
    [System.Flags]
    public enum AttributePoint
    {
        HealthPoint,
        ManaPoint,
        StaminaPoint,
    }

    public enum DamageType
    {
        Default,
        Poison,
        Bleed,
        Burning,
        Electric,
        Dark,
    }
}