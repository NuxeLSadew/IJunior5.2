using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TargetChecker : MonoBehaviour
{
    private List<ITargetable> _targetables;
    private BoxCollider _collider;
    
    public IReadOnlyList<ITargetable> Targetables => _targetables;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        SetScaleToOriginalValue();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ITargetable>(out ITargetable targetable))
        {
            _targetables.Add(targetable);
        }
    }

    public void ClearTargetsBuffer()
    {
        _targetables.Clear();
    }

    private void SetScaleToOriginalValue()
    {
        _collider.size = transform.InverseTransformVector(transform.localScale);
    }
}
