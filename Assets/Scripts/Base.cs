using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour, IBase, IHasHealth
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private UnityEvent _onChangeHealthEvent;

    public event UnityAction OnChangeHealthEvent
    {
        add => _onChangeHealthEvent.AddListener(value);
        remove => _onChangeHealthEvent.RemoveListener(value);
    }

    public bool IsDefeated { get; private set; }

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

    public void TakeDamage(int damage)
    {
        if (IsDefeated)
        {
            Debug.Log("База уже побеждена");
            return;
        }

        CurrentHealth -= damage;
        _onChangeHealthEvent?.Invoke();

        if (_currentHealth <= 0)
        {
            IsDefeated = true;
        }
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public void AddListenerOnChangeHealthEvent(UnityAction unityAction)
    {
        OnChangeHealthEvent += unityAction;
    }

    private void OnValidate()
    {
        if (_maxHealth < 1)
        {
            _maxHealth = 1;
        }

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void OnDestroy()
    {
        _onChangeHealthEvent.RemoveAllListeners();
    }
}
