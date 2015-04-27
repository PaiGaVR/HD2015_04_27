using UnityEngine;
using System.Collections;

public class CameraOperated3D : MonoBehaviour {
    public Transform target;

    private Camera mainCamera;

    private int MouseWheelSensitivity = 2;
    private int MouseZoomMin = 5;
    private int MouseZoomMax = 20;
    private float normalDistance = 3;

    private Vector3 normalized;

    private float xSpeed = 250.0f;
    private float ySpeed = 120.0f;

    private int yMinLimit = 20;
    private int yMaxLimit = 80;

    private float x = 0.0f;
    private float y = 0.0f;

    private Vector3 screenPoint;
    private Vector3 offset;

    private Quaternion rotation = Quaternion.Euler(new Vector3(30f, 0f, 0f));
    private Vector3 CameraTarget = Vector3.zero;

    private bool exchangedFlag = true;

    private Vector3 cameraRotationAngles;

    void Start()
    {
        mainCamera = Camera.main;

        normalDistance = Vector3.Distance(transform.position, CameraTarget);

        transform.LookAt(CameraTarget);

        mainCamera.orthographic = false;
        mainCamera.fieldOfView = 90f;

        cameraRotationAngles = mainCamera.transform.rotation.eulerAngles;

        if (target != null)
        {
            CameraTarget = target.transform.position;
        }
        transform.LookAt(CameraTarget);
    }

    public void ChangedTarget(Transform newTarget)
    {
        CameraTarget = target.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            x += (Input.GetAxis("Mouse X") * xSpeed * 0.02f);
            y -= (Input.GetAxis("Mouse Y") * ySpeed * 0.02f);

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            RotatedCamera(y, x, 0, normalDistance);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            normalized = (transform.position - CameraTarget).normalized;

            if (normalDistance >= MouseZoomMin && normalDistance <= MouseZoomMax)
            {
                normalDistance -= Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity;
            }
            if (normalDistance < MouseZoomMin)
            {
                normalDistance = MouseZoomMin;
            }
            if (normalDistance > MouseZoomMax)
            {
                normalDistance = MouseZoomMax;
            }
            transform.position = normalized * normalDistance;

        }
    }

    private void RotatedCamera(Vector3 v, float distance)
    {
        transform.rotation = Quaternion.Euler(v);
        transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -distance) + CameraTarget;
    }

    private void RotatedCamera(float x, float y, float z, float distance)
    {
        transform.rotation = Quaternion.Euler(x, y, z);
        transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -distance) + CameraTarget;
    }

    private void RotatedCamera(Quaternion q, float distance)
    {
        transform.rotation = q;
        transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -distance) + CameraTarget;
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    public void SetTargetPosition(Vector3 position)
    {
        this.CameraTarget = position;
    }

    public void OnDestroy()
    {
        
    }
}
