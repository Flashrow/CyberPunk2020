using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCEnemy : NPCCharacter {
    public Slider HealthbarHandler;
    public Text HealthbarTextHandler;
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, PlayerDetectArea);
    }

    void Awake() {
        HealthbarTextHandler.text = $"{MaxHealth}";
    }

    void Update () {
        float distance = Vector3.Distance (player.position, transform.position);
        if (distance <= PlayerDetectArea) {
            agent.SetDestination (player.position);
            FaceTarget ();
            if (distance <= agent.stoppingDistance) {
                // TODO: Combat + event
                OnHit (1);
                PlayerManager.Instance.HeroScript.HitPlayer(1);
            }
        } else NPCMove ();
        if (currentHealth < 0) Die ();
    }
    public override void OnHit (float val) {
        currentHealth -= val;
        float valHealth = currentHealth / MaxHealth;
        HealthbarHandler.value = valHealth;
        HealthbarTextHandler.text = $"{currentHealth} / {MaxHealth}";
    }
    protected override void Die () {
        // TODO: Animation
        Destroy (gameObject);
    }
}