using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI coinText;
    public PlayerInventory inv;
    void Start()
    {
        inv = player.GetComponent<PlayerInventory>();
    }


    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coins: " + inv.TotalCoin.ToString();
    }
}
