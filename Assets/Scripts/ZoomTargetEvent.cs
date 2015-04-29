using UnityEngine;
using System.Collections;

/// <summary>
/// 相机指向或指出物体的事件
/// </summary>
public interface ZoomTargetEvent {

    /// <summary>
    /// 相机指向目标点
    /// </summary>
    void ZoomInTarget(ref Transform target);

    /// <summary>
    /// 相机指出目标点
    /// </summary>
    void ZoomOutTarget(ref Transform parent);
	
}
