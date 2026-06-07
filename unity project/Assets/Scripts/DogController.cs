using UnityEngine;

public class DogController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = false;
    private Vector3 startPosition;
    private Vector3 defaultScale = new Vector3(0.2f, 0.2f, 0.2f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position; // 시작 위치 저장
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        if (move != 0)
        {
            animator.SetBool("isRunning", true);
            transform.localScale = move > 0 ? defaultScale : new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

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
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }

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
        if (other.CompareTag("Flag"))
        {
            FindFirstObjectByType<GameManager>().GameWin();
        }
    }

    void LateUpdate()
    {
        float direction = transform.localScale.x >= 0 ? defaultScale.x : -defaultScale.x;
        transform.localScale = new Vector3(direction, defaultScale.y, defaultScale.z);
    }
}
