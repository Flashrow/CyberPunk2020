using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemyAttack : MonoBehaviour {
    public int Luck;
    public float MinDamage;
    public float MaxDamage;
    public float Area = 30f;
    void Start () {
        if (Luck < 0 || Luck > 100) Luck = 50;
    }
    public void ShootToPlayer () {
        //TODO: ANIMATION + AUDIO
        if (UnityEngine.Random.Range (0, 100) >= Luck) {
            RaycastHit hit;
            transform.LookAt (PlayerManager.Instance.Player.transform);
            if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, Mathf.Infinity)) {
                if (hit.transform.name == "Player") {
                    PlayerManager.Instance.HeroScript.HitPlayer (Random.Range (MinDamage, MaxDamage));
                    Debug.Log ("NPC HIT YOU :D");
                }
            }
        }

    }
}