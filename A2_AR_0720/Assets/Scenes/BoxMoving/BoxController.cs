using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    protected const float cameraDistance = 7.5f; //
    protected float positionY = 0.4f; // Y축 포지션
    [SerializeField] protected GameObject[] obj; // 생성할 오브젝트

    protected Camera mainCam; // 메인카메라
    protected GameObject HoldingObj; // 손으로 잡은 오브젝트
    protected Vector3 InputPos; // input posision
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        init();
    }
    
    private void init()
    {
        //어느위치에 오브젝트를 생성시킬지 position을 받아옴
        var pos = mainCam.ViewportToWorldPoint(new Vector3(0.5f, positionY, mainCam.nearClipPlane * cameraDistance));

        //랜덤
        var index = UnityEngine.Random.Range(0, obj.Length);

        //오브젝트 생성
        var cube = Instantiate(obj[index], pos, Quaternion.identity, mainCam.transform);
        //RigidBody
        var rigid = cube.GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //두 손가락을 터치했을 때 오브젝트를 생성
#if !UNITY_EDITOR
        if (Input.touchCount == 0) return;
#endif
        InputPos = TouchHelper.TouchPos;

        if (TouchHelper.Touch2)
        {
            init();
        }

        //오브젝트 이동
        if (HoldingObj)
        {
            //오브젝트 놓기
            if (TouchHelper.IsUp)
            {
                OnPut(InputPos);
                HoldingObj = null;
                return;
            }

            Move(InputPos);
        }

        //오브젝트 잡기
        if (!TouchHelper.IsDown) return;

        RaycastHit hit;
        if (Physics.Raycast(mainCam.ScreenPointToRay(InputPos), out hit, mainCam.farClipPlane))
        {
            if (hit.transform.tag.Equals("Player"))
            {
                HoldingObj = hit.transform.gameObject;
                OnHold();
            }
        }
    }

    protected virtual void OnPut(Vector3 pos)
    {
        HoldingObj.GetComponent<Rigidbody>().useGravity = true;
        HoldingObj.transform.SetParent(null); // 자식해제
    }

    protected virtual void OnHold()
    {
        HoldingObj.GetComponent<Rigidbody>().useGravity = false;

        //잡은 오브젝트를 카메라의 자식으로
        HoldingObj.transform.SetParent(mainCam.transform); // 자식등록
        HoldingObj.transform.rotation = Quaternion.identity;
        HoldingObj.transform.position = mainCam.ViewportToWorldPoint(new Vector3(0.5f, positionY, mainCam.nearClipPlane * cameraDistance));
    }

    void Move(Vector3 pos)
    {
        //오브젝트 이동
        //z축을 고정
        pos.z = mainCam.nearClipPlane * cameraDistance;
        HoldingObj.transform.position = Vector3.Lerp(HoldingObj.transform.position, mainCam.ScreenToWorldPoint(pos), Time.deltaTime * 7f);
    }
}

