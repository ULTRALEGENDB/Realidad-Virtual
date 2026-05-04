using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.BuildingBlocks.AIBlocks;
using NUnit.Framework;
public class enemyMove : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent agent;
    private bool isChasing = false;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isChasing && Player != null)
        {
            agent.SetDestination(Player.position);
            animator.SetBool("seeing", true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other.transform;
            isChasing = true;
            other.GetComponent<PlayerAudio>().SetChasing(true);
        }
    }
        void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAudio>().SetChasing(false);
        }
    }
}
