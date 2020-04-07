using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {
    public float TimeLife = 2f;
    public float Damage = 50;
    public float Tolerant = 10;
    private float timer = 0f;
    void Update () {
        timer += Time.deltaTime;
        if (timer >= TimeLife) Destroy (this.gameObject);
        transform.position += transform.forward * Time.deltaTime * 20f;
    }

    void OnTriggerEnter (Collider item) {
        if (item.name == "Player") {
            var t = UnityEngine.Random.Range (0, Tolerant);
            if (UnityEngine.Random.Range (0, 100) < 50)
                Damage -= Tolerant;
            else Damage += Tolerant;
            PlayerManager.Instance.HeroScript.HitPlayer (Damage);
            Debug.Log (Damage);
        }
        Destroy (this.gameObject);
    }

    private void OnTriggerExit (Collider other) {
        Destroy (this.gameObject);
    }

    void OnCollisionEnter (Collision collision) {
        Destroy (this.gameObject);
    }
}