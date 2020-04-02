using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCCharacter : MonoBehaviour {
    public float PlayerDetectArea = 10f;
    public Transform[] MoveSpots;
    protected int randomSpot;
    protected Transform player;
    protected NavMeshAgent agent;
    void Start () {
        player = PlayerManager.Instance.Player.transform;
        agent = GetComponent<NavMeshAgent> ();
        randomSpot = Random.Range (0, MoveSpots.Length - 1);
    }
    protected void FaceTarget () {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    public abstract void OnDrawGizmosSelected ();
    public virtual void Die () { }
}