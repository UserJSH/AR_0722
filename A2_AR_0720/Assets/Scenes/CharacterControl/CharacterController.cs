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
    protected Transform destination; // ������
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
        anim.SetBool("Rest", false); //��ġ�� ���� �� Rest�� ����

        var hit = new List<ARRaycastHit>();
        raycastMng.Raycast(TouchHelper.TouchPos, hit, TrackableType.Planes);

        if (hit.Count == 0) return; // �浹�� �ȉ����� ����

        //��ġ�� ������ ������ ������ �־��ش�
        destination.transform.position = hit[0].pose.position;

        Rotate(); // ĳ���� ���� ȸ��

        Move(hit[0].pose.position); // ĳ���� �̵�
    }
    private void LateUpdate() // ��� Update�� ȣ��� �� �������Ӹ��� ����
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
        var direction = destination.position - transform.position; // ������ - ������ġ = ����
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
