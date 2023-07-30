using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _damage;

    public void Attack(IDamageable damageableObject)
    {
        damageableObject.TakeDamage(_damage);
    }
}
