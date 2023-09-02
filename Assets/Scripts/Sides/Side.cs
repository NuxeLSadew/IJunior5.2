using UnityEngine;

public class Side : MonoBehaviour
{
    [SerializeField] private Action _action;

    public Action Action => _action.Clone();
}
