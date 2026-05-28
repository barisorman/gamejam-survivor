using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _xpBar;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _levelText;

    private PlayerStats _playerStats;
    private float _elapsed;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _playerStats = player.GetComponent<PlayerStats>();
        }

        _levelText.text = "Lv 1";
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerDamaged += OnPlayerDamaged;
        GameEvents.OnXPChanged += OnXPChanged;
        GameEvents.OnLevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDamaged -= OnPlayerDamaged;
        GameEvents.OnXPChanged -= OnXPChanged;
        GameEvents.OnLevelUp -= OnLevelUp;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
        {
            return;
        }

        _elapsed += Time.deltaTime;

        float remaining = Mathf.Max(0f, 600f - _elapsed);
        int min = (int)(remaining / 60f);
        int sec = (int)(remaining % 60f);
        _timerText.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    private void OnPlayerDamaged(float amount)
    {
        if (_playerStats == null)
        {
            return;
        }

        _hpBar.value = _playerStats.CurrentHp / _playerStats.MaxHp;
    }

    private void OnXPChanged(float current, float max)
    {
        if (max <= 0f)
        {
            return;
        }

        _xpBar.value = current / max;
    }

    private void OnLevelUp(int level)
    {
        _levelText.text = "Lv " + level;
    }
}