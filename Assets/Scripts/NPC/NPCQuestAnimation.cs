using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestAnimation : NPCAnimation {
    RuntimeAnimatorController primary = null;
    public RuntimeAnimatorController InteractionController = null;
    protected override void onAwake () {
        primary = anim.runtimeAnimatorController;
    }
    public void AnimStartInteraction () {
        anim.runtimeAnimatorController = InteractionController;
    }

    public void AnimEndInteraction () {
        anim.runtimeAnimatorController = primary;
    }

}