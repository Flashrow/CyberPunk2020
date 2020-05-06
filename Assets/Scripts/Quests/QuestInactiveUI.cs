using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestInactiveUI : QuestUI
{
    protected override void OnEnableFun()
    {
        try { 
            LoadData(QuestStatus.TODO);
        }
        catch(Exception what)
        {
            Debug.LogError($"{what}. Are there some null ScriptableObject in QuestManager?");
        }
    }
}
