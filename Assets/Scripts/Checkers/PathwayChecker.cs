using UnityEngine;

public class PathwayChecker : MonoBehaviour
{
    private Pathway _findedPathway;

    public bool IsPathwayFinded => _findedPathway != null;
    public Pathway FindedPathway => _findedPathway;

    public bool CheckPathway()
    {
        bool hitted = false;

        if (IsPathwayFinded == false)
        {
            Vector3 raycastSizes = new Vector3(0.3f, 0.3f, 0.3f);
            Vector3 rayCastHitDirection = new Vector3(0, -1, 0);

            hitted = Physics.BoxCast(transform.position, raycastSizes, rayCastHitDirection, out RaycastHit hitInfo);
            if (hitted)
            {
                hitInfo.collider.gameObject.TryGetComponent<Pathway>(out _findedPathway);
            }
        }

        return IsPathwayFinded;
    }
}
