using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float jumpForce = 12f;
    public bool isGrounded = true;
    private Rigidbody2D rb;
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocityY = rb.linearVelocityY * jumpForce;
    }
}
