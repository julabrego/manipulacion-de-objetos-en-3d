using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 60f;
    public float elapsedTime = 0f;
    public bool isCounting = true;
    public string formattedCounter = "";

    private void OnEnable()
    {
        GameEvents.OnGameStart += StartCounting;
        GameEvents.OnGameOver += FinishCounting;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStart -= StartCounting;
        GameEvents.OnGameOver -= FinishCounting;
    }

    void Update()
    {
        isCounting = GameManager.Instance.currentInGameState == InGameStates.PLAYING_GAME;

        if (isCounting)
        {
            if (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
                elapsedTime += Time.deltaTime;
                UpdateCountodownString(totalTime);
                GameEvents.TriggerUpdateElapsedTime(elapsedTime);
            }
            else
            {
                totalTime = 0;
                UpdateCountodownString(totalTime);
                this.FinishCounting();
                Debug.Log("Time's Up!");
            }
        }
    }

    void UpdateCountodownString(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        GameEvents.TriggerCountdownUpdate();

        formattedCounter = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void StartCounting()
    {
        this.isCounting = true;
    }

    private void FinishCounting()
    {
        this.isCounting = false;
        GameEvents.TriggerCountdownFinished();
    }
}