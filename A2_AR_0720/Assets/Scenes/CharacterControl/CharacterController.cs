using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CharacterController : MonoBehaviour
{
    private ARRaycastManager raycastMng;
    private Animator anim;
    protected Transform destination; // 목적지
    private float restTime;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1.5f;
        destination = GameObject.FindWithTag("Player").transform;
        raycastMng = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            restTime += Time.deltaTime;

            if (restTime < 3) return;
            restTime = 0;

            anim.SetBool("Rest", true);
            return;
        }

        restTime = 0;
        anim.SetBool("Rest", false); //터치가 됬을 때 Rest를 꺼줌

        var hit = new List<ARRaycastHit>();
        raycastMng.Raycast(TouchHelper.TouchPos, hit, TrackableType.Planes);

        if (hit.Count == 0) return; // 충돌이 안됬으면 리턴

        //터치된 지점을 목적지 지점에 넣어준다
        destination.transform.position = hit[0].pose.position;

        Rotate(); // 캐릭터 방향 회전

        Move(hit[0].pose.position); // 캐릭터 이동
    }
    private void LateUpdate() // 모든 Update가 호출된 후 매프레임마다 실행
    {
        var delta = Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        anim.SetFloat("Speed", delta * 100);
    }

    protected virtual void Move(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
    }

    protected virtual void Rotate()
    {
        var direction = destination.position - transform.position; // 목적지 - 현재위치 = 방향
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
