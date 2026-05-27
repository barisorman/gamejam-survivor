using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHp = 100f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _damageMultiplier = 1f;
    [SerializeField] private float _magnetRange = 3f;

    public float MaxHp;
    public float CurrentHp;
    public float MoveSpeed;
    public float DamageMultiplier;
    public float MagnetRange;

    private void Awake()
    {
        MaxHp = _maxHp;
        CurrentHp = _maxHp;
        MoveSpeed = _moveSpeed;
        DamageMultiplier = _damageMultiplier;
        MagnetRange = _magnetRange;
    }

    public void TakeDamage(float amount)
    {
        if (CurrentHp <= 0f)
        {
            return;
        }

        CurrentHp -= amount;

        if (CurrentHp < 0f)
        {
            CurrentHp = 0f;
        }

        GameEvents.PlayerDamaged(amount);

        if (CurrentHp <= 0f)
        {
            GameEvents.PlayerDied();
        }
    }

    public void Heal(float amount)
    {
        CurrentHp += amount;
        if (CurrentHp > MaxHp)
        {
            CurrentHp = MaxHp;
        }
    }

    public void IncreaseMoveSpeed(float amount)
    {
        MoveSpeed += amount;
    }

    public void IncreaseDamage(float amount)
    {
        DamageMultiplier += amount;
    }

    public void IncreaseMagnetRange(float amount)
    {
        MagnetRange += amount;
    }

    public void IncreaseMaxHp(float amount)
    {
        MaxHp += amount;
        CurrentHp += amount;
    }
}