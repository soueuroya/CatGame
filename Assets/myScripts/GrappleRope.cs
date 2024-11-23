using UnityEngine;

public class GrappleRope : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ropeRenderer;

    private Transform playerTransform;
    private Transform hookTransform;

    public void Initialize(Transform player, Transform hook)
    {
        playerTransform = player;
        hookTransform = hook;
    }

    void Update()
    {
        if (playerTransform == null || hookTransform == null) return;

        // Set position to the midpoint between player and hook
        transform.position = (playerTransform.position + hookTransform.position) / 2f;

        // Adjust rotation to match the angle
        Vector2 direction =  playerTransform.position - hookTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Adjust length and tiling
        float distance = Vector2.Distance(playerTransform.position, hookTransform.position);
        //transform.localScale = new Vector3(distance, 1, 1);

        // Update the tiling of the sprite
        ropeRenderer.size = new Vector2(distance/4, 1);
    }
}