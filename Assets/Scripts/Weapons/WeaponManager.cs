﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    public GameObject player;

    public Inventory inventory;

    public GameObject fpsCamera;

    public Weapon weapon = null;

    private float lastShootTime;
    private WeaponModel modelHandler;

    private enum State{readyToShoot, reloading, shooting};
    State state;

    private void Awake()
    {
        if (instance != null)
        {
           // Debug.LogError("More than one WeaponManager in the scene");
        }
        else
        {
            instance = this;
        }
        modelHandler = new WeaponModel(player.transform.Find("RightArm"));
    }

    private void Start()
    {
        AmmoAmount.UpdateAmmoUI();
    }

    private void Update()
    {
        if (weapon != null
            && inventory.slots.ContainsKey(Slots.Primary)                      
            && inventory.slots[Slots.Primary].data.type == ItemType.Rifle)
        {
           //Debug.Log("WeaponManager: Update");
            if (hasAmmo())
            {
                //Debug.Log("WeaponManager: lastShootTime add " + Time.deltaTime);
                lastShootTime += Time.deltaTime;

                if (weapon.getData().isAutomatic)                                  
                {
                    autoWeaponHandler();
                }
                else
                {
                    singleWeaponHandler();
                }
                
            }
            else if(Input.GetButtonDown("Fire1"))
            {
                UImanager.Alert($"No ammo in inventory", 1f);
               // Debug.Log("WeaponManager: no ammo in inventory");
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            UImanager.Alert($"No weapon in hand", 1f);
           // Debug.Log("WeaponManager: no weapon in hand");
        }

    }

    private bool hasAmmo()
    {
        if ((inventory.items.ContainsKey(ItemType.Ammo)
            && inventory.items[ItemType.Ammo].number > 0)
            || weapon.getInGunAmmo() > 0)
        {
            return true;
        }
        else
        {
           // Debug.Log("WeaponManager: no ammo");
            return false;
        }
    }

    private string getAmmoString()
    {
        if ((inventory.items.ContainsKey(ItemType.Ammo) && inventory.items[ItemType.Ammo].number > 0)
          || weapon.getInGunAmmo() > 0)
            return $"{weapon.getInGunAmmo()}/{inventory.items[ItemType.Ammo].number}";
        else
            return "0";
    }

    private void singleWeaponHandler()
    {
        if (state != State.reloading)
        {
            if (Input.GetButtonDown("Fire1"))
            {
               // Debug.Log("Try to shoot");
                if (!weapon.needReload())
                {
                   // Debug.Log("WeaponManager: single shot");
                    shoot();
                    lastShootTime = 0;
                }
                else
                {
                    state = State.reloading;
                    UImanager.Alert($"Reloading...", weapon.getData().reloadingTime);
                   // Debug.Log("WeaponManager: single shot - reloading");
                    inventory.items[ItemType.Ammo].number = weapon.reload(inventory.items[ItemType.Ammo].number);
                }
            }
        }
        else if (lastShootTime >= weapon.getData().reloadingTime)
        {
            state = State.readyToShoot;
        }
    }

    private void autoWeaponHandler()
    {
        if (state != State.reloading)                                           // check if reloading
        {
            if (Input.GetButton("Fire1"))                                       // wait for fire button
            {
               // Debug.Log("WeaponManager: Try to shoot");
                if (lastShootTime >= (1.0f / weapon.getData().fireRate))        // check if ready to shoot
                {
                  // Debug.Log("WeaponManager: Ready To shoot, last: " + lastShootTime + ">=" + (1.0f / weapon.getData().fireRate));
                    if (!weapon.needReload())                                   // check if there is ammo in weapon
                    {
                       // Debug.Log("WeaponManager:  shooting, last shoot:" + lastShootTime);
                        shoot();
                    }
                    else
                    {
                       // Debug.Log("WeaponManager: reloading");
                        state = State.reloading;
                        UImanager.Alert($"Reloading...", weapon.getData().reloadingTime);
                        inventory.items[ItemType.Ammo].number = weapon.reload(inventory.items[ItemType.Ammo].number);
                    }
                }
                else
                {
                   // Debug.Log("WeaponManager: fire rate waiting" + lastShootTime);
                }
            }
        }
        else if (lastShootTime >= weapon.getData().reloadingTime)
        {
            state = State.readyToShoot;
        }
        AmmoAmount.UpdateAmmoUI(getAmmoString());
    }

    private bool readyToShoot()
    {
        return true;
    }

    public void updateWeapon()
    {
        if (inventory.slots.ContainsKey(Slots.Primary))
        {
            if (inventory.slots[Slots.Primary].data.type == ItemType.Rifle)
            {
                weapon = (Weapon)inventory.slots[Slots.Primary];
                modelHandler.setModel(weapon.data.model);
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
               // Debug.Log("WeaponManager: Raycast hit");

                Instantiate(weapon.getData().impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

                findShotNPC(hit);

            }
            lastShootTime = 0;
        }
        AmmoAmount.UpdateAmmoUI(getAmmoString());
    }

    private void findShotNPC(RaycastHit hit)
    {
        var objShooted = GameObject.Find(hit.transform.name);
        try
        {
            NPCCharacter NPCShooted = (NPCCharacter)objShooted.GetComponent(typeof(NPCCharacter));
            NPCShooted.OnHit(10);
        }
        catch (NPCShootedException) { }
        catch {}
    }

}


