using UnityEngine;

public class WayChecker : MonoBehaviour
{
    private Pathway _pathway;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Pathway>(out Pathway pathway))
        {
            _pathway = pathway;
        }
    }

    public bool TryGetNextCellPosition(out Vector3 position)
    {
        position = new Vector3();

        if (_pathway.IsNextPathwayDefined)
        {
            position = _pathway.NextPathwayPosition;
        }

        return _pathway.IsNextPathwayDefined;
    }
}
