using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _damage;
    private float _lifetime;
    private float _timer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Called by PlayerWeapon after getting from pool
    public void Init(Vector2 direction, float speed, float damage, float lifetime = 3f)
    {
        _damage = damage;
        _lifetime = lifetime;
        _timer = 0f;
        _rb.linearVelocity = direction * speed;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _lifetime)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
        {
            return;
        }

        other.GetComponent<BaseEnemy>().TakeDamage(_damage);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        _rb.linearVelocity = Vector2.zero;
        PoolManager.Instance.ReturnProjectile(this);
    }
}