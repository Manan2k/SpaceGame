using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour
{
    public Transform player;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}
