using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDataScript : MonoBehaviour
{
    public void DeleteAllData()
    {
        SafePrefs.DeleteAll();
    }
}
