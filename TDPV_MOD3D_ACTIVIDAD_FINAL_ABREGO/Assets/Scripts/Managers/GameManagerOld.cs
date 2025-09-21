using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerOld : MonoBehaviour
{
    public static GameManagerOld Instance { get; private set; }

    private int score;
    private int highScore;

    private bool isPaused;
    private bool isPlaying;

    private readonly LevelProgressionController gameProgression;


    [SerializeField] private PersistenceKeys persistanceKeys;
    public PersistenceKeys PersistenceKeys { get => persistanceKeys; private set => persistanceKeys = value; }

    public enum Views
    {
        OPENING,
        MAIN_MENU,
        GAME,
        END
    }

    public enum InGameStates
    {
        PLAYING_CINEMATIC,
        PLAYING,
        PAUSED,
        FINISHED
    }

    private Views currentView = Views.MAIN_MENU;
    private InGameStates currentInGameState = InGameStates.PLAYING;

    public Views GetCurrentView()
    {
        return this.currentView;
    }

    public void SetCurrentView(Views currentView)
    {
        this.currentView = currentView;
    }

    public InGameStates GetCurrentInGameState()
    {
        return this.currentInGameState;
    }

    public void SetCurrentInGameState(InGameStates currentInGameState)
    {
        this.currentInGameState = currentInGameState;
    }

    public void AddScore(int _score)
    {
        score += _score;
        if (score > highScore)
        {
            highScore = score;
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
        GameEvents.OnCountdownFinished += FinishGame;
    }

    private void OnDisable()
    {
        GameEvents.OnPause -= PauseGame;
        GameEvents.OnResume -= ResumeGame;
        GameEvents.OnCountdownFinished -= FinishGame;
    }

    private void Update()
    {
        if (isPlaying)
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

    private void FinishGame()
    {
        Time.timeScale = 0;
        SetIsPlaying(false);
    }
}
