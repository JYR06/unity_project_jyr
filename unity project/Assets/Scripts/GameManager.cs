using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;   // 남은 시간을 표시할 UI(TextMeshPro)
    public TextMeshProUGUI resultText;  // 승리/실패 결과 문구를 표시할 UI(TextMeshPro)
    public float timeLimit = 30f;       // 제한 시간 (초 단위)
    private float timeRemaining;        // 현재 남은 시간
    private bool isGameOver = false;    // 게임 종료 여부
    private bool isWin = false;         // 승리 여부

    void Start()
    {
        // 게임 시작 시 제한 시간을 초기화하고 결과 문구는 숨김 처리
        timeRemaining = timeLimit;
        resultText.gameObject.SetActive(false);
    }

    void Update()
    {
        // 게임이 끝나지 않았을 때만 타이머 감소
        if (!isGameOver)
        {
            // 매 프레임마다 경과 시간(Time.deltaTime)을 빼서 남은 시간 갱신
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0) timeRemaining = 0; // 음수 방지

            // UI에 남은 시간을 올림(Mathf.Ceil)해서 표시
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining);

            // 시간이 다 되었는데 승리하지 못했으면 실패 처리
            if (timeRemaining <= 0 && !isWin)
            {
                GameFail();
            }
        }
    }

    public void GameWin()
    {
        // 승리 처리: 게임 종료 + 승리 상태 true + 결과 문구 표시
        isGameOver = true;
        isWin = true;
        resultText.gameObject.SetActive(true);
        resultText.text = "🏁 You Win!";
    }

    public void GameFail()
    {
        // 실패 처리: 게임 종료 + 결과 문구 표시
        isGameOver = true;
        resultText.gameObject.SetActive(true);
        resultText.text = "💀 Time's Up! You Fail!";
    }
}
