using UnityEngine;
using UnityEngine.Events;

public class UIButton : MonoBehaviour
{

    [SerializeField] KeyCode keyCode;
    [SerializeField] UnityEvent action;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            action.Invoke();
        }
    }
}
