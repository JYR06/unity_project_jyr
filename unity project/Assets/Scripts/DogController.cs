using UnityEngine;

public class DogController : MonoBehaviour
{
    public float moveSpeed = 5f;       // 강아지 좌우 이동 속도
    public float jumpForce = 7f;       // 점프 힘
    private Rigidbody2D rb;            // 강아지 물리엔진(Rigidbody2D)
    private Animator animator;         // 애니메이터 (달리기/대기 애니메이션 전환)
    private bool isGrounded = false;   // 땅에 닿아있는지 여부 체크
    private Vector3 startPosition;     // 시작 위치 저장 (복귀용)
    private Vector3 defaultScale = new Vector3(0.2f, 0.2f, 0.2f); // 기본 크기

    void Start()
    {
        // Rigidbody2D와 Animator 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 게임 시작 시 현재 위치를 시작 위치로 저장
        startPosition = transform.position;
    }

    void Update()
    {
        // 좌우 이동 입력 (-1: 왼쪽, 0: 정지, 1: 오른쪽)
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // 이동 중이면 달리기 애니메이션 실행 + 방향 전환
        if (move != 0)
        {
            animator.SetBool("isRunning", true);
            transform.localScale = move > 0 ? defaultScale : new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // 점프 (스페이스바) — 땅에 닿아있을 때만 가능
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // 바닥 아래로 떨어지면 원래 위치로 복귀
        if (transform.position.y < -10f)
        {
            transform.position = startPosition;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅이나 발판에 닿으면 점프 가능 상태로 변경
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }

        // 장애물에 닿으면 원래 위치로 복귀
        if (collision.gameObject.CompareTag("Rock") ||
            collision.gameObject.CompareTag("Ball") ||
            collision.gameObject.CompareTag("Butterfly"))
        {
            transform.position = startPosition;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 깃발에 닿으면 GameManager의 승리 처리 호출
        if (other.CompareTag("Flag"))
        {
            FindFirstObjectByType<GameManager>().GameWin();
        }
    }

    void LateUpdate()
    {
        // 애니메이션이 Scale을 덮어써도 방향 유지 (좌우 반전)
        float direction = transform.localScale.x >= 0 ? defaultScale.x : -defaultScale.x;
        transform.localScale = new Vector3(direction, defaultScale.y, defaultScale.z);
    }
}
