using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;   // 남은 시간 표시
    public TextMeshProUGUI resultText;  // 결과 문구 표시
    public float timeLimit = 30f;       // 제한 시간 (초)
    private float timeRemaining;
    private bool isGameOver = false;
    private bool isWin = false;

    void Start()
    {
        timeRemaining = timeLimit;
        resultText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isGameOver)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0) timeRemaining = 0;

            timerText.text = "Time: " + Mathf.Ceil(timeRemaining);

            if (timeRemaining <= 0 && !isWin)
            {
                GameFail();
            }
        }
    }

    public void GameWin()
    {
        isGameOver = true;
        isWin = true;
        resultText.gameObject.SetActive(true);
        resultText.text = "🏁 You Win!";
    }

    public void GameFail()
    {
        isGameOver = true;
        resultText.gameObject.SetActive(true);
        resultText.text = "💀 Time's Up! You Fail!";
    }
}
