using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenCharacterController : CharacterController
{
    protected override void Move(Vector3 target)
    {
        // speed = time / distance
        // time = distance / speed
        float speed = 0.5f; // 초당 0.5m 이동
        var distance = Vector3.Distance(transform.position, target);

        transform.DOMove(target, distance / speed);
        //transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
    }

    protected override void Rotate()
    {
        var direction = destination.position - transform.position; // 목적지 - 현재위치 = 방향
        direction.y = 0;
        transform.DORotateQuaternion(Quaternion.LookRotation(direction), 0.5f);
        //transform.rotation = Quaternion.LookRotation(direction);
    }
}
