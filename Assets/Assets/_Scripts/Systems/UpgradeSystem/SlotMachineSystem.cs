using System.Collections;
using UnityEngine;

public class SlotMachineSystem : MonoBehaviour
{
    [SerializeField] private SlotMachineUI _ui;
    [SerializeField] private UpgradeManager _upgradeManager;

    private string[] _symbols = { "⚔", "⚡", "🌀", "💀", "🍀" };
    private int _currentLevel = 1;

    private const float BaseJackpotChance = 0.20f;
    private const float JackpotChancePerLevel = 0.015f;
    private const float MaxJackpotChance = 0.40f;

    private void OnEnable()
    {
        GameEvents.OnLevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelUp -= OnLevelUp;
    }

    private void OnLevelUp(int level)
    {
        _currentLevel = level;
        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        yield return null;

        GameManager.Instance.SetPaused(true);

        float chance = BaseJackpotChance + (_currentLevel - 1) * JackpotChancePerLevel;
        if (chance > MaxJackpotChance)
        {
            chance = MaxJackpotChance;
        }

        bool isJackpot = Random.value < chance;
        string[] results = GenerateResults(isJackpot);

        yield return StartCoroutine(_ui.PlaySpin(results, isJackpot));

        if (isJackpot)
        {
            _upgradeManager.ApplyEvolvedUpgrade();
        }
        else
        {
            _upgradeManager.ShowUpgradeCards();
        }
    }

    private string[] GenerateResults(bool jackpot)
    {
        if (jackpot)
        {
            string s = _symbols[Random.Range(0, _symbols.Length)];
            return new string[] { s, s, s };
        }

        return new string[]
        {
            _symbols[Random.Range(0, _symbols.Length)],
            _symbols[Random.Range(0, _symbols.Length)],
            _symbols[Random.Range(0, _symbols.Length)]
        };
    }
}