using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private bool _cameraIsDefined;
    public float MoveSpeed = 50f;
    public float TurnSpeed = 300f;
    public float SmoothFactor = 0.25f;
    
    enum axis { Horizontal, Vertical }

    private void Awake() {
        _cameraIsDefined = (Camera.main != null);
    }

    void Update() {
        if (! _cameraIsDefined)
            return;
        
        var pointedWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dt = Time.deltaTime;
        SmoothLookAtPos(transform, pointedWorldPos, TurnSpeed, SmoothFactor, dt);

        var translation = new Vector2(Input.GetAxis(axis.Horizontal + ""),
                                      Input.GetAxis(axis.Vertical + ""));
        SmoothTranslate(transform, translation, MoveSpeed, SmoothFactor, dt);
    }

    public void SmoothTranslate(Transform transf, Vector2 translation, float moveSpeed, float t, float dt) {
        if (Mathf.Approximately(translation.x, 0) && Mathf.Approximately(translation.y, 0))
            return;
        var smoothTranslation = Vector2.Lerp(Vector2.zero, translation, t);
        transf.Translate(smoothTranslation * dt * moveSpeed, Space.World);
    }

    public void SmoothLookAtPos(Transform transf, Vector3 targetPos, float turnSpeed, float t, float dt) {
        var direction = targetPos - transf.position; // target pos - origin pos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Below is the same as: Quaternion.AngleAxis(angle, Vector3.forward);
        var targetRotation = Quaternion.Euler(0f, 0f, angle);

        var smoothRotation = Quaternion.Slerp(transf.rotation, targetRotation, t);

        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, TurnSpeed * Time.deltaTime);
        transf.rotation = Quaternion.RotateTowards(transf.rotation, smoothRotation, turnSpeed * dt);
    }
    
}
