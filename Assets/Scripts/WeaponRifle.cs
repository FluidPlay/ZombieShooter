using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRifle : MonoBehaviour {
    public GameObject Projectile;
    // Start is called before the first frame update

    public void Fire(Vector3 pos, Quaternion rot)
    {
        var proj = Instantiate(Projectile, pos, rot);
        //proj.transform.Translate(0f,0f, Speed);
    }
}
