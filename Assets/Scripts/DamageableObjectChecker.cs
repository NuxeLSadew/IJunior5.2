using UnityEngine;
using UnityEngine.Events;

public class DamageableObjectChecker : MonoBehaviour
{
    [SerializeField] private UnityEvent<IDamageable> _onReachDamageableObjectEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageableObject))
        {
            _onReachDamageableObjectEvent?.Invoke(damageableObject);
        }
    }
}
