using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestActiveUI : QuestUI
{
    protected override void OnEnableFun()
    {
        try { 
            LoadData(QuestStatus.IN_PROGRESS);
        }
        catch(Exception what)
        {
            Debug.LogError($"{what}. Are there some null ScriptableObject in QuestManager?");
        }
    }
}
