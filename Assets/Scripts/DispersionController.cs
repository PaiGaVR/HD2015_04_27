using UnityEngine;
using System.Collections;
using System;

public class DispersionController : MonoBehaviour
{
    private bool isDispersion = false;
    public delegate void EventHandler(GameObject e);
    public event EventHandler Dispersion;
    public event EventHandler Aggregation;
    private bool isDoubleClick = false;
    // Use this for initialization
    void Start()
    {
      
    }

    void Update()
    {
        if (isDoubleClick)
        {
            Debug.Log("1");
            if (!isDispersion)
            {
                Debug.Log("2");
                if (Dispersion != null)
                {
                    Debug.Log("3");
                    Dispersion(this.gameObject);
                }
                isDispersion = true;
            }
            else
            {
                if (Dispersion != null)
                {
                    Aggregation(this.gameObject);
                }
            }

            isDoubleClick = false;
        }
    }

    void OnGUI()
    {
        Event Mouse = Event.current;
        if (Mouse.isMouse && Mouse.type == EventType.MouseDown && Mouse.clickCount == 2)
        {
            isDoubleClick = true;
        }
    }
}