using UnityEngine;
using System.Collections;

public class CameraOperated3D : MonoBehaviour {

    public GameObject target;
    private Vector3 targetVector3 = Vector3.zero;

    private float distance;

    private const float X_SPEED = 250.0f;
    private const float Y_SPEED = 120.0f;
    private float x = 0.0f;
    private float y = 0.0f;
    private int yMinLimit = -45;
    private int yMaxLimit = 45;

    public void Start()
    {
        if (target != null) targetVector3 = target.transform.position;

        distance = Vector3.Distance(transform.position, targetVector3);

        Vector3 rotateVector3 = transform.rotation.eulerAngles;
        x = rotateVector3.y;
        y = rotateVector3.x;
        transform.rotation = Quaternion.Euler(y, x, 0);
        transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -distance) + targetVector3;
    }

    public void Update()
    {
        if (Input.GetMouseButton(1))
        {
            x += (Input.GetAxis("Mouse X") * X_SPEED * 0.02f);
            y -= (Input.GetAxis("Mouse Y") * Y_SPEED * 0.02f);

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            transform.rotation = Quaternion.Euler(y, x, 0);
            transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -distance) + targetVector3;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
