using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float _fireRate = 0.8f;
    [SerializeField] private float _damage = 20f;
    [SerializeField] private float _projSpeed = 12f;
    [SerializeField] private float _range = 20f;

    private PlayerStats _stats;
    private float _fireTimer;

    private void Awake()
    {
        _stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
        {
            return;
        }

        _fireTimer += Time.deltaTime;
        if (_fireTimer < _fireRate)
        {
            return;
        }

        Transform target = FindNearestEnemy();
        if (target == null)
        {
            return;
        }

        _fireTimer = 0f;
        Fire(target);
    }

    private void Fire(Transform target)
    {
        Vector2 dir = ((Vector2)target.position - (Vector2)transform.position).normalized;
        Projectile p = PoolManager.Instance.GetProjectile(transform.position);
        p.Init(dir, _projSpeed, _damage * _stats.DamageMultiplier);
    }

    private Transform FindNearestEnemy()
    {
        BaseEnemy[] enemies = FindObjectsByType<BaseEnemy>(FindObjectsSortMode.None);
        Transform nearest = null;
        float minDist = _range;

        foreach (BaseEnemy e in enemies)
        {
            if (!e.gameObject.activeSelf)
            {
                continue;
            }

            float d = Vector2.Distance(transform.position, e.transform.position);
            if (d < minDist)
            {
                minDist = d;
                nearest = e.transform;
            }
        }

        return nearest;
    }

    public void IncreaseFireRate(float reduction)
    {
        _fireRate -= reduction;
        if (_fireRate < 0.1f)
        {
            _fireRate = 0.1f;
        }
    }

    public void IncreaseProjSpeed(float v)
    {
        _projSpeed += v;
    }

    public void IncreaseDamage(float v)
    {
        _damage += v;
    }
}