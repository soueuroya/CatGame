using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private bool isHooked = false;
    private Transform hookedObject;

    public GrapplingHook Hook { get; set; }

    public void Initialize(GrapplingHook _hook)
    {
        Hook = _hook;
        Hook.TryingToGrapple();
    }

    void Update()
    {
        if (!isHooked)
        {
            // Move the hook towards its forward direction
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else if (Hook != null && hookedObject != null)
        {
            // Pull the player towards the hook
            Hook.transform.position = Vector2.MoveTowards(Hook.transform.position, transform.position, speed * Time.deltaTime);

            // Stop pulling when the player reaches the hook
            if (Vector2.Distance(Hook.transform.position, transform.position) < 50.0f)
            {
                //Hook.transform.position = transform.position;
                this.enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grapple"))
        {
            isHooked = true;
            hookedObject = collision.transform;
            Hook.Grappled();
        }
        else
        {
            Hook.StopGrapple();
        }
    }
}