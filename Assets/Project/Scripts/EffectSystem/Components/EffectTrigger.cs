namespace Project.Scripts.EffectSystem.Components
{
    /// <summary>
    /// Specifies the trigger mode for extra effects in the effect handler.
    /// </summary>
    public enum EffectTrigger
    {
        Damage,
        TakeDamage,
        Heal,
        Stat,
        SelfDodge,
        Critical,
    }
}