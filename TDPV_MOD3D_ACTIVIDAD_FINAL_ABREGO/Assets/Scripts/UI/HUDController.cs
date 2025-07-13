using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class HUDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI collectedItemsText;
    [SerializeField] TextMeshProUGUI centeredMessageText;
    [SerializeField] GameObject pauseModal;
    [SerializeField] GameProgression gameProgression;

    public void UpdateCollectedItemsText()
    {
        collectedItemsText.text = gameProgression.GetCollectedItems() + " / " + gameProgression.GetCollectableItemsToWin();
    }

    public void ShowMessageText(string message)
    {
        centeredMessageText.text = message;
    }

    public void ShowGameOver(bool victory)
    {
        centeredMessageText.text = victory ? "GANASTE" : "GAME OVER";
        centeredMessageText.gameObject.SetActive(true);
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

    private void PauseGame()
    {
        pauseModal.gameObject.SetActive(true);
    }

    private void ResumeGame()
    {
        pauseModal.gameObject.SetActive(false);

    }
}
