using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        // reduce health
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Invoke("DeleteEnemy",0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DeleteEnemy()
    {
        gameObject.SetActive(false);
    }
    public void Alive() 
    {
        currentHealth = 3;
    }
}
