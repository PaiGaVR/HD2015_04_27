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
                    Vector3 VEC = trans.localPosition;
                    Vector3 NEWVEC = new Vector3((float)Math.Pow(-1, count), VEC.y * 2f, VEC.z *2f);
                    Debug.Log("NEWVEC" + NEWVEC);
                    trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, NEWVEC.x, Time.time),
                        Mathf.Lerp(trans.localPosition.y, NEWVEC.y, Time.time),
                        Mathf.Lerp(trans.localPosition.z, NEWVEC.z, Time.time));
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
