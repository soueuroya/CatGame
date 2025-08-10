using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    PlayerInventory inventory;

    [SerializeField] private GameObject grappleHookPrefab;
    [SerializeField] private GameObject grappleRopePrefab;

    private GameObject currentGrappleHook;
    private GameObject currentGrappleRope;

    private bool isGrappling = false;
    private bool isTryingToGrapple = false;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.Grapple > 0)
        {
            if (Input.GetMouseButtonDown(1) && !isTryingToGrapple && !isGrappling)
            {
                ShootGrapple();
            }
            else if (Input.GetMouseButtonDown(1) && isTryingToGrapple)
            {
                StopGrapple();
            }
        }
    }

    public void StopGrapple()
    {
        isGrappling = false;
        isTryingToGrapple = false;
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Movement>().SetIsAiming(false);
        GetComponent<Movement>().SetIsGrappling(false);

        if (currentGrappleHook != null)
        {
            Destroy(currentGrappleHook.gameObject);
        }

        if (currentGrappleRope != null)
        {
            Destroy(currentGrappleRope.gameObject);
        }
    }

    private void ShootGrapple()
    {
        if (currentGrappleHook != null)
        {
            Destroy(currentGrappleHook.gameObject);
        }

        if (currentGrappleRope != null)
        {
            Destroy(currentGrappleRope.gameObject);
        }


        float angle = GetAngleBetweenPlayerAndCursor(transform.position);

        // Instantiate Grapple Hook
        currentGrappleHook = Instantiate(grappleHookPrefab, transform.position, Quaternion.Euler(0, 0, angle));

        GrappleHook hookScript = currentGrappleHook.GetComponent<GrappleHook>();
        hookScript.Initialize(this);

        // Instantiate Grapple Rope
        currentGrappleRope = Instantiate(grappleRopePrefab, transform.position, Quaternion.identity);

        GrappleRope ropeScript = currentGrappleRope.GetComponent<GrappleRope>();
        ropeScript.Initialize(transform, currentGrappleHook.transform);

        GetComponent<Movement>().SetIsAiming(true);
    }

    private float GetAngleBetweenPlayerAndCursor(Vector2 playerPosition)
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPosition - playerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }

    public void TryingToGrapple()
    {
        isTryingToGrapple = true;
    }

    public void Grappled()
    {
        isGrappling = true;
        isTryingToGrapple = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Movement>().Grappled();
        GetComponent<Movement>().SetIsAiming(false);
        GetComponent<Movement>().SetIsGrappling(true);
    }

    public void Ungrappled()
    {
        isTryingToGrapple = false;
        isGrappling = false;
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Movement>().SetIsAiming(false);
        GetComponent<Movement>().SetIsGrappling(false);

        if (currentGrappleHook != null)
        {
            Destroy(currentGrappleHook.gameObject);
        }

        if (currentGrappleRope != null)
        {
            Destroy(currentGrappleRope.gameObject);
        }
    }
}
