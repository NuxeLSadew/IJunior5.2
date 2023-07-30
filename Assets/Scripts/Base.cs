using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;
    [SerializeField] private UnityEvent<float> _onChangeHealthEvent;

    public bool IsDefeated { get; private set; }

    public int Health
    {
        get => _health;
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

            _health = value;
        }
    }

    private void OnValidate()
    {
        if (_maxHealth < 1)
        {
            _maxHealth = 1;
        }

        if (_health < 0)
        {
            _health = 0;
        }
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDefeated)
        {
            Debug.Log("База уже побеждена");
            return;
        }

        _health -= damage;
        _onChangeHealthEvent?.Invoke((float)_health / _maxHealth);

        if (_health <= 0)
        {
            IsDefeated = true;
        }
    }
}
