using UnityEngine;

public class BaseDamager : MonoBehaviour
{
    [SerializeField] private int _damage;

    public void Attack(IBase @base)
    {
        @base.TakeDamage(_damage);
    }
}
