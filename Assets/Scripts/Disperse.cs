using UnityEngine;
using System.Collections;
using System;

public class Disperse : MonoBehaviour
{
    private GameObject gameO;
    private int count = 0;

    void Start()
    {
        foreach (Transform trans in transform.parent)
        {
            if (trans.name == "GameObject")
            {
                gameO = trans.gameObject;
            }
        }
        if (gameO != null)
        {
            gameO.GetComponent<DispersionController>().Dispersion += Dis;
            gameO.GetComponent<DispersionController>().Aggregation += Agg;
        }
    }

    void Dis(GameObject ga)
    {
        foreach (Transform trans in ga.transform)
        {
            count++;
            trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, (-1) * count, Time.time),
                Mathf.Lerp(trans.localPosition.y, trans.localPosition.y * 2f, Time.time),
                Mathf.Lerp(trans.localPosition.z, trans.localPosition.y * 2f, Time.time));
        }
        count = 0;
    }

    void Agg(GameObject ga)
    {
        foreach (Transform trans in ga.transform)
        {
            trans.localPosition = new Vector3(Mathf.Lerp(trans.localPosition.x, 0, Time.time),
                Mathf.Lerp(trans.localPosition.y, trans.localPosition.y / 2f, Time.time),
                Mathf.Lerp(trans.localPosition.z, trans.localPosition.y / 2f, Time.time));
        }
    }
}

