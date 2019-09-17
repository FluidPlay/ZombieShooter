using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed = 50f;
    public float TurnSpeed = 300f;
    private bool _cameraIsDefined;
    public float SmoothFactor = 0.25f;

    enum axis { Horizontal, Vertical }
    
    private void Awake()
    {
        _cameraIsDefined = (Camera.main != null);
    }

    void Update()
    {
        if (!_cameraIsDefined)
            return;

        //Game.Manager.Lives -= 1;
        
        var pointedWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dt = Time.deltaTime;

        SmoothLookAtPos(transform, pointedWorldPos, TurnSpeed, SmoothFactor, dt);
        
        // Criar método SmoothTranslate
        
        var translation = new Vector2(Input.GetAxis(axis.Horizontal+""), Input.GetAxis(axis.Vertical+""));

        SmoothTranslate(transform, translation, MoveSpeed, SmoothFactor, dt);

//        if (Input.GetMouseButton(1)) // 0 = left, 1 = right, 2 = middle
//            transform.position = Vector2.MoveTowards(transform.position, pointedWorldPos, MoveSpeed * dt);
    }

    private void SmoothTranslate(Transform transf, Vector2 translation, float moveSpeed, float smoothFactor, float dt)
    {
        if (Mathf.Approximately(translation.x , 0f ) && Mathf.Approximately(translation.y, 0f) )
            return;

        var smoothTranslation = Vector2.Lerp(Vector2.zero, translation, smoothFactor);

        transf.Translate(smoothTranslation * dt * moveSpeed, Space.World);
    }

    private void SmoothLookAtPos(Transform transf, Vector3 pointedWorldPos, float turnSpeed, float t, float dt)
    {
        var direction = pointedWorldPos - transf.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        var targetRotation = Quaternion.Euler(0f, 0f, angle);

        var smoothRotation = Quaternion.Slerp(transf.rotation, targetRotation, t);

        transf.rotation = Quaternion.RotateTowards(transf.rotation, smoothRotation, turnSpeed * dt);
    }
}
