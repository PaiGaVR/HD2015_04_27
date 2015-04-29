using UnityEngine;
using System.Collections;
using System;

public class NewBehaviourScript : MonoBehaviour {
    private bool isDoubleClick = false;
    private bool isDispersion = false;
    private int count = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isDoubleClick)
        {
            if (!isDispersion)
            {
                foreach (Transform trans in transform)
                {
                    count++;
                    Debug.Log("before" + trans.localPosition);
                    trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, (float)Math.Pow(-1,count), Time.time),
                        Mathf.Lerp(trans.localPosition.y, trans.localPosition.y * 2f , Time.time),
                        Mathf.Lerp(trans.localPosition.z, trans.localPosition.y * 2f , Time.time));
                    Debug.Log("later" + trans.localPosition);
                }
                count = 0;

                isDispersion = true;
            }
            else
            {
                foreach (Transform trans in transform)
                {
                    trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, 0, Time.time),
                        Mathf.Lerp(trans.localPosition.y, trans.localPosition.y / 2f, Time.time),
                        Mathf.Lerp(trans.localPosition.z, trans.localPosition.y / 2f, Time.time));
                }
                isDispersion = false;
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
