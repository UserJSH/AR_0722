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
        float speed = 0.5f; // �ʴ� 0.5m �̵�
        var distance = Vector3.Distance(transform.position, target);

        transform.DOMove(target, distance / speed);
        //transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
    }

    protected override void Rotate()
    {
        var direction = destination.position - transform.position; // ������ - ������ġ = ����
        direction.y = 0;
        transform.DORotateQuaternion(Quaternion.LookRotation(direction), 0.5f);
        //transform.rotation = Quaternion.LookRotation(direction);
    }
}
