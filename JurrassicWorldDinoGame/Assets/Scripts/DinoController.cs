using TreeEditor;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float jumpForce = 14f;
    //Setting isDucking to true in the unity inspector will cause issues with the ducking animation.
    //Same for any booleans associated with animation triggers
    private bool isDucking = false;
    public bool isGrounded = true;
    private bool isDuckHeld = false;
    private bool duckChanging = false;
    public float duckScaleY = 0.4f;

    private Rigidbody2D rb;
    private GameManager gameManager;
    private Vector3 originalScale;
    private BoxCollider2D boxCollider;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    public Animator animator;

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
            if(!isDucking)
            {
                Jump();
            }
        }

        isDuckHeld = Input.GetKey(KeyCode.S);
        if (isDuckHeld && isGrounded)
        {
            Duck();
        }

        if (isDucking && !isDuckHeld)
        {
            Standing();
        }
    }

    void Jump()
    {
        //animator.SetTrigger("Jump");
        rb.linearVelocityY = jumpForce;
        isGrounded = false;
    }

    void Duck()
    {
        //Debug.Log("duck");
        if (!isDucking)
        {
            duckChanging = true;
            animator.SetTrigger("Duck");
            //transform.localScale = new Vector3(originalScale.x, originalScale.y * duckScaleY, originalScale.z);
            boxCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - originalColliderSize.y * duckScaleY);
            boxCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y * duckScaleY);
            //boxCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y * duckScaleY);
            isDucking = true;
            duckChanging = false;
        }
    }

    void Standing()
    {
        Debug.Log("Stand");
        if (duckChanging)
        {
            return;
        }
        else if(isDucking)
        {
            animator.SetTrigger("UnDuck");
        }
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

    public void Die()
    {
        Destroy(gameObject);
    }

}
