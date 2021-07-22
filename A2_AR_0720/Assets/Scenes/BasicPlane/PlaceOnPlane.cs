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
        if (Input.touchCount == 0) return; // 터치 안하면 리턴

        var touch = Input.GetTouch(0); // 터치 1개

        if (touch.phase != TouchPhase.Began) return; // 터치상태가 바로 눌렸을 때

        var hit = new List<ARRaycastHit>();
        if (rayMrg.Raycast(touch.position, hit, TrackableType.PlaneWithinBounds))
        {
            var pose = hit[0].pose; // 충돌된 첫번재 오브젝트 가져옴
            Instantiate(obj, pose.position, pose.rotation); //오브젝트 생성
        }
    }
}
