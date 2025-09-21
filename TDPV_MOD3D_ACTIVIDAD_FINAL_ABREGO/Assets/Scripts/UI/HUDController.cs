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
    [SerializeField] TextMeshProUGUI finalTimeText;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] GameObject pauseModal;
    [SerializeField] LevelProgressionController gameProgression;

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
        
        if (!victory) {
            finalTimeText.gameObject.SetActive(false);
        }

        collectedItemsText.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);

        centeredMessageModal.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        GameEvents.OnPause += PauseGame;
        GameEvents.OnResume += ResumeGame;
        GameEvents.OnUpdateCoundown += UpdateCountdown;
        GameEvents.OnUpdateElapsedTime += UpdateElapsedTime;

        UpdateCollectedItemsText();
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

    private void UpdateElapsedTime(float elapsedTime)
    {
        if (finalTimeText != null)
        {
            float minutes = Mathf.FloorToInt(elapsedTime / 60);
            float seconds = Mathf.FloorToInt(elapsedTime % 60);
            string formattedCounter = string.Format("{0:00}:{1:00}", minutes, seconds);
            finalTimeText.text = "Tu tiempo: " + formattedCounter;
            finalTimeText.gameObject.SetActive(true);
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

    public void Restart()
    {
        GameManager.Instance.Restart();
    }

}
