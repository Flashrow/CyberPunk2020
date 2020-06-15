using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    public GameObject fpsCamera;

    public Weapon weapon = null;

    private float lastShootTime;

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
        AmmoAmount.UpdateAmmoUI();
    }

    private void Update()
    {
        if (weapon != null
            && PlayerManager.Instance.HeroScript.inventory.slots.ContainsKey(Slots.Primary)                      
            && PlayerManager.Instance.HeroScript.inventory.slots[Slots.Primary].data.type == ItemType.Gun)
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
                UImanager.Alert($"No ammo in inventory", 1f);
                //Debug.Log("WeaponManager: no ammo in inventory");
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            UImanager.Alert($"No weapon in hand", 1f);
            //Debug.Log("WeaponManager: no weapon in hand");
        }

    }

    private bool hasAmmo()
    {
        if ((PlayerManager.Instance.HeroScript.inventory.items.ContainsKey(ItemType.Ammo)
            && PlayerManager.Instance.HeroScript.inventory.items[ItemType.Ammo].number > 0)
            || weapon.getInGunAmmo() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private string getAmmoString()
    {
        if ((PlayerManager.Instance.HeroScript.inventory.items.ContainsKey(ItemType.Ammo) && PlayerManager.Instance.HeroScript.inventory.items[ItemType.Ammo].number > 0)
          || weapon.getInGunAmmo() > 0)
            return $"{weapon.getInGunAmmo()}/{PlayerManager.Instance.HeroScript.inventory.items[ItemType.Ammo].number}";
        else
            return "0";
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
                    UImanager.Alert($"Reloading...", weapon.getData().reloadingTime);
                    //Debug.Log("WeaponManager: single shot - reloading");
                    PlayerManager.Instance.HeroScript.inventory.items[ItemType.Ammo].number = weapon.reload(PlayerManager.Instance.HeroScript.inventory.items[ItemType.Ammo].number);
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
                        UImanager.Alert($"Reloading...", weapon.getData().reloadingTime);
                        PlayerManager.Instance.HeroScript.inventory.items[ItemType.Ammo].number = weapon.reload(PlayerManager.Instance.HeroScript.inventory.items[ItemType.Ammo].number);
                    }
                    lastShootTime = 0;
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
        if (PlayerManager.Instance.HeroScript.inventory.slots.ContainsKey(Slots.Primary))
        {
            if (PlayerManager.Instance.HeroScript.inventory.slots[Slots.Primary].data.type == ItemType.Gun)
            {
                weapon = (Weapon)PlayerManager.Instance.HeroScript.inventory.slots[Slots.Primary];
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


