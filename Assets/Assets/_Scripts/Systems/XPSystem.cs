using UnityEngine;

public class XPSystem : MonoBehaviour
{
    public static XPSystem Instance;

    private Transform _player;
    private PlayerStats _playerStats;
    private float _totalXP;

    // Pre-allocated array to avoid GC in Update
    private Collider2D[] _magnetResults = new Collider2D[50];
    [SerializeField] private LayerMask _xpOrbLayer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _player = player.transform;
            _playerStats = player.GetComponent<PlayerStats>();
        }
    }

    private void OnEnable()
    {
        GameEvents.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyKilled -= OnEnemyKilled;
    }

    private void OnEnemyKilled(int xp, Vector2 pos)
    {
        XPOrb orb = PoolManager.Instance.GetXPOrb(pos);
        orb.Init(xp, _player);
    }

    private void Update()
    {
        if (_player == null || _playerStats == null)
        {
            return;
        }

        // Pull nearby orbs into magnet range
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            _player.position,
            _playerStats.MagnetRange,
            _xpOrbLayer
        );

        foreach (Collider2D hit in hits)
        {
            XPOrb orb = hit.GetComponent<XPOrb>();
            if (orb != null)
            {
                orb.Attract();
            }
        }
    }

    public void AddXP(int amount)
    {
        _totalXP += amount;
        GameEvents.XPChanged(_totalXP, 0f);
    }
}