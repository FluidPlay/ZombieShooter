using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtMouse : MonoBehaviour
{
    public float TurnSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        var clickWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        var direction =  clickWorldPos - transform.position; // target.position - transform.position;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Quaternion.AngleAxis(angle, Vector3.forward);
        var rotation = Quaternion.Euler(0f, 0f, angle);
        
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, TurnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 10f);

        //var newTargetPos = new Vector2(clickWorldPos.x, clickWorldPos.y);
        if (Input.GetMouseButton(1))
            transform.position = Vector2.MoveTowards(transform.position, clickWorldPos, 0.1f);
    }
}
