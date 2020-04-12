using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject player;

    public Inventory inventory;

    public GameObject fpsCamera;

    public Weapon weapon;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {  
                shoot();
        }

        try
        {
            updateWeapon();
        }catch{ }
    }
    public void updateWeapon()
    {
        if(inventory.slots[Slots.Primary].data.type == ItemType.Gun)
        {
            //Debug.Log("update weapon: " + inventory.slots[Slots.Primary].itemId);
            weapon = (Weapon)inventory.slots[Slots.Primary];
        }
    }

    public void shoot()
    {
        if (inventory.HasItem(ItemType.Ammo))
        {
            if (inventory.items[ItemType.Ammo].number > 0)
            {
                inventory.items[ItemType.Ammo].number--;
                RaycastHit hit;
                if (Physics.Raycast(fpsCamera.transform.position, 
                    fpsCamera.transform.forward, out hit, 
                    weapon.getData().range))
                {
                    weapon.getData().gunFlash.Play();
                    AudioManager.instance.playSound("shoot");
                    // TODO: NOW MUST HAVE UNICATE ID (NAME)
                    var objShooted = GameObject.Find(hit.transform.name);
                    try
                    {
                        NPCCharacter NPCShooted = (NPCCharacter)objShooted.GetComponent(typeof(NPCCharacter));
                        NPCShooted.OnHit(10);
                        Instantiate(weapon.getData().impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    }
                    catch (NPCShootedException) { }
                    catch
                    {
                        Debug.Log("YOU ARE BLIND :D");
                    }
                }
            }
        }
        
        
        
    }
}
