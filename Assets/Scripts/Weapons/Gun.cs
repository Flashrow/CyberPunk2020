using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Hero player;

    public ushort ammunition = 60;
    public float damage = 50f;
    public float range = 100f;

    public Camera fpsCamera;

    public ParticleSystem gunFlash;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponentInParent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (player.inGunAmmo > 0)
            {
                Shoot();
            }
            else
            {
                if (player.playerAmmo > 30)
                {
                    player.inGunAmmo = 30;
                    player.playerAmmo -= 30;
                }
                else
                {
                    player.inGunAmmo = player.playerAmmo;
                    player.playerAmmo = 0;
                }
            }
        }
    }

    void Shoot()
    {
            player.inGunAmmo -= 1;
            gunFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        AudioManager.instance.playSound("shoot"); 
        player.inGunAmmo -= 1;
        gunFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
        Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
