using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;

public class ResultsUI : MonoBehaviour
{
    public TextMeshProUGUI scoreResultText;
    public TextMeshProUGUI heartsResultText;
    public TextMeshProUGUI timeResultText;

    public LocalizedString scoreLabel;
    public LocalizedString heartsLabel;
    public LocalizedString timeLabel;

    private void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        int finalHearts = PlayerPrefs.GetInt("FinalHearts", 0);
        int finalTime = PlayerPrefs.GetInt("FinalTime", 0);

        scoreResultText.text = "Puntos: " + finalScore;
        heartsResultText.text = "Vidas: " + finalHearts;
        timeResultText.text = "Tiempo: " + finalTime;

    }
}

