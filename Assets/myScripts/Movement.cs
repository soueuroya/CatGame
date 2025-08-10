using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontal;
    public float speed;
    public float jumpingPower;
    private bool isFacingRight = true;
    private bool isHidding = false;
    private bool isGrappling = false;
    private bool isAiming = false;
    private bool isAttacking = false;
    private bool isJumping = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Collider2D colliderpl;
    private RigidbodyConstraints2D originalConstraints;
    public static Movement Instance;

    public Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

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
        if (!isAttacking)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Jump") && (IsGrounded() || isGrappling))
            {
                isJumping = true;
                if (isGrappling) { Ungrappled(); }
                animator.SetTrigger("Jump");
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
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("Grounded", IsGrounded());
        animator.SetBool("Jumping", isJumping);
        animator.SetBool("Aiming", isAiming);
        animator.SetBool("Grappling", isGrappling);

        if (IsGrounded())
        {
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        //add hazardLayer to allow jumping on spikes
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

    public void SetIsAttacking(bool isAttacking)
    {
        rb.velocity = Vector2.zero;
        this.isAttacking = isAttacking;
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    public void Grappled()
    {
        isGrappling = true;
    }

    public void Ungrappled()
    {
        isGrappling = false;
        GetComponent<GrapplingHook>().Ungrappled();
    }

    public void SetIsAiming(bool isAiming)
    {
        this.isAiming = isAiming;
    }

    public void SetIsGrappling(bool isGrappling)
    {
        this.isGrappling = isGrappling;
    }
}