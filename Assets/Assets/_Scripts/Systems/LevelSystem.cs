using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int _level = 1;
    private float _currentXP;
    private float _xpToNextLevel = 20f;

    private void OnEnable()
    {
        GameEvents.OnXPChanged += OnXPChanged;
    }

    private void OnDisable()
    {
        GameEvents.OnXPChanged -= OnXPChanged;
    }

    private void OnXPChanged(float totalXP, float unused)
    {
        // Unsubscribe before firing event to prevent infinite loop
        GameEvents.OnXPChanged -= OnXPChanged;

        _currentXP = totalXP;

        while (_currentXP >= _xpToNextLevel)
        {
            _currentXP -= _xpToNextLevel;
            _xpToNextLevel *= 1.5f;
            _level++;
            GameEvents.LevelUp(_level);
        }

        GameEvents.XPChanged(_currentXP, _xpToNextLevel);

        GameEvents.OnXPChanged += OnXPChanged;
    }

    public int GetLevel()
    {
        return _level;
    }
}