using TreeEditor;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float jumpForce = 14f;
    public bool isDucking = true;
    public bool isGrounded = true;
    public float duckScaleY = 0.3f;

    private Rigidbody2D rb;
    private GameManager gameManager;
    private Vector3 originalScale;
    private BoxCollider2D boxCollider;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
        boxCollider = GetComponent<BoxCollider2D>();

        originalScale = transform.localScale;
        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S) && isGrounded)
        {
            Duck();
        }

        if (isDucking)
        {
            Standing();
        }
    }

    void Jump()
    {
        rb.linearVelocityY = jumpForce;
        isGrounded = false;
    }

    void Duck()
    {
        isDucking = true;
        transform.localScale = new Vector3(originalScale.x, originalScale.y * duckScaleY, originalScale.z);

        boxCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y * duckScaleY);
        boxCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y * duckScaleY);
    }

    void Standing()
    {
        isDucking = false;
        transform.localScale = originalScale;

        boxCollider.size = originalColliderSize;
        boxCollider.offset = originalColliderOffset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
        }
    }
}
