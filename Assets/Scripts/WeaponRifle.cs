using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class WeaponRifle : MonoBehaviour {
    public GameObject Projectile;
    // Start is called before the first frame update
    public float ReloadTime = 1f;
    private float timeSinceLastShot = 0f;
    private bool shotQueued;
    public async void Fire(PlayerController controller)
    {
        if (shotQueued)
            return;
        //await FireAsync(pos, rot);
        timeSinceLastShot += Time.deltaTime;
        shotQueued = true;
        await Task.Delay(TimeSpan.FromSeconds(ReloadTime - timeSinceLastShot));
        var firePos = controller.weaponTransf.position;
        var fireAngle = controller.weaponTransf.rotation
                        * Quaternion.Euler(0f, 0f, controller.zRotationOffset);
        
        var proj = Instantiate(Projectile, firePos, fireAngle);
        proj.transform.SetParent(Game.Manager.SceneRoot);
        
        timeSinceLastShot = 0f;
        shotQueued = false;
    }

    async Task FireAsync(Vector3 pos, Quaternion rot)
    {
        var proj = Instantiate(Projectile, pos, rot);
    }
}
