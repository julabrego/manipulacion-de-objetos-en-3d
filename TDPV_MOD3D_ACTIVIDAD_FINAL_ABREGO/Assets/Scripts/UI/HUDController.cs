using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class HUDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI collectedItemsText;
    [SerializeField] GameObject centeredMessageModal;
    [SerializeField] TextMeshProUGUI centeredMessageText;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] GameObject pauseModal;
    [SerializeField] GameProgression gameProgression;

    [SerializeField] CountdownTimer countdownTimerController;

    public void UpdateCollectedItemsText()
    {
        collectedItemsText.text = gameProgression.GetCollectedItems() + " / " + gameProgression.GetCollectableItemsToWin();
    }

    public void ShowMessageText(string message)
    {
        centeredMessageModal.gameObject.SetActive(true);
        centeredMessageText.text = message;
    }

    public void HideMessageText()
    {
        centeredMessageModal.gameObject.SetActive(false);
    }

    public void ShowGameOver(bool victory)
    {
        centeredMessageText.text = victory ? "GANASTE" : "GAME OVER";

        collectedItemsText.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);

        centeredMessageModal.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        GameEvents.OnPause += PauseGame;
        GameEvents.OnResume += ResumeGame;
        GameEvents.OnUpdateCoundown += UpdateCountdown;
    }


    private void OnDisable()
    {
        GameEvents.OnPause -= PauseGame;
        GameEvents.OnResume -= ResumeGame;
        GameEvents.OnUpdateCoundown -= UpdateCountdown;
    }

    private void UpdateCountdown()
    {
        if (countdownText != null)
        {
            this.countdownText.text = this.countdownTimerController.formattedCounter;
        }
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
