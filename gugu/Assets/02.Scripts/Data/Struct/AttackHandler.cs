public struct AttackHandler
{
    public readonly uint CasterID;
    public readonly uint TargetID;
    public readonly double Damage;
    public readonly bool IsCritical;

    public AttackHandler(uint casterID, uint targetID, double damage, bool isCritical)
    {
        CasterID = casterID;
        TargetID = targetID;
        Damage = damage;
        IsCritical = isCritical;
    }
}
