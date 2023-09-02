using UnityEngine;

public interface ITargetable
{
    public void TakeDamage(int damage);
    public TargetType GetTargetType();
}
