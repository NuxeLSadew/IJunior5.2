using UnityEngine;

public class Enemy : MonoBehaviour, ITargetable
{
    private const TargetType Type = TargetType.Single;

    [SerializeField] private int _maxHealth;

    private int _currentHealth;

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

    public void Die()
    {
        Destroy(gameObject);
    }

    public TargetType GetTargetType()
    {
        return Type;
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
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
}
