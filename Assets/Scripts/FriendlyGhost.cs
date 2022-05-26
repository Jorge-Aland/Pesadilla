using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyGhost : MonoBehaviour
{
    private NavMeshAgent nav;
    private Transform player;
    private Sight sight;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        sight = GetComponent<Sight>();
    }
    // Update is called once per frame
    void Update()
    {
        if (sight.playerDetected)
        {
            FollowPlayer();
        }
    }

    
    private void FollowPlayer()
    {
        nav.SetDestination(player.transform.position);
    }
}
