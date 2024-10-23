using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI coinText;

    // Update is called once per frame
    void Update()
    {
        PlayerInventory inv = player.GetComponent<PlayerInventory>();
        coinText.text = "Coins: " + inv.TotalCoin.ToString();
    }
}
