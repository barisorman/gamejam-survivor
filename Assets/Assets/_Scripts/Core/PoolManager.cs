using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private GruntEnemy _gruntPrefab;
    [SerializeField] private SpeedEnemy _speedPrefab;
    [SerializeField] private TankEnemy  _tankPrefab;
    [SerializeField] private XPOrb      _xpOrbPrefab;

    private Queue<Projectile> _projectiles = new Queue<Projectile>();
    private Queue<GruntEnemy> _grunts      = new Queue<GruntEnemy>();
    private Queue<SpeedEnemy> _speeders    = new Queue<SpeedEnemy>();
    private Queue<TankEnemy>  _tanks       = new Queue<TankEnemy>();
    private Queue<XPOrb>      _xpOrbs      = new Queue<XPOrb>();

    private void Awake()
    {
        Instance = this;

        // Pre-fill pools before gameplay starts
        for (int i = 0; i < 80;  i++) CreateAndHide(_projectilePrefab, _projectiles);
        for (int i = 0; i < 60;  i++) CreateAndHide(_gruntPrefab,      _grunts);
        for (int i = 0; i < 40;  i++) CreateAndHide(_speedPrefab,      _speeders);
        for (int i = 0; i < 20;  i++) CreateAndHide(_tankPrefab,        _tanks);
        for (int i = 0; i < 100; i++) CreateAndHide(_xpOrbPrefab,      _xpOrbs);
    }

    private void CreateAndHide<T>(T prefab, Queue<T> pool) where T : MonoBehaviour
    {
        T obj = Instantiate(prefab, transform);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    private T GetFromPool<T>(Queue<T> pool, T prefab, Vector2 pos) where T : MonoBehaviour
    {
        T obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Instantiate(prefab, transform);
        }

        obj.transform.position = pos;
        obj.gameObject.SetActive(true);
        return obj;
    }

    private void ReturnToPool<T>(Queue<T> pool, T obj) where T : MonoBehaviour
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    public Projectile GetProjectile(Vector2 pos) { return GetFromPool(_projectiles, _projectilePrefab, pos); }
    public GruntEnemy GetGrunt(Vector2 pos)       { return GetFromPool(_grunts,      _gruntPrefab,      pos); }
    public SpeedEnemy GetSpeeder(Vector2 pos)     { return GetFromPool(_speeders,    _speedPrefab,      pos); }
    public TankEnemy  GetTank(Vector2 pos)        { return GetFromPool(_tanks,       _tankPrefab,       pos); }
    public XPOrb      GetXPOrb(Vector2 pos)       { return GetFromPool(_xpOrbs,      _xpOrbPrefab,      pos); }

    public void ReturnProjectile(Projectile p) { ReturnToPool(_projectiles, p); }
    public void ReturnXPOrb(XPOrb o)           { ReturnToPool(_xpOrbs,      o); }

    public void ReturnEnemy(BaseEnemy e)
    {
        if      (e is GruntEnemy grunt)   { ReturnToPool(_grunts,   grunt); }
        else if (e is SpeedEnemy speeder) { ReturnToPool(_speeders, speeder); }
        else if (e is TankEnemy tank)     { ReturnToPool(_tanks,    tank); }
        else                              { Destroy(e.gameObject); }
    }
}