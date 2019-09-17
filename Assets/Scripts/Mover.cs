using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class Mover : MonoBehaviour {
    public float Speed = 1f;
    public Vector3 MoveVector = Vector3.forward;
    private bool _stopMoving;

    private List<int> inteiros = new List<int> {20, 30, 40, 62};
    private ArrayList elementos = new ArrayList { 20, "hoje", 40, 62 }; 

    public Mover(float _speed)
    {
        Speed = _speed;
    }

    public void Update()
    {
        if (_stopMoving) 
            return;
        transform.Translate(MoveVector * Speed * Time.deltaTime);
    }

    public void StopMoving()
    {
        _stopMoving = true;
    }
}
