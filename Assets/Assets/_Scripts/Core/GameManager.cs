using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { Playing, Paused, GameOver, Win }
    public GameState CurrentState;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        GameEvents.GameStarted();
    }

    public void SetGameOver()
    {
        if (CurrentState == GameState.GameOver)
        {
            return;
        }
        CurrentState = GameState.GameOver;
        Time.timeScale = 0f;
        GameEvents.GameEnded(false);
    }

    public void SetWin()
    {
        if (CurrentState == GameState.Win)
        {
            return;
        }
        CurrentState = GameState.Win;
        Time.timeScale = 0f;
        GameEvents.GameEnded(true);
    }

    public void SetPaused(bool paused)
    {
        if (paused)
        {
            CurrentState = GameState.Paused;
            Time.timeScale = 0f;
        }
        else
        {
            CurrentState = GameState.Playing;
            Time.timeScale = 1f;
        }
    }
}