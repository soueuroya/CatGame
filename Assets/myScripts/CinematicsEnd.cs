using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CinematicsEnd : MonoBehaviour
{
    public GameObject continueBtn;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowButton", delay);
    }

    public void ShowButton()
    {
        continueBtn.SetActive(true);
    }
}
