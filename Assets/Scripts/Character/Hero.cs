using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
    public float BaseHp = 1000;
    public float health = 1000;
    public int Coins = 16000;
    public string playerName = "Cyber";
    public ushort playerAmmo = 60;
    public ushort inGunAmmo = 16;
    
    public Inventory inventory;

    private State mainState;
    private MovementState movState;
    private ShootingState weaponState;


    private void Awake () {
        inventory = new Inventory ();
    }

    public void setPlayerAmmo (ushort ammo) {
        this.playerAmmo = ammo;
    }

    public void setHealth (ushort hp) {
        this.health = hp;
    }

    public void setGunAmmo (ushort ammo) {
        this.playerAmmo = ammo;
    }

    public void HitPlayer (float val) {
        this.health -= val;
    }

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public State getState()
    {
        return mainState;
    }
     public void setState(State state)
    {
        mainState = state;
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