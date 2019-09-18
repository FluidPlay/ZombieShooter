using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

public class ZombieController : MonoBehaviour {
    public float Speed = 1f;
    private bool _stopMoving;
    public float TurnSpeed = 110f;
    public float SmoothFactor = 0.2f;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;    // Needed to prevent unwanted rotation
    }

    // Update is called once per frame
    void Update()
    {
        if (_stopMoving)
            return;
        var target = Game.Manager.Player.transform;
        //transform.right = target.position - transform.position;
        transform.SmoothLookAtPos(target.position, TurnSpeed, SmoothFactor, Time.deltaTime);
        //transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        _agent.destination = target.position;
    }

    public void StopMoving()
    {
        _stopMoving = true;
    }
}
