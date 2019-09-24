using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class WeaponRifle : MonoBehaviour {
    public GameObject Projectile;
    // Start is called before the first frame update
    public float ReloadTime = 1f;
    private float lastFireTime = 0f;
    private bool shotQueued;
    public async void Fire(PlayerController controller)
    {
        if (shotQueued)
            return;
        //await FireAsync(pos, rot);
        var reloadTimeLeft = (ReloadTime + lastFireTime) - Time.time; //Ex: 1 + 2 - 2.5 = 0.5
        reloadTimeLeft = Mathf.Max(0f, reloadTimeLeft);
        shotQueued = true;
        await Task.Delay(TimeSpan.FromSeconds(reloadTimeLeft));
        var firePos = controller.weaponTransf.position;
        var fireAngle = controller.weaponTransf.rotation
                        * Quaternion.Euler(0f, 0f, controller.zRotationOffset);
        var proj = Instantiate(Projectile, firePos, fireAngle);
        lastFireTime = Time.time;
        shotQueued = false;
    }

    async Task FireAsync(Vector3 pos, Quaternion rot)
    {
        var proj = Instantiate(Projectile, pos, rot);
    }
}
