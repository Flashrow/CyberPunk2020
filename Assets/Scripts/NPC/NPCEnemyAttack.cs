using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemyAttack : MonoBehaviour {
    public float Area = 30f;
    public Transform WPX;
    public float ShootGapSec = 0.15f;
    public GameObject AmmoType;
    private float timer = 0f;
    public void ShootToPlayer (float distance) {
        timer += Time.deltaTime;
        if (timer < ShootGapSec) return;
        timer = 0f;
        Instantiate (AmmoType, WPX.position, WPX.transform.rotation);
        AudioManager.instance.playSound ("shoot");
    }
}