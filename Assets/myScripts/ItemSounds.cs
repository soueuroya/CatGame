using UnityEngine;

public class ItemSounds : MonoBehaviour
{
    private AudioSource audioSource;
    private BoxCollider2D itemCollider;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        itemCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            audioSource.Play();
            itemCollider.enabled = false;
            sr.enabled = false;
        }
    }
}
