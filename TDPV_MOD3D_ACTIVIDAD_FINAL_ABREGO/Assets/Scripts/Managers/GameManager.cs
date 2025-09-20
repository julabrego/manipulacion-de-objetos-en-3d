using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Screens
{
    OPENING,
    MAIN_MENU,
    GAME,
    END
}

public enum InGameStates
{
    PLAYING_CINEMATIC,
    PLAYING_GAME,
    PAUSED,
    ENDED_GAME,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PersistenceKeys persistanceKeys;
    public PersistenceKeys PersistenceKeys { get => persistanceKeys; private set => persistanceKeys = value; }

    public Screens currentScreen = Screens.MAIN_MENU;
    public InGameStates currentInGameState = InGameStates.PLAYING_GAME;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        if (!activeSceneName.Equals("MainMenu", System.StringComparison.OrdinalIgnoreCase))
        {
            currentScreen = Screens.GAME;
        }
        else
        {
            currentScreen = Screens.MAIN_MENU;
        }
        currentInGameState = InGameStates.PLAYING_GAME;
    }

    private void Update()
    {
        if (currentScreen.Equals(Screens.GAME))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (currentInGameState == InGameStates.PLAYING_GAME)
                {
                    SetCurrentInGameState(InGameStates.PAUSED);
                }
                else if (currentInGameState == InGameStates.PAUSED)
                {
                    SetCurrentInGameState(InGameStates.PLAYING_GAME);
                }
            }
        }
    }

    public void StartGame()
    {
        currentInGameState = InGameStates.PLAYING_GAME;
        currentScreen = Screens.GAME;
        ApplicationManager.Instance.GoToScene("Prototype_house");
    }

    public void Restart()
    {
        currentInGameState = InGameStates.PAUSED;
        currentScreen = Screens.MAIN_MENU;
        ApplicationManager.Instance.GoToScene("MainMenu");
    }

    public bool IsPlayingGame()
    {
        return currentInGameState == InGameStates.PLAYING_GAME;
    }

    public void SetCurrentScreen(Screens screen)
    {
        currentScreen = screen;
    }

    public void SetCurrentInGameState(InGameStates state)
    {
        currentInGameState = state;

        switch (currentInGameState)
        {
            case InGameStates.PLAYING_CINEMATIC:
                Time.timeScale = 1f;
                break;
            case InGameStates.PLAYING_GAME:
                GameEvents.TriggerResume();
                Time.timeScale = 1f;
                break;
            case InGameStates.ENDED_GAME:
                break;
            case InGameStates.PAUSED:
                GameEvents.TriggerPause();
                Time.timeScale = 0f;
                break;
            default:
                break;
        }

    }

    public void EndGame(bool isWin)
    {
        SetCurrentInGameState(InGameStates.ENDED_GAME);
    }

}
