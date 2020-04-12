using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : Item {
    [SerializeField]
    private int magazineCapacity=0;
    [SerializeField]
    private int currentlyInGunAmmo=0;

    private string spriteName;
    private string scriptName;
    //public new WeaponScriptable data;

    public Weapon()
    {

    }

    public Weapon(string _spriteName, string _scriptName) {
        spriteName = _spriteName;
        scriptName = _scriptName;
        itemId = "gun";
        Debug.Log("Weapon: Weapon");
        LoadScriptableObject(scriptName);
    }

    public override void LoadScriptableObject(string scriptName)
    {
        //this.data = Resources.Load<WeaponScriptable>("Items/Weapons/" + scriptName);
        this.data = Resources.Load<WeaponScriptable>("Items/Weapons/" + scriptName);
        Debug.Log("Weapon: id - " + this.data.itemId);
        Debug.Log("Weapon: type - " + this.data.type);
        Debug.Log("Weapon: range - " + this.getData().range);
    }

    public override Item CreateInstance()
    {
        Weapon newWeapon = new Weapon(spriteName,scriptName);
        newWeapon.cost = cost;
        return newWeapon;
    }

    public WeaponScriptable getData()
    {
        return (WeaponScriptable)data;
    }

    public bool needReload()
    {
        if (currentlyInGunAmmo == 0) return true;
        else return false;
    }

    public void setMagazineCapacity(int capacity)
    {
        magazineCapacity = capacity;
    }

    public void singleShoot()
    {
        if (currentlyInGunAmmo > 0)
        {
            currentlyInGunAmmo--;
        }
    }

    public int getInGunAmmo()
    {
        return currentlyInGunAmmo;
    }

    public int getMagazineCapacity()
    {
        return magazineCapacity;
    }

    public int reload(int ammoToReload)         // returns not loaded ammo
    {
        currentlyInGunAmmo += ammoToReload;
        if (currentlyInGunAmmo > magazineCapacity)
        {
            currentlyInGunAmmo = magazineCapacity;
            return ammoToReload - currentlyInGunAmmo;
        }
        else
        {
            return 0;
        }
    }
}
