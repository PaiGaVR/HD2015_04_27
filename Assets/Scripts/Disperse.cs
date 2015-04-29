using UnityEngine;
using System.Collections;

public class Disperse : MonoBehaviour
{
    private GameObject gameO;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
       
	}

   void MouseDoubelCheck()
    {
        foreach (Transform trans in gameO.transform)
        {
            if (trans.localPosition.y < 0 && trans.localPosition.z < 0)
            {
                trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, -1f, Time.time), Mathf.Lerp(trans.localPosition.y, trans.localPosition.y * 2f, Time.time), Mathf.Lerp(trans.localPosition.z, trans.localPosition.y * 2f, Time.time));
            }
            if (trans.localPosition.y < 0 && trans.localPosition.z > 0)
            {
                trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, 1f, Time.time), Mathf.Lerp(trans.localPosition.y, trans.localPosition.y * 2f, Time.time), Mathf.Lerp(trans.localPosition.z, trans.localPosition.y * 2f, Time.time));
            }

            if (trans.localPosition.y > 0 && trans.localPosition.z > 0)
            {
                trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, 1f, Time.time), Mathf.Lerp(trans.localPosition.y, trans.localPosition.y * 2f, Time.time), Mathf.Lerp(trans.localPosition.z, trans.localPosition.y * 2f, Time.time));
            }
            if (trans.localPosition.y > 0 && trans.localPosition.z < 0)
            {
                trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, -1f, Time.time), Mathf.Lerp(trans.localPosition.y, trans.localPosition.y * 2f, Time.time), Mathf.Lerp(trans.localPosition.z, trans.localPosition.y * 2f, Time.time));
            }
        }
    }
}
