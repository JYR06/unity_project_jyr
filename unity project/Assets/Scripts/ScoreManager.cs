using UnityEngine;
using UnityEngine.UI; // Unity 기본 Text UI 사용 시 필요

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;   // 점수를 표시할 UI(Text)
    private int score = 0;   // 현재 점수 값 (초기값 0)

    void Start()
    {
        // 게임 시작 시 UI에 초기 점수 표시
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int value)
    {
        // 점수를 value만큼 증가시킴
        score += value;

        // UI에 갱신된 점수 표시
        scoreText.text = "Score: " + score;
    }
}
