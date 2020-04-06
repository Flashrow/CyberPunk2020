using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemyAttack : MonoBehaviour {
    public int Luck;
    public float MinDamage;
    public float MaxDamage;
    public float Area = 30f;
    public GameObject ImpactEffect;
    public GameObject ShootPrefab;
    public Transform WPX;
    public float ShootGapSec = 0.25f;
    private float timer = 0f;
    void Start () {
        if (Luck < 0 || Luck > 100) Luck = 50;
    }
    public void ShootToPlayer (float distance) {
        //TODO: AUDIO
        timer += Time.deltaTime;
        if(timer < ShootGapSec) return;
        timer = 0f;
        RaycastHit hit;
        GameObject shoot = Instantiate(ShootPrefab, WPX.position, WPX.transform.rotation);
        if (Physics.Raycast (WPX.position, WPX.forward, out hit, Mathf.Infinity)) {
            if (hit.transform.name == "Player") {
                if (distance < 5 || UnityEngine.Random.Range (0, 100) <= Luck) {
                    PlayerManager.Instance.HeroScript.HitPlayer (Random.Range (MinDamage, MaxDamage));
                    Instantiate (ImpactEffect, hit.point, Quaternion.LookRotation (hit.normal));
                }
            }
        }
        Destroy(shoot, 3f);
        AudioManager.instance.playSound ("shoot");
    }
}