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
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack && !Movement.Instance.IsHidding() && Input.GetMouseButtonDown(0))
        {
            // play the animation
            anim.SetTrigger("Attack");

            canAttack = false;

            Invoke("AllowAttack", attackCooldown);

            pac.SetAttackStartCallback(() => {
                ToggleAttackCollision(true);
            });

            pac.SetAttackStopCallback(() => {
                ToggleAttackCollision(false);
            });
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
