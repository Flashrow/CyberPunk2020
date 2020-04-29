using System;
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
    private Item ammo;

    private enum State{readyToShoot, reloading, shooting};
    State state;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene");
        }
        else
        {
            instance = this;
        }

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (weapon != null
            && inventory.slots.ContainsKey(Slots.Primary)                      
            && inventory.slots[Slots.Primary].data.type == ItemType.Gun)
        {
            if (hasAmmo())
            {
                if (weapon.getData().isAutomatic)                                  
                {
                    autoWeaponHandler();
                }
                else
                {
                    singleWeaponHandler();
                }
                lastShootTime += Time.deltaTime;
            }
            else if(Input.GetButtonDown("Fire1"))
            {
                Debug.Log("WeaponManager: no ammo in inventory");
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("WeaponManager: no weapon in hand");
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
            return false;
        }
    }

    private void singleWeaponHandler()
    {
        if (state != State.reloading)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!weapon.needReload())
                {
                    Debug.Log("WeaponManager: single shot");
                    shoot();
                    lastShootTime = 0;
                }
                else
                {
                    state = State.reloading;
                    Debug.Log("WeaponManager: single shot - reloading");
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
                if (lastShootTime >= (1.0f / weapon.getData().fireRate))        // check if ready to shoot
                {
                    if (!weapon.needReload())                                   // check if there is ammo in weapon
                    {
                        Debug.Log("WeaponManager:  shooting, last shoot:" + lastShootTime);
                        shoot();
                    }
                    else
                    {
                        state = State.reloading;
                        inventory.items[ItemType.Ammo].number = weapon.reload(inventory.items[ItemType.Ammo].number);
                    }
                    lastShootTime = 0;
                }
            }
        }
        else if (lastShootTime >= weapon.getData().reloadingTime)
        {
            state = State.readyToShoot;
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

                Instantiate(weapon.getData().impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

                findShotNPC(hit);

            }
        }
            
    }

    // TODO: NOW MUST HAVE UNICATE ID (NAME)
    private void findShotNPC(RaycastHit hit)
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


