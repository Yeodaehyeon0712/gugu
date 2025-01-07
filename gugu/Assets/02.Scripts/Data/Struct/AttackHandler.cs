public readonly struct AttackHandler
{
    public readonly double Damage;
    public readonly uint CasterID;
    public readonly uint TargetID;
    public readonly bool IsCritical;

    public AttackHandler(uint casterID, uint targetID, double damage, bool isCritical)
    {
        CasterID = casterID;
        TargetID = targetID;
        Damage = damage;
        IsCritical = isCritical;
    }
}
