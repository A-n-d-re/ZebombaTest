using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private int greenBallScore = 2;
    [SerializeField] private int redBallScore = 1;
    [SerializeField] private int blueBallScore = 3;

    private int score;

    private void OnEnable()
    {
        score = 0;

        UpdateScoreUI();
    }

    public void AddScore(string ballName)
    {
        switch (ballName)
        {
            case "GreenBall":
                score += greenBallScore;
                break;
            case "RedBall":
                score += redBallScore;
                break;
            case "BlueBall":
                score += blueBallScore;
                break;
            default:
                break;
        }

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
