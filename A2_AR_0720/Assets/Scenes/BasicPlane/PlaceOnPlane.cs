using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField] private ARRaycastManager rayMrg;
    [SerializeField] private GameObject obj;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return; // ��ġ ���ϸ� ����

        var touch = Input.GetTouch(0); // ��ġ 1��

        if (touch.phase != TouchPhase.Began) return; // ��ġ���°� �ٷ� ������ ��

        var hit = new List<ARRaycastHit>();
        if (rayMrg.Raycast(touch.position, hit, TrackableType.PlaneWithinBounds))
        {
            var pose = hit[0].pose; // �浹�� ù���� ������Ʈ ������
            Instantiate(obj, pose.position, pose.rotation); //������Ʈ ����
        }
    }
}
