using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnGameStarted;
    public static event Action OnPlayerDied;
    public static event Action<bool> OnGameEnded;
    public static event Action<int, Vector2> OnEnemyKilled;
    public static event Action<float> OnPlayerDamaged;
    public static event Action<float, float> OnXPChanged;
    public static event Action<int> OnLevelUp;

    public static void GameStarted()                     => OnGameStarted?.Invoke();
    public static void PlayerDied()                      => OnPlayerDied?.Invoke();
    public static void GameEnded(bool win)               => OnGameEnded?.Invoke(win);
    public static void EnemyKilled(int xp, Vector2 pos) => OnEnemyKilled?.Invoke(xp, pos);
    public static void PlayerDamaged(float dmg)          => OnPlayerDamaged?.Invoke(dmg);
    public static void XPChanged(float cur, float max)   => OnXPChanged?.Invoke(cur, max);
    public static void LevelUp(int level)                => OnLevelUp?.Invoke(level);
}