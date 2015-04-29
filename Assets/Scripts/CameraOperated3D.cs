using UnityEngine;
using System.Collections;

/// <summary>
/// 场景相机的控制脚本
/// </summary>
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

    /// <summary>
    /// 场景旋转在Y上的灵敏程度
    /// </summary>
    private const float Y_SPEED = 120.0f;

    /// <summary>
    /// 场景旋转的X轴上的值
    /// </summary>
    private float x = 0.0f;

    /// <summary>
    /// 场景旋转的Y轴上的值
    /// </summary>
    private float y = 0.0f;

    /// <summary>
    /// 场景旋转的Y轴的最小限值
    /// </summary>
    private int yMinLimit = -45;

    /// <summary>
    /// 场景旋转的X轴的最小限值
    /// </summary>
    private int yMaxLimit = 45;

    /// <summary>
    /// 相机与目标点的距离
    /// </summary>
    private float distance;

    public void Start()
    {
        // 获取目标点
        if (target != null) targetVector3 = target.transform.position;

        // 获取相机与目标点的距离
        distance = Vector3.Distance(transform.position, targetVector3);

        // 初始化场景的旋转
        Vector3 rotateVector3 = transform.rotation.eulerAngles;
        x = rotateVector3.y;
        y = rotateVector3.x;
        RotatedCamera(transform, y, x, 0, distance, targetVector3);

        // 获取场景缩放的大小
        normalDistance = transform.camera.fieldOfView;
    }

    /// <summary>
    /// 记录场景旋转的X轴上的变化值
    /// </summary>
    private float axisX;

    /// <summary>
    /// 旋转惯性的控制变量
    /// </summary>
    private float flag = 1f;

    /// <summary>
    /// 旋转惯性的控制变量变化速度
    /// </summary>
    private float gradualSpeed = 2f;

    /// <summary>
    /// 用来辅助计算旋转惯性的变量
    /// </summary>
    private Vector2 gradualVector2;

    public void Update()
    {
        // 惯性旋转计算
        if (flag < 1f)
        {
            flag += Time.deltaTime * gradualSpeed;
            gradualVector2 = Vector2.Lerp(gradualVector2, Vector2.zero, flag);
            x += gradualVector2.x;
            RotatedCamera(transform, y, x, 0, distance, targetVector3);
        }

        // 场景旋转
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
        // 场景的放大与缩小
        else if(Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            normalDistance -= Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity;
            normalDistance = Mathf.Clamp(normalDistance, MouseZoomMin, MouseZoomMax);

            transform.camera.fieldOfView = normalDistance;
        }
    }

    /// <summary>
    /// 场景旋转的函数
    /// </summary>
    /// <param name="cameraTransform">相机的Transform</param>
    /// <param name="x">相机旋转的X轴的值</param>
    /// <param name="y">相机旋转的Y轴的值</param>
    /// <param name="z">相机旋转的Z轴的值</param>
    /// <param name="distance">相机距离目标点的距离</param>
    /// <param name="target">目标点</param>
    static void RotatedCamera(Transform cameraTransform, float x, float y, float z, float distance, Vector3 target)
    {
        cameraTransform.rotation = Quaternion.Euler(new Vector3(x, y, z));
        cameraTransform.position = cameraTransform.rotation * new Vector3(0.0f, 0.0f, -distance) + target;
    }

    /// <summary>
    /// 场景旋转在场景Y轴的限值函数
    /// </summary>
    /// <param name="angle">场景旋转在场景Y轴的值</param>
    /// <param name="min">最小限值</param>
    /// <param name="max">最大限值</param>
    /// <returns></returns>
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
