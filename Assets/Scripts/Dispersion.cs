using UnityEngine;
using System.Collections;
using System;

public class Dispersion : MonoBehaviour, ZoomTargetEvent
{
    private bool isDispersion = false;
    private int count = 0;
    // Use this for initialization

    void Start()
    {
        transform.GetComponent<CameraOperated3D>().SetZoomTargetEvent(this);
    }

    public void ZoomInTarget(ref Transform target)
    {
        try
        {
          
            if (!isDispersion)
            {
              
                if (target.parent.childCount < 5)
                {
               
                    foreach (Transform trans in target.parent)
                    {
                        count++;
                        Vector3 VEC = trans.localPosition;
                        Vector3 NEWVEC = new Vector3((VEC.x + (float)Math.Pow(-1, count)) * 2f, VEC.y * 2f, VEC.z * 2f);
                        trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, NEWVEC.x, Time.time * 2f),
                            Mathf.Lerp(trans.localPosition.y, NEWVEC.y, Time.time * 2f),
                            Mathf.Lerp(trans.localPosition.z, NEWVEC.z, Time.time * 2f));
                       
                       Debug.DrawLine(transform.TransformPoint(trans.localPosition), transform.TransformPoint(VEC));
                    }
                }
                else
                {
                    Debug.Log("1");
                    foreach (Transform trans in target.parent)
                    {
                       trans.localPosition = Vector3.Slerp(trans.localPosition, trans.localPosition * 3, Time.time * 2f);
                    }
                }
                count = 0;
                isDispersion = true;
            }
        }
<<<<<<< HEAD

=======
>>>>>>> origin/master
        catch
        {
            throw new NotImplementedException();
        }
<<<<<<< HEAD

=======
>>>>>>> origin/master
    }

    public void ZoomOutTarget(ref Transform parent)
    {
        try
        {
            if (isDispersion)
            {
                if (parent.childCount < 5)
                {
                    foreach (Transform trans in parent)
                    {
                        count++;
                        Vector3 VEC = trans.localPosition;
                        Vector3 NEWVEC = new Vector3(VEC.x / 2f - (float)Math.Pow(-1, count), VEC.y / 2f, VEC.z / 2f);
                        trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, NEWVEC.x, Time.time * 2f),
                            Mathf.Lerp(trans.localPosition.y, NEWVEC.y, Time.time * 2f),
                            Mathf.Lerp(trans.localPosition.z, NEWVEC.z, Time.time * 2f));
                    }
                }
                else
                {
                    foreach (Transform trans in parent)
                    {
                        trans.localPosition = Vector3.Slerp(trans.localPosition, trans.localPosition / 3, Time.time * 2f);
                    }
                }
                count = 0;
                isDispersion = false;
            }
        }
<<<<<<< HEAD

=======
>>>>>>> origin/master
        catch
        {
            throw new NotImplementedException();
        }
<<<<<<< HEAD

=======
>>>>>>> origin/master
    }
}
