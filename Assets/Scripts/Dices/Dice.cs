using UnityEngine;

public class Dice : MonoBehaviour
{
    private const int SidesCount = 6;

    [SerializeField] private Side _side1;
    [SerializeField] private Side _side2;
    [SerializeField] private Side _side3;
    [SerializeField] private Side _side4;
    [SerializeField] private Side _side5;
    [SerializeField] private Side _side6;

    private Side[] _sides;
    private Side _currentSide;

    public int SideNumber { get; private set; }

    private void Awake()
    {
        _sides = new Side[SidesCount] { _side1, _side2, _side3, _side4, _side5, _side6};
    }

    public void Roll()
    {
        _currentSide = _sides[Random.Range(0, SidesCount)];
    }

    public void ApplySideAction(ICanDoAction canDoAction)
    {
        //canDoAction.ChangeAction();
    }
}