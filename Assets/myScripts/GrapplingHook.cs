using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private GameObject grappleHookPrefab;
    [SerializeField] private GameObject grappleRopePrefab;
    [SerializeField] private Transform playerTransform;

    private GameObject currentGrappleHook;
    private GameObject currentGrappleRope;

    private bool isGrappling = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isGrappling)
        {
            ShootGrapple();
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
    }

    private float GetAngleBetweenPlayerAndCursor(Vector2 playerPosition)
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPosition - playerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }

    public void Grappled()
    {
        isGrappling = true;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Movement>().Grappled();
    }

    public void Ungrappled()
    {
        isGrappling = false;
        GetComponent<Rigidbody2D>().simulated = true;

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
