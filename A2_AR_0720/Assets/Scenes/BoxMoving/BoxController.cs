using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private const float cameraDistance = 7.5f; //
    private float positionY = 0.4f; // Y축 포지션
    [SerializeField] private GameObject[] obj; // 생성할 오브젝트

    private Camera mainCam; // 메인카메라
    private GameObject HoldingObj; // 손으로 잡은 오브젝트
    private Vector3 InputPos; // input posision
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
        //오브젝트 생성
        var cube = Instantiate(obj[0], pos, Quaternion.identity, mainCam.transform);
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
    }
}
