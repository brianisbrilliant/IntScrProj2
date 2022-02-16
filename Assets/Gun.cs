using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // public variables
    public Rigidbody bulletPrefab;
    public Transform bulletSpawn;

    // attributes are awesome, really
    [Range(10,100)]
    public float bulletSpeed = 50;

    public bool debug = false;

    // private variables
    public int totalAmmo = 30;
    public int clipSize = 10;
    public int clip = 0;

    // 1. we're allowed to burn our ammo by pressing R repeatedly.
    // 2. we lose the whole clip if we reload a partial clip.

    public void Reload() {
        if(clip == clipSize) {
            if(debug) Debug.Log("Clip is already full.");
            return;
        }

        if(totalAmmo + clip >= clipSize) {          // if 90 > 10
            totalAmmo -= (clipSize - clip);          // 90 - 10 = 80
            clip = clipSize;                // clip = 10
        } else {
            // throw the rest of the ammo into the clip
            clip = totalAmmo + clip;
            totalAmmo = 0;
        }

        
    }

    public void Fire() {
        
        if(clip > 0) {
            if(debug) Debug.Log("Pow!");
            clip -= 1;
            // create a copy of the bullet prefab
            Rigidbody bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            // move the bullet in front of the gun
            bullet.transform.Translate(0,0,1);
            // add forward force to the bullet
            bullet.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
        } else {
            if(debug) Debug.Log("Out of Ammo!");
        }
    }
    
}
