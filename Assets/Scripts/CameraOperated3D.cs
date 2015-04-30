using UnityEngine;
using System.Collections;

/// <summary>
/// 场景相机的控制脚本
/// </summary>
public class CameraOperated3D : MonoBehaviour
{

    /// <summary>
    /// 目标物体
    /// </summary>
    public GameObject target;

    /// <summary>
    /// 当相机目标点指向或指出该物体时，调用的事件
    /// </summary>
    private ZoomTargetEvent zoomTargetEvent;

   
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
        if (target != null)
        {
            targetVector3 = target.transform.position;
            newTarget = target.transform;
        }

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
    private float cameraRotatedFlag = 1f;

    /// <summary>
    /// 旋转惯性的控制变量变化速度
    /// </summary>
    private float gradualSpeed = 2f;

    /// <summary>
    /// 相机中的鼠标位置碰撞点
    /// </summary>
    private RaycastHit cameraHit;

    /// <summary>
    /// 移动惯性的控制变量
    /// </summary>
    private float cameraMoveFlag = 1f;

    /// <summary>
    /// 相机的新目标点
    /// </summary>
    private Transform newTarget;

    /// <summary>
    /// 用来辅助计算移动惯性的变量
    /// </summary>
    private float gradualMoveSpeed = 2f;

    public void Update()
    {

#if UNITY_STANDALONE_WIN

        // 惯性旋转计算
        if (cameraRotatedFlag < 1f)
        {
            cameraRotatedFlag += Time.deltaTime * gradualSpeed;
            axisX = Mathf.Lerp(axisX, 0, cameraRotatedFlag);
            x += axisX;
            RotatedCamera(transform, y, x, 0, distance, targetVector3);
        }

        // 相机目标点移动的计算
        if (cameraMoveFlag < 1f)
        {
            cameraMoveFlag += Time.deltaTime * gradualMoveSpeed;
            targetVector3 = Vector3.Lerp(targetVector3, newTarget.position, cameraMoveFlag);

            // 场景的旋转变化
            Vector3 rotateVector3 = transform.rotation.eulerAngles;
            x = rotateVector3.y;
            y = rotateVector3.x;
            RotatedCamera(transform, y, x, 0, distance, targetVector3);
        }

        // 鼠标双击事件
        if (Input.GetMouseButtonDown(0))
        {
            // 如果获取到碰撞物体，则相机目标物体指向该碰撞物体；否则，相机目标物体指向父物体
            if (Physics.Raycast(transform.camera.ScreenPointToRay(Input.mousePosition), out cameraHit))
            {
                newTarget = cameraHit.transform;
                if (zoomTargetEvent != null) zoomTargetEvent.ZoomInTarget(ref newTarget);
                zoomTargetEvent.ZoomInTarget(ref newTarget);
                cameraMoveFlag = 0f;
            }
            else if (newTarget.parent != null && !"Scane".Equals(newTarget.parent.name))
            {
                newTarget = newTarget.parent;
                if (zoomTargetEvent != null) zoomTargetEvent.ZoomOutTarget(ref newTarget);
                zoomTargetEvent.ZoomOutTarget(ref newTarget);
                cameraMoveFlag = 0f;
            }
            mouseDoubleClick = false;
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
                cameraRotatedFlag = 0f;
            }
        }
        // 场景的放大与缩小
        else if(Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            normalDistance -= Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity;
            normalDistance = Mathf.Clamp(normalDistance, MouseZoomMin, MouseZoomMax);

            transform.camera.fieldOfView = normalDistance;
        }
# elif UNITY_ANDROID

# endif
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

    /// <summary>
    /// 鼠标双击事件控制变量
    /// </summary>
    private bool mouseDoubleClick = false;

    /// <summary>
    /// 用来记录事件
    /// </summary>
    private Event mouseEvent;

    public void OnGUI()
    {
        mouseEvent = Event.current;

#if UNITY_STANDALONE_WIN
        if (mouseEvent.isMouse && mouseEvent.type == EventType.MouseDown && mouseEvent.clickCount == 2)
        {
            mouseDoubleClick = true;
        }
# elif UNITY_ANDROID

# endif
    }

    /// <summary>
    /// 注册相机指向或指出物体的事件
    /// </summary>
    /// <param name="zoomTargetEvent">相机指向或指出物体的事件</param>
    public void SetZoomTargetEvent(ZoomTargetEvent zoomTargetEvent)
    {
        this.zoomTargetEvent = zoomTargetEvent;
    }

    /// <summary>
    /// 获取相机指向或指出物体的事件
    /// </summary>
    /// <returns>相机指向或指出物体的事件</returns>
    public ZoomTargetEvent GetZoomTargetEvent()
    {
        return this.zoomTargetEvent;
    }
}
