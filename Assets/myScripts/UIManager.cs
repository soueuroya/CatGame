using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite emptysoul;
    public Sprite fullsoul;
    public static UIManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void UpdateHearts(int health)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullsoul;
            }
            else
            {
                hearts[i].sprite = emptysoul;
            }
        }
    }
}
