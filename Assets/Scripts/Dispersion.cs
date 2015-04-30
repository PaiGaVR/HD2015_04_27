using UnityEngine;
using System.Collections;
using System;

public class Dispersion : MonoBehaviour,ZoomTargetEvent
{
    private bool isDoubleClick = false;
    private bool isDispersion = false;
    private int count = 0;
    // Use this for initialization
  
    void Start()
    {
        transform.GetComponent<CameraOperated3D>().SetZoomTargetEvent(this);
    }
    //// Update is called once per frame
    //void Update()
    //{
    //    if (isDoubleClick)
    //    {
    //        if (!isDispersion)
    //        {
    //            foreach (Transform trans in transform)
    //            {
    //                count++;
    //                Debug.Log("before" + trans.localPosition);
    //                Vector3 VEC = trans.localPosition;
    //                Vector3 NEWVEC = new Vector3((float)Math.Pow(-1, count), VEC.y * 2f, VEC.z * 2f);
    //                Debug.Log("NEWVEC" + NEWVEC);
    //                trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, NEWVEC.x, Time.time),
    //                    Mathf.Lerp(trans.localPosition.y, NEWVEC.y, Time.time),
    //                    Mathf.Lerp(trans.localPosition.z, NEWVEC.z, Time.time));
    //                Debug.Log("later" + trans.localPosition);
    //            }
    //            count = 0;

    //            isDispersion = true;
    //        }
    //        if (isDispersion)
    //        {
    //            foreach (Transform trans in transform)
    //            {
    //                Vector3 VEC = trans.localPosition;
    //                Vector3 NEWVEC = new Vector3(0f, VEC.y / 2f, VEC.z / 2f);
    //                trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, NEWVEC.x, Time.time),
    //                    Mathf.Lerp(trans.localPosition.y, NEWVEC.y, Time.time),
    //                    Mathf.Lerp(trans.localPosition.z, NEWVEC.z, Time.time));
    //            }
    //            isDispersion = false;
    //        }

    //        isDoubleClick = false;
    //    }
    //}

    //void OnGUI()
    //{
    //    Event Mouse = Event.current;
    //    if (Mouse.isMouse && Mouse.type == EventType.MouseDown && Mouse.clickCount == 2)
    //    {
    //        isDoubleClick = true;
    //    }
    //}


    public void ZoomInTarget(ref Transform target)
    {
        if (!isDispersion)
        {
            foreach (Transform trans in target.parent)
            {
                count++;
                Debug.Log("before" + trans.localPosition);
                Vector3 VEC = trans.localPosition;
                Vector3 NEWVEC = new Vector3((float)Math.Pow(-1, count), VEC.y * 2f, VEC.z * 2f);
                Debug.Log("NEWVEC" + NEWVEC);
                trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, NEWVEC.x, Time.time * 2f),
                    Mathf.Lerp(trans.localPosition.y, NEWVEC.y, Time.time * 2f),
                    Mathf.Lerp(trans.localPosition.z, NEWVEC.z, Time.time * 2f));
                Debug.Log("later" + trans.localPosition);
            }
            count = 0;

            isDispersion = true;
        }
        //throw new NotImplementedException();
    }

    public void ZoonOutTarget(ref Transform parent)
    {
        if (isDispersion)
        {
            foreach (Transform trans in parent)
            {
                Vector3 VEC = trans.localPosition;
                Vector3 NEWVEC = new Vector3(0f, VEC.y / 2f, VEC.z / 2f);
                trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, NEWVEC.x, Time.time * 2f),
                    Mathf.Lerp(trans.localPosition.y, NEWVEC.y, Time.time * 2f),
                    Mathf.Lerp(trans.localPosition.z, NEWVEC.z, Time.time * 2f));
            }
            isDispersion = false;
        }
        //throw new NotImplementedException();
    }
}
