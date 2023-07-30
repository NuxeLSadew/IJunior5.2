using UnityEngine;

public class PathChecker : MonoBehaviour
{
    public bool IsNextCellDefined { get; private set; }
    public Quaternion RotationToNextCell { get; private set; }

    private void Awake()
    {
        RotationToNextCell = transform.rotation;
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsNextCellDefined)
        {
            return;
        }

        if (other.TryGetComponent<Base>(out _) 
            || (other.TryGetComponent<Pathway>(out Pathway pathway) && pathway.IsNextPathwayDefined))
        {
            IsNextCellDefined = true;
        }
    }
}
