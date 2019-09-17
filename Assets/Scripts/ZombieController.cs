using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float Speed = 1f;
    private bool _stopMoving;

    public void Update()
    {
        if (_stopMoving) 
            return;
        var target = Game.Manager.Player.transform;
        //transform.LookAt(player, Vector3.forward);
        transform.right = target.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
    }

    public void StopMoving()
    {
        _stopMoving = true;
    }        
}
