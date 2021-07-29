using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshCharacterContoller : CharacterController
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    protected override void Move(Vector3 target)
    {
        var objs = GameObject.FindGameObjectsWithTag("Respawn");

        foreach (var item in objs)
        {
            item.GetComponentInChildren<NavMeshSurface>().BuildNavMesh();//bake
        }

        agent.destination = target;
    }
}
