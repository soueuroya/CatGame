using Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontal;
    public float speed;
    public float jumpingPower;
    private bool isFacingRight = true;
    private bool isHiding = false;
    private bool isGrappling = false;
    private bool isAiming = false;
    private bool isAttacking = false;
    private bool isJumping = false;
    private bool isDead = false;
    private bool isCrouching = false;
    private bool canLook = true;
    private bool takingDamage = false;
    private bool isTryingToCrouch = false;
    private bool isTryingToUncrouch = false;
    private bool isUncrouching = false;
    private PlayerAnimationCallback pac;
    private RigidbodyConstraints2D originalConstraints;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform headCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private CapsuleCollider2D colliderpl;
    [SerializeField] private GameObject flipTarget;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineFramingTransposer transposer;
    public Animator animator;

    public static Movement Instance;

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
        pac = GetComponentInChildren<PlayerAnimationCallback>();
        colliderpl = GetComponent<CapsuleCollider2D>();

        transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        pac = GetComponentInChildren<PlayerAnimationCallback>();
        pac.SetUncrouchFinishCallback(() => {
            if (!isTryingToCrouch)
            {
                animator.SetBool("Crouching", false);
                isUncrouching = false;
                isCrouching = false;
                colliderpl.size = new Vector2(24.53f, 39.56f);
                colliderpl.offset = new Vector2(-0.15f, 2.68f);
            }
        });
    }

    void Update()
    {
        if (!isAttacking && !isDead && !isHiding && !takingDamage)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Jump") && (IsGrounded() || isGrappling))
            {
                isJumping = true;
                if (isGrappling) { Ungrappled(); }
                animator.SetTrigger("Jump");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            }

            HandleCrouch();

            if (Input.GetKey(KeyCode.S) && canLook)
            {
                transposer.m_TrackedObjectOffset = new Vector3(transposer.m_TrackedObjectOffset.x, -72, transposer.m_TrackedObjectOffset.z);
            }
            else
            {
                transposer.m_TrackedObjectOffset = new Vector3(transposer.m_TrackedObjectOffset.x, 74, transposer.m_TrackedObjectOffset.z);
            }

            HandleFlipping();
            if (isCrouching && IsGrounded())
            {
                rb.linearVelocity = new Vector2(horizontal * speed * 0.6f, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
            }
        }

        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("Grounded", IsGrounded());
        animator.SetBool("Jumping", isJumping);
        animator.SetBool("Aiming", isAiming);
        animator.SetBool("Grappling", isGrappling);
        animator.SetBool("Dead", isDead);

        if (IsGrounded())
        {
            isJumping = false;
        }
    }

    
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        //add hazardLayer to allow jumping on spikes
    }

    public bool IsCrouching()
    {
        return isCrouching;
    }

    public bool CanUncrouch()
    {
        return !Physics2D.OverlapCircle(headCheck.position, 5f, groundLayer);

    }


    public bool IsDead()
    {
        return isDead;
    }

    private void HandleCrouch()
    {
        if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftControl)) && !isCrouching) //Holding key to crouch
        {
            isTryingToCrouch = true;
            isTryingToUncrouch = false;
        }

        if (isTryingToCrouch && !isGrappling && !isDead && !isCrouching)
        {
            isTryingToUncrouch = false;
            isUncrouching = false;
            isCrouching = true;
            animator.ResetTrigger("Uncrouch");
            animator.SetTrigger("Crouch");
            animator.SetBool("Crouching", true);
            colliderpl.size = new Vector2(24.53f, 24.53f);
            colliderpl.offset = new Vector2(-0.15f, -4.83f);
        }

        if ((Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.LeftControl)) && isCrouching && !isUncrouching) // Not holding key anymore
        {
            isTryingToUncrouch = true;
            isTryingToCrouch = false;
        }

        if (isTryingToUncrouch && CanUncrouch())
        {
            isTryingToUncrouch = false;
            isUncrouching = true;
            animator.ResetTrigger("Crouch");
            animator.SetTrigger("Uncrouch");
        }
    }

    private void HandleFlipping()
    {
        if (isFacingRight && horizontal < 0f)
        {
            isFacingRight = false;
            flipTarget.transform.localScale = Vector2.up + Vector2.right * -1;
        }
        else if (!isFacingRight && horizontal > 0f)
        {
            isFacingRight = true;
            flipTarget.transform.localScale = Vector2.one;
        }
    }

    public void SetCanLookDown(bool _canLook)
    {
        canLook = _canLook;
    }

    public void ToggleHiding(bool _isHidding)
    {
        if (IsGrounded())
        {
            isHiding = _isHidding;

            if (_isHidding)
            {
                rb.linearVelocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                colliderpl.enabled = false;
                this.enabled = false;
                sr.sortingOrder = 1;
            }
            else
            {
                this.enabled = true;
                rb.constraints = originalConstraints;
                colliderpl.enabled = true;
                sr.sortingOrder = 3;
            }
        }
    }

    public void HideSprite()
    {
        sr.enabled = false;
    }

    public void DarkenPlayer()
    {
        LeanTween.color(sr.gameObject, new Color(0f, 0f, 0f, 1f), 0.36f).setDelay(0.1f);
    }

    public void ShowSprite()
    {
        sr.enabled = true;
    }

    public void AnimateShadow()
    {
        animator.SetBool("isHiding", true);
        animator.SetTrigger("HideInShadow");
    }

    public void AnimateExitShadow()
    {
        animator.SetTrigger("ExitHideInShadow");
        Invoke("SetIsHiddingFalse", 0.9f);
    }

    public void SetIsHiddingFalse()
    {
        animator.SetBool("isHiding", false);
        ToggleHiding(false);
    }

    public bool CanHide()
    {
        return (!isAttacking && !isCrouching && !isJumping && IsGrounded() && !isGrappling && !isDead);
    }

    public bool IsHiding()
    {
        return isHiding;
    }


    public void SetIsAttacking(bool isAttacking)
    {
        rb.linearVelocity = Vector2.zero;
        this.isAttacking = isAttacking;
    }

    public void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
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

    public void SetInstantDead(bool _isDead)
    {
        isDead = _isDead;
        if (isDead)
        {
            animator.SetTrigger("Die");
            StopMovement();
            MultipleDeaths.Instance.RandomNumber();
        }
    }

    public void SetIsDead(bool _isDead)
    {
        isDead = _isDead;
        if (isDead)
        {
            animator.SetTrigger("Die");
            StopMovement();
        }
    }

    public void SetTakingDamage(bool _takingDamage)
    {
        takingDamage = _takingDamage;
        isAttacking = false;
    }

    public void Knockback(bool isRight)
    {
        if (isRight)
        {
            rb.AddForce(new Vector2(-150f, 175f), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(150f, 175f), ForceMode2D.Impulse);
        }
    }
}