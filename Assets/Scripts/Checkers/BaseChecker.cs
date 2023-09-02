using UnityEngine;
using UnityEngine.Events;

public class BaseChecker : MonoBehaviour
{
    [SerializeField] private UnityEvent<IBase> _onReachBaseEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IBase>(out IBase damageableObject))
        {
            _onReachBaseEvent?.Invoke(damageableObject);
        }
    }
}
