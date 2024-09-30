using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontal;
    public float speed;
    public float jumpingPower;
    private bool isFacingRight = true;
    private bool isHidding = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Collider2D colliderpl;
    private RigidbodyConstraints2D originalConstraints;

    public static Movement Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        originalConstraints = rb.constraints;

        colliderpl = GetComponent<Collider2D>();
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl))
        {
            // is crouching
            transform.localScale = Vector2.right * transform.localScale.x + Vector2.up * 0.5f; // making character smaller / can be swapped with play animation or something like that
        }
        else
        {
            // not crouching
            transform.localScale = Vector2.right * transform.localScale.x + Vector2.up; // reset characters size
        }

        HandleFlipping();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void HandleFlipping()
    {
        if (isFacingRight && horizontal < 0f)
        {
            isFacingRight = false;
            transform.localScale = Vector2.up + Vector2.right * -1;
        }
        else if (!isFacingRight && horizontal > 0f)
        {
            isFacingRight = true;
            transform.localScale = Vector2.one;
        }
    }

    public void ToggleHidding(bool _isHidding)
    {
        isHidding = _isHidding;

        if (_isHidding)
        {
            sr.enabled = false;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            colliderpl.enabled = false;
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
            sr.enabled = true;
            rb.constraints = originalConstraints;
            colliderpl.enabled = true;
        }
    }

    public bool IsHidding()
    {
        return isHidding;
    }
}
