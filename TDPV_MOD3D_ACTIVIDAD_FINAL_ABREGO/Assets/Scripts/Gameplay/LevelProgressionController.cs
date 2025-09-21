using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class LevelProgressionController : MonoBehaviour
{
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int collectedItems = 0;
    [SerializeField] private int collectableItemsToWin = 0;
    [SerializeField] private float elapsedTime = 0f;

    //--------- Events ---------- //
    [SerializeField] UnityEvent<string> OnCollectedItemsTextChanged;
    [SerializeField] UnityEvent<string> OnMessageTriggered;
    [SerializeField] UnityEvent OnAllItemsCollectedTriggered;
    [SerializeField] UnityEvent OnCountdownFinishedTriggered;

    private void OnEnable()
    {
        GameEvents.OnVictory += Win;
        GameEvents.OnGameOver += Lose;
        GameEvents.OnAddItem += AddItem;
        GameEvents.OnSubstractItem += SubstractItem;
        GameEvents.OnCountdownFinished += Lose;
        GameEvents.OnUpdateElapsedTime += UpdateElapsedTime;
    }

    private void OnDisable()
    {
        GameEvents.OnVictory -= Win;
        GameEvents.OnGameOver -= Lose;
        GameEvents.OnAddItem -= AddItem;
        GameEvents.OnSubstractItem -= SubstractItem;
        GameEvents.OnCountdownFinished -= Lose;
    }

    private void Start()
    {
        //progressionData.CollectedItems = collectedItems;
        //progressionData.CollectableItemsToWin = collectableItemsToWin;
        OnCollectedItemsTextChanged.Invoke(GetCollectedItems().ToString());
    }

    public int GetCollectedItems()
    {
        return collectedItems;
    }

    public int GetCollectableItemsToWin()
    {
        return collectableItemsToWin;
    }

    public void AddItem()
    {
        collectedItems++;
        OnCollectedItemsTextChanged.Invoke(GetCollectedItems().ToString());

        if (GetCollectedItems() >= GetCollectableItemsToWin())
        {
            Win();
        }
    }

    public void SubstractItem()
    {
        collectedItems--;
        OnCollectedItemsTextChanged.Invoke(GetCollectedItems().ToString());
    }

    public void UpdateElapsedTime(float value)
    {
        elapsedTime = value;
    }

    public void Win()
    {
        GameManager.Instance.SetCurrentInGameState(InGameStates.ENDED_GAME);
        OnAllItemsCollectedTriggered.Invoke();

        var records = GameManager.Instance.GameProgression.TimeRecords;
        int index = System.Array.FindIndex(records, record => record.level == currentLevel);

        if (index == -1)
        {
            // No existe registro, lo añadimos
            var newList = new List<GameProgressionData.TimeRecordByLevel>(records)
        {
            new GameProgressionData.TimeRecordByLevel
            {
                level = currentLevel,
                timeRecord = elapsedTime
            }
        };
            GameManager.Instance.GameProgression.TimeRecords = newList.ToArray();
        }
        else
        {
            // Existe registro, actualizamos solo si el tiempo es menor
            if (elapsedTime < records[index].timeRecord || records[index].timeRecord == 0f)
            {
                records[index].timeRecord = elapsedTime;
                GameManager.Instance.GameProgression.TimeRecords = records;
            }
        }
    }

    public void Lose()
    {
        GameManager.Instance.SetCurrentInGameState(InGameStates.ENDED_GAME);
        OnCountdownFinishedTriggered.Invoke();
    }
}
