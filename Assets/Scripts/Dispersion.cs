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
    
    public void ZoomInTarget(ref Transform target)
    {
        if (!isDispersion)
        {
            foreach (Transform trans in target.parent)
            {
                count++;
                Vector3 VEC = trans.localPosition;
                Vector3 NEWVEC = new Vector3((float)Math.Pow(-1, count), VEC.y * 2f, VEC.z * 2f);
                trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, NEWVEC.x, Time.time * 2f),
                    Mathf.Lerp(trans.localPosition.y, NEWVEC.y, Time.time * 2f),
                    Mathf.Lerp(trans.localPosition.z, NEWVEC.z, Time.time * 2f));
            }
            count = 0;

            isDispersion = true;
        }
        //throw new NotImplementedException();
    }

    public void ZoomOutTarget(ref Transform parent)
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
