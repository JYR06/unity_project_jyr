using UnityEngine;
using UnityEngine.UI; // Text UI 사용 시 필요

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;   // 점수 표시할 UI
    private int score = 0;   // 현재 점수

    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }
}
