using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _winText;

    private float _elapsed;
    private int _kills;

    private void OnEnable()
    {
        GameEvents.OnGameEnded += OnGameEnded;
        GameEvents.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        GameEvents.OnGameEnded -= OnGameEnded;
        GameEvents.OnEnemyKilled -= OnEnemyKilled;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            _elapsed += Time.deltaTime;
        }
    }

    private void OnEnemyKilled(int xp, Vector2 pos)
    {
        _kills++;
    }

    private void OnGameEnded(bool win)
    {
        int min = (int)(_elapsed / 60f);
        int sec = (int)(_elapsed % 60f);
        string timeStr = min.ToString("00") + ":" + sec.ToString("00");

        if (win)
        {
            _winPanel.SetActive(true);
            _winText.text = "YOU WIN!\n\nTime: " + timeStr + "\nKills: " + _kills;
        }
        else
        {
            _gameOverPanel.SetActive(true);
            _gameOverText.text = "GAME OVER\n\nTime: " + timeStr + "\nKills: " + _kills;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}