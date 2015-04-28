using UnityEngine;
using System.Collections;

public class CameraOperated3D : MonoBehaviour {

    /// <summary>
    /// 目标物体
    /// </summary>
    public GameObject target;

    /// <summary>
    /// 目标点
    /// </summary>
    private Vector3 targetVector3 = Vector3.zero;

    /// <summary>
    /// 场景缩放的默认值
    /// </summary>
    private float normalDistance = 60f;

    /// <summary>
    /// 场景缩放的最小值
    /// </summary>
    private float MouseZoomMin = 20f;

    /// <summary>
    /// 场景缩放的最大值
    /// </summary>
    private float MouseZoomMax = 100f;

    /// <summary>
    /// 鼠标滑轮的灵敏程度
    /// </summary>
    private float MouseWheelSensitivity = 15f;

    /// <summary>
    /// 场景旋转在X上的灵敏程度
    /// </summary>
    private const float X_SPEED = 250.0f;
    private const float Y_SPEED = 120.0f;
    private float x = 0.0f;
    private float y = 0.0f;
    private int yMinLimit = -45;
    private int yMaxLimit = 45;
    private float distance;

    public void Start()
    {
        if (target != null) targetVector3 = target.transform.position;

        distance = Vector3.Distance(transform.position, targetVector3);

        Vector3 rotateVector3 = transform.rotation.eulerAngles;
        x = rotateVector3.y;
        y = rotateVector3.x;
        RotatedCamera(transform, y, x, 0, distance, targetVector3);

        normalDistance = transform.camera.fieldOfView;
    }

    private float axisX;
    private float flag = 1f;
    private float gradualSpeed = 2f;
    private Vector2 gradualVector2;
    public void Update()
    {
        if (flag < 1f)
        {
            flag += Time.deltaTime * gradualSpeed;
            gradualVector2 = Vector2.Lerp(gradualVector2, Vector2.zero, flag);
            x += gradualVector2.x;
            RotatedCamera(transform, y, x, 0, distance, targetVector3);
        }

        // 旋转
        if (Input.GetMouseButton(1))
        {
            axisX = (Input.GetAxis("Mouse X") * X_SPEED * 0.02f);
            x += axisX;
            y -= (Input.GetAxis("Mouse Y") * Y_SPEED * 0.02f);

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            RotatedCamera(transform, y, x, 0, distance, targetVector3);

            if (Mathf.Abs(axisX) > 10f)
            {
                flag = 0f;
                gradualVector2 = new Vector2(axisX, 0f);
            }
        }
        // 放大与缩小
        else if(Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            normalDistance -= Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity;
            normalDistance = Mathf.Clamp(normalDistance, MouseZoomMin, MouseZoomMax);

            transform.camera.fieldOfView = normalDistance;
        }
    }

    static void RotatedCamera(Transform cameraTransform, float x, float y, float z, float distance, Vector3 target)
    {
        cameraTransform.rotation = Quaternion.Euler(new Vector3(x, y, z));
        cameraTransform.position = cameraTransform.rotation * new Vector3(0.0f, 0.0f, -distance) + target;
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
