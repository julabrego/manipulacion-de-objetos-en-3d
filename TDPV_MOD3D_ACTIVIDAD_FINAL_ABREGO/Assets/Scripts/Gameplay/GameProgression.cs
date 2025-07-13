using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class GameProgression : MonoBehaviour
{
    [SerializeField] private GameProgressionData progressionData;

    //--------- Events ---------- //
    [SerializeField] UnityEvent<string> OnMessageTriggered;
    [SerializeField] UnityEvent<bool> OnEndGameTriggered;

    private void OnEnable()
    {
        GameEvents.OnVictory += Win;
        GameEvents.OnGameOver += Lose;
        GameEvents.OnAddItem += AddItem;
        GameEvents.OnSubstractItem += SubstractItem;
    }

    private void OnDisable()
    {
        GameEvents.OnVictory -= Win;
        GameEvents.OnGameOver -= Lose;
        GameEvents.OnAddItem -= AddItem;
        GameEvents.OnSubstractItem -= SubstractItem;
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

        if (GetCollectedItems() >= GetCollectableItemsToWin())
        {
            Win();
        }
    }

    public void SubstractItem()
    {
        progressionData.CollectedItems--;
    }

    public void Win()
    {
        GameManager.Instance.AddScore(1000);
        GameManager.Instance.SetIsPlaying(false);
        OnEndGameTriggered.Invoke(true);
    }
    public void Lose()
    {
        OnEndGameTriggered.Invoke(false);
    }
}
