using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _minSpawnDist = 12f;
    [SerializeField] private float _maxSpawnDist = 18f;

    public enum EnemyType { Grunt, Speed, Tank }
    public EnemyType ActiveType = EnemyType.Grunt;

    private Transform _player;
    private float _timer;
    private bool _active = true;

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
        }
    }

    private void Update()
    {
        if (!_active || _player == null)
        {
            return;
        }

        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
        {
            return;
        }

        _timer += Time.deltaTime;
        if (_timer < _spawnInterval)
        {
            return;
        }

        _timer = 0f;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Vector2 pos = GetSpawnPosition();

        if (ActiveType == EnemyType.Grunt)
        {
            PoolManager.Instance.GetGrunt(pos);
        }
        else if (ActiveType == EnemyType.Speed)
        {
            PoolManager.Instance.GetSpeeder(pos);
        }
        else if (ActiveType == EnemyType.Tank)
        {
            PoolManager.Instance.GetTank(pos);
        }
    }

    private Vector2 GetSpawnPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float dist = Random.Range(_minSpawnDist, _maxSpawnDist);
        return (Vector2)_player.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * dist;
    }

    public void SetSpawnInterval(float interval)
    {
        _spawnInterval = interval;
    }

    public void SetEnemyType(EnemyType type)
    {
        ActiveType = type;
    }

    public void StopSpawning()
    {
        _active = false;
    }
}