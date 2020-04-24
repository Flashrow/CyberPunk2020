using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject player;

    public Inventory inventory;

    public GameObject fpsCamera;

    public Weapon weapon = null;

    private float lastShoot;
    private bool reloading = false;
    private Item ammo;

    private void Start()
    {
        
    }

    private void Update()
    {
        try
        {
            updateWeapon();         // TODO: call it only when items in hand are changing
        }
        catch { }

        if (weapon != null
            && inventory.slots.ContainsKey(Slots.Primary)                      // has weapon in hand
            && inventory.slots[Slots.Primary].data.type == ItemType.Gun)
        {
            
            if (weapon.getData().isAutomatic)                                  // auto
            {
                autoWeaponHandler();
            }
            else
            {
                singleWeaponHandler();
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("WeaponManager: no weapon in hand");
        }

    }

    private bool hasAmmo()
    {
        if (inventory.items[ItemType.Ammo].number > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void singleWeaponHandler()
    {
        //Debug.Log("WeaponManager: single shooting waiting for Fire1 down");
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("WeaponManager: single shot - checking gun");
            if (!weapon.needReload())
            {
                Debug.Log("WeaponManager: single shot");
                shoot();
            }
            else
            {
                Debug.Log("WeaponManager: single shot - reloading");
                inventory.items[ItemType.Ammo].number = weapon.reload(inventory.items[ItemType.Ammo].number);
            }
        }
    }

    private void autoWeaponHandler()
    {
        if (Input.GetButton("Fire1"))                              // wait for fire button
        {
            if(lastShoot >= (1 / weapon.getData().fireRate))    // check if ready to shoot
            {
                if (!weapon.needReload())                       // check if there is ammo in weapon
                {
                    Debug.Log("WeaponManager:  shooting");
                    shoot();
                }
                else
                {
                    inventory.items[ItemType.Ammo].number = weapon.reload(inventory.items[ItemType.Ammo].number);
                }
                lastShoot = 0;
            }
            else
            {
                lastShoot += Time.deltaTime;
            }
            
        }
    }

    private bool readyToShoot()
    {
        return true;
    }

    public void updateWeapon()
    {
        if (inventory.slots.ContainsKey(Slots.Primary))
        {
            if (inventory.slots[Slots.Primary].data.type == ItemType.Gun)
            {
                //Debug.Log("update weapon: " + inventory.slots[Slots.Primary].itemId);
                weapon = (Weapon)inventory.slots[Slots.Primary];
            }
            else
            {
                weapon = null;
            }
        }
    }

    public void shoot()
    {
        if(weapon.singleShoot())
        {
            AudioManager.instance.playSound("shoot");
            RaycastHit hit;

            if (Physics.Raycast(fpsCamera.transform.position,
                fpsCamera.transform.forward, out hit,
                weapon.getData().range))
            {
                Debug.Log("WeaponManager: Raycast hit");

                //weapon.getData().gunFlash.Play();

                Instantiate(weapon.getData().impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                //Instantiate(weapon.getData().gunFlash, fpsCamera.transform);
                //Instantiate(weapon.getData().gunFlash);

                findShootedNPC(hit);

            }
        }
            
    }

    // TODO: NOW MUST HAVE UNICATE ID (NAME)
    private void findShootedNPC(RaycastHit hit)
    {
        var objShooted = GameObject.Find(hit.transform.name);
        try
        {
            NPCCharacter NPCShooted = (NPCCharacter)objShooted.GetComponent(typeof(NPCCharacter));
            NPCShooted.OnHit(10);
        }
        catch (NPCShootedException) { }
        catch
        {
            Debug.Log("YOU ARE BLIND :D");
        }
    }

}


