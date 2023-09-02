using UnityEngine;

public class AttackAction : Action
{
    public int Damage { get; protected set; }

    private void Start()
    {
        Range = 3;
        Damage = 2;
        TargetType = TargetType.Single;
    }

    private AttackAction(float range, int damage)
    {
        Range = range;
        Damage = damage;
    }

    public override void Do(ITargetable targetable)
    {
        targetable.TakeDamage(Damage);
    }

    public override Action Clone()
    {
        return new AttackAction(Range, Damage);
    }
}
