using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponRifle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Projectile;
    public float zRotationOffset = -90f;
    private float timeSinceLastShot = 0f;
    public float ReloadTime = 1f;
    private bool shotQueued;

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    public async void Fire(PlayerController controller)
    {
        if (shotQueued)
            return;
        shotQueued = true;
        // 1 - 2 = 0.5s
        var waitTime = TimeSpan.FromSeconds(Mathf.Max(0f, ReloadTime - timeSinceLastShot));
        await Task.Delay(waitTime);
        
        var firePos = controller.ShotSpawnPoint.position;
        var fireAngle = controller.transform.rotation 
                        * Quaternion.Euler(0f, 0f, zRotationOffset);
        Instantiate(Projectile, firePos, fireAngle);

        timeSinceLastShot = 0f;
        shotQueued = false;
    }
}
