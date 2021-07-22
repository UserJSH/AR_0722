using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchHelper
{
    #region ��������
#if UNITY_EDITOR
    public static bool Touch2 => Input.GetMouseButtonDown(1); // ���콺 ������
    public static bool IsDown => Input.GetMouseButtonDown(0); // ���콺 ����
    public static bool IsUp => Input.GetMouseButtonUp(0);
    public static Vector2 TouchPos => Input.mousePosition;

#else
    public static bool Touch2 => Input.touchCount == 2 && (Input.GetTouch(1).phase == TouchPhase.Began);
    public static bool IsDown => Input.GetTouch(0).phase == TouchPhase.Began;
    public static bool IsUp => Input.GetTouch(0).phase == TouchPhase.Ended;
    public static Vector2 TouchPos => Input.GetTouch(0).position;

#endif
    #endregion
}
