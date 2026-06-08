using UnityEngine;

public class ButterflyMove : MonoBehaviour
{
    public float speed = 2f;   // 나비가 움직이는 속도 (빠르게/느리게)
    public float range = 3f;   // 움직이는 범위 (좌우로 얼마나 이동할지)
    private Vector3 startPos;  // 시작 위치 저장 (기준점)

    void Start()
    {
        // 게임 시작 시 현재 위치를 기준점으로 저장
        startPos = transform.position;
    }

    void Update()
    {
        // Time.time: 게임 시작 후 흐른 시간
        // Mathf.Sin: -1 ~ 1 사이의 값을 반복적으로 반환하는 함수 (파도처럼 움직임)
        // Sin(Time * speed) → 시간에 따라 좌우로 진동하는 값
        float x = Mathf.Sin(Time.time * speed) * range;

        // 시작 위치에서 x축으로만 움직이도록 설정
        transform.position = startPos + new Vector3(x, 0, 0);
    }
}
