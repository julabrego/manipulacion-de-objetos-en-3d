using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score;
    private int highScore;

    private bool isPaused;
    private bool isPlaying;

    private readonly GameProgression gameProgression;

    [SerializeField] private PersistenceKeys persistanceKeys;
    public PersistenceKeys PersistenceKeys { get => persistanceKeys; private set => persistanceKeys = value; }
    public void AddScore(int _score)
    {
        score += _score;
        if (score > highScore)
        {
            highScore = score;
            // PersistenceManager.Instance.SetInt(PersistenceKeys.HighScoreKey, highScore);
        }
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

    public bool GetIsPlaying()
    {
        return isPlaying;
    }

    public void SetIsPlaying(bool value)
    {
        isPlaying = value;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // highScore = Mathf.Max(PersistenceManager.Instance.GetInt(PersistenceKeys.HighScoreKey), 0);
            isPaused = false;
            isPlaying = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnPause += PauseGame;
        GameEvents.OnResume += ResumeGame;
    }

    private void OnDisable()
    {
        GameEvents.OnPause -= PauseGame;
        GameEvents.OnResume -= ResumeGame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                GameEvents.TriggerPause();
            }
            else
            {
                GameEvents.TriggerResume();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}
