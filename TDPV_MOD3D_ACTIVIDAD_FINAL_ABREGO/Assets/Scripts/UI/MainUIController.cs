using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject RecordsPanel;
    [SerializeField] private GameObject CreditsPanel;

    [SerializeField] private TextMeshProUGUI RecordsText;

    enum CurrentPanel
    {
        MainMenu,
        Records,
        Credits
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentPanel("MainMenu");

        GameProgressionData.TimeRecordByLevel[] timeRecords = GameManager.Instance.GameProgression.TimeRecords;

        if (timeRecords.Length > 0)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            foreach (var record in timeRecords)
            {
                float minutes = Mathf.FloorToInt(record.timeRecord / 60);
                float seconds = Mathf.FloorToInt(record.timeRecord % 60);

                string formattedCounter = string.Format("{0:00}:{1:00}", minutes, seconds);

                sb.AppendLine($"Nivel {record.level}: {formattedCounter} segundos");
            }
            RecordsText.text = sb.ToString();
        }
        else
        {
            RecordsText.text = "No hay records disponibles.";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentPanel(string panelName)
    {
        switch (panelName)
        {
            case "MainMenu":
                MainMenuPanel.SetActive(true);
                RecordsPanel.SetActive(false);
                CreditsPanel.SetActive(false);
                break;
            case "Records":
                MainMenuPanel.SetActive(false);
                RecordsPanel.SetActive(true);
                CreditsPanel.SetActive(false);
                break;
            case "Credits":
                MainMenuPanel.SetActive(false);
                RecordsPanel.SetActive(false);
                CreditsPanel.SetActive(true);
                break;
            default:
                MainMenuPanel.SetActive(true);
                RecordsPanel.SetActive(false);
                CreditsPanel.SetActive(false);
                break;
        }
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
