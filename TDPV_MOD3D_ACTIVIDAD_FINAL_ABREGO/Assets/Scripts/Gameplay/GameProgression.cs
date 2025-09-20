using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class GameProgression : MonoBehaviour
{
    [SerializeField] private GameProgressionData progressionData;

    //--------- Events ---------- //
    [SerializeField] UnityEvent<string> OnCollectedItemsTextChanged;
    [SerializeField] UnityEvent<string> OnMessageTriggered;
    [SerializeField] UnityEvent<bool> OnEndGameTriggered;
    [SerializeField] UnityEvent<bool> OnCountdownFinishedTriggerd;

    private void OnEnable()
    {
        GameEvents.OnVictory += Win;
        GameEvents.OnGameOver += Lose;
        GameEvents.OnAddItem += AddItem;
        GameEvents.OnSubstractItem += SubstractItem;
        GameEvents.OnCountdownFinished += Lose;
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
        progressionData.CollectableItemsToWin = 7;
        progressionData.CollectedItems = 0;
    }

    public int GetCollectedItems()
    {
        return progressionData.CollectedItems;
    }

    public int GetCollectableItemsToWin()
    {
        return progressionData.CollectableItemsToWin;
    }

    public void AddItem()
    {
        progressionData.CollectedItems++;
        OnCollectedItemsTextChanged.Invoke(GetCollectedItems().ToString());

        if (GetCollectedItems() >= GetCollectableItemsToWin())
        {
            Win();
        }
    }

    public void SubstractItem()
    {
        progressionData.CollectedItems--;
        OnCollectedItemsTextChanged.Invoke(GetCollectedItems().ToString());
    }

    public void Win()
    {
        GameManager.Instance.SetCurrentInGameState(InGameStates.ENDED_GAME);
        OnEndGameTriggered.Invoke(true);
        OnCollectedItemsTextChanged.Invoke(GetCollectedItems().ToString());
    }
    public void Lose()
    {
        OnCountdownFinishedTriggerd.Invoke(false);
        OnEndGameTriggered.Invoke(false);
    }
}
