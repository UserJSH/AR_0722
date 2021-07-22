using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private const float cameraDistance = 7.5f; //
    private float positionY = 0.4f; // Y�� ������
    [SerializeField] private GameObject[] obj; // ������ ������Ʈ

    private Camera mainCam; // ����ī�޶�
    private GameObject HoldingObj; // ������ ���� ������Ʈ
    private Vector3 InputPos; // input posision
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        init();
    }
    
    private void init()
    {
        //�����ġ�� ������Ʈ�� ������ų�� position�� �޾ƿ�
        var pos = mainCam.ViewportToWorldPoint(new Vector3(0.5f, positionY, mainCam.nearClipPlane * cameraDistance));
        //������Ʈ ����
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
        //�� �հ����� ��ġ���� �� ������Ʈ�� ����
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
