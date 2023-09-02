using UnityEngine;

[RequireComponent(typeof(TargetFinder))]
public class Tower : MonoBehaviour, ICanDoAction, ITargetable
{
    private const TargetType Type = TargetType.Tower;

    [SerializeField] private int _maxHealth;

    private int _currentHealth;
    private Dice _dice;
    private Action _currentAction;
    private TargetFinder _targetFinder;
    private ITargetable _target;

    public TargetType TargetType => TargetType.Tower;
    public TargetPriority TargetPriority { get; private set; }

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > _maxHealth)
            {
                value = _maxHealth;
            }

            _currentHealth = value;
        }
    }

    private void Start()
    {
        _dice = GetComponentInChildren<Dice>();
        _targetFinder = GetComponent<TargetFinder>();
        TargetPriority = TargetPriority.First;
    }

    public void ChoseTarget()
    {
        _target = _targetFinder.FindTarget(TargetType, TargetPriority);
    }

    public void ChangeAction(Action action)
    {
        _currentAction = action;
    }

    public void DoAction()
    {
        _currentAction.Do(_target);
    }

    public void ChangeTargetPriority()
    {
        int targetPrioritiesCount = TargetPriority.GetNames(typeof(TargetPriority)).Length;
        int priorityIndex = (int)TargetPriority;
        priorityIndex++;

        if (priorityIndex >= targetPrioritiesCount)
        {
            priorityIndex = 0;
        }

        TargetPriority = (TargetPriority)priorityIndex;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }

    public TargetType GetTargetType()
    {
        return Type;
    }
}
