using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseEnemy : MonoBehaviour
{
    public enum EnemyState { Chase, Dead }

    [SerializeField] protected EnemyData Data;

    protected EnemyState CurrentState;
    protected Rigidbody2D Rb;
    protected Transform PlayerTransform;

    private float _currentHp;
    private float _damageTimer;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.gravityScale = 0f;
        Rb.freezeRotation = true;
    }

    // Called every time enemy is taken from pool
    protected virtual void OnEnable()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerTransform = player.transform;
        }

        _currentHp = Data.MaxHp;
        _damageTimer = 0f;
        CurrentState = EnemyState.Chase;
    }

    protected virtual void FixedUpdate()
    {
        if (CurrentState == EnemyState.Dead || PlayerTransform == null)
        {
            return;
        }

        Vector2 direction = ((Vector2)PlayerTransform.position - Rb.position).normalized;
        Rb.MovePosition(Rb.position + direction * Data.MoveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        // Damage once per second instead of every frame
        _damageTimer += Time.fixedDeltaTime;
        if (_damageTimer < 1f)
        {
            return;
        }

        _damageTimer = 0f;
        other.GetComponent<PlayerStats>().TakeDamage(Data.DamagePerSec);
    }

    public virtual void TakeDamage(float amount)
    {
        if (CurrentState == EnemyState.Dead)
        {
            return;
        }

        _currentHp -= amount;

        if (_currentHp <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        CurrentState = EnemyState.Dead;
        GameEvents.EnemyKilled(Data.XpValue, Rb.position);
        PoolManager.Instance.ReturnEnemy(this);
    }
}