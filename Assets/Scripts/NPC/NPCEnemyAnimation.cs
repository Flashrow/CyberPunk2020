using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemyAnimation : NPCAnimation {
    public void StartFire () {
        anim.SetBool ("fire", true);
    }
    public void StopFire () {
        anim.SetBool ("fire", false);
    }
}