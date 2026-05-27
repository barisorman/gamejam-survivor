using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private BossEnemy _bossPrefab;

    private float _elapsed;
    private bool _bossSpawned;

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
        {
            return;
        }

        _elapsed += Time.deltaTime;
        float minutes = _elapsed / 60f;

        UpdateWave(minutes);

        if (!_bossSpawned && _elapsed >= 600f)
        {
            SpawnBoss();
        }
    }

    private void UpdateWave(float minutes)
    {
        if (minutes < 2f)
        {
            EnemySpawner.Instance.SetSpawnInterval(2f);
            EnemySpawner.Instance.SetEnemyType(EnemySpawner.EnemyType.Grunt);
        }
        else if (minutes < 4f)
        {
            EnemySpawner.Instance.SetSpawnInterval(1.5f);
            EnemySpawner.Instance.SetEnemyType(EnemySpawner.EnemyType.Grunt);
        }
        else if (minutes < 6f)
        {
            EnemySpawner.Instance.SetSpawnInterval(1f);
            EnemySpawner.Instance.SetEnemyType(EnemySpawner.EnemyType.Speed);
        }
        else if (minutes < 8f)
        {
            EnemySpawner.Instance.SetSpawnInterval(0.8f);
            EnemySpawner.Instance.SetEnemyType(EnemySpawner.EnemyType.Tank);
        }
        else
        {
            EnemySpawner.Instance.SetSpawnInterval(0.5f);
        }
    }

    private void SpawnBoss()
    {
        _bossSpawned = true;
        EnemySpawner.Instance.StopSpawning();

        GameObject player = GameObject.FindWithTag("Player");
        Vector2 pos = (Vector2)player.transform.position + Vector2.right * 15f;
        Instantiate(_bossPrefab, pos, Quaternion.identity);
    }
}