using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject target;

    public bool IsWalking { get; set; } = false;

    public void StartWalking() {
        IsWalking = true;
    }

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    void LateUpdate() {
        if(IsWalking)
            agent.SetDestination(target.transform.position);
    }
}
