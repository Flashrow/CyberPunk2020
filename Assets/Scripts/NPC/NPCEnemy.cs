using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCEnemy : NPCCharacter {
    public Slider HealthbarHandler;
    public Text HealthbarTextHandler;
    public float currentHealth { get; protected set; }
    public NPCEnemy () {
        currentHealth = MaxHealth;
    }
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, PlayerDetectArea);
    }
    void Update () {
        float distance = Vector3.Distance (player.position, transform.position);
        if (distance <= PlayerDetectArea) {
            agent.SetDestination (player.position);
            FaceTarget ();
            if (distance <= agent.stoppingDistance) {
                // TODO: Combat
                currentHealth--;
                float val = currentHealth / MaxHealth;
                HealthbarHandler.value = val;
                HealthbarTextHandler.text = $"{val}%";
            }
        }
        if(currentHealth < 0)
            Die();
    }
    public override void Die () {
        // TODO: Animation
        Destroy (gameObject);
    }
}