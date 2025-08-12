using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea;
    private PlayerAnimationCallback pac;
    private Animator anim;
    private bool canAttack = true;
    [SerializeField] private float attackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = GetComponentInChildren<AttackArea>(true).gameObject;
        anim = GetComponentInChildren<Animator>();
        pac = GetComponentInChildren<PlayerAnimationCallback>();

        pac.SetDeathFinishCallback(() => {
            MultipleDeaths.Instance.RandomNumber();
        });

        pac.SetAttackStartCallback(() => {
            ToggleAttackCollision(true);
        });

        pac.SetAttackStopCallback(() => {
            ToggleAttackCollision(false);
            anim.SetBool("Attacking", false);
            Movement.Instance.SetIsAttacking(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack && !Movement.Instance.IsHidding() && Input.GetMouseButtonDown(0) && Movement.Instance.IsGrounded())
        {
            // play the animation
            anim.SetTrigger("Attack");
            anim.SetBool("Attacking", true);

            canAttack = false;
            Movement.Instance.SetIsAttacking(true);

            Invoke("AllowAttack", attackCooldown);

        }
    }

    private void AllowAttack()
    {
        canAttack = true;
    }

    public void ToggleAttackCollision(bool activate)
    {
        attackArea.SetActive(activate);
    }
}
