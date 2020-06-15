using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour {
    public float BaseHp = 1000;
    public float health = 1000;
    public int moneys = 10000;
    public string playerName = "Cyber";
    public ushort playerAmmo = 60;
    public ushort inGunAmmo = 16;

    public Inventory inventory;

    static public UnityEvent OnDieEvent = new UnityEvent();
    private State mainState;
    private MovementState movState;
    private ShootingState weaponState;


    private void Awake () {
        inventory = new Inventory ();
        OnDieEvent.AddListener(() =>
        {
            UImanager.Alert($"TODO:// Game Over !!!", 2.5f);
            Invoke("goToMenu", 2.5f);
        });
    }

    void goToMenu()
    {
        Debug.Log("GAME OVER !!!");
        //SceneManager.LoadScene("MenuStart");
    }

    public void setPlayerAmmo(ushort ammo)
    {
        this.playerAmmo = ammo;
    }

    public void setPlayerName(string name) {
        this.playerName = name;
    }

    public void setHealth(float hp) {
        this.health = hp;
    }

    public void setGunAmmo (ushort ammo) {
        this.playerAmmo = ammo;
    }

    public void setInGunAmmo(ushort inGunAmmo)
    {
        this.inGunAmmo = inGunAmmo;
    }

    public void setMoneys(int coins)
    {
        this.moneys = coins;
        BitCoinsBar.loadMoneyEvent.Invoke(coins);
    }

    public void HitPlayer (float val) {
        this.health -= val;
    }

    public State getState()
    {
        return mainState;
    }
     public void setState(State state)
    {
        mainState = state;
    }

    public void setInventory(InventorySerializable data)
    {
        inventory.slots = data.slots;
        foreach(KeyValuePair<Slots, Item> item in data.slots)
        {
            Debug.LogWarning($"{item.Key} -> {item.Value.name}");
            // TODO: ADD ITEM TO SLOT - ########KAMIL########
        }
        // TODO: ADD WEPON TO INVENTORY AND SLOT ########LUKASZ########
        inventory.items = data.items;
    }

    public MovementState getMovementState()
    {
        return movState;
    }
    public void setMovementState(MovementState state)
    {
        movState = state;
    }

    public ShootingState getShootingState()
    {
        return weaponState;
    }
    public void setShootingState(ShootingState state)
    {
        weaponState = state;
    }
}