using UnityEngine;

public class Saws : MonoBehaviour
{
    public float rotatespeed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,rotatespeed);
       
    }
}
