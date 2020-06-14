using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameList : MonoBehaviour
{
    static List<RectTransform> GameLists = null;

    private void OnEnable()
    {
        GameLists = new List<RectTransform>();
        //File.Exists($"{Application.persistentDataPath}/{fname}.cjc")
    }

    private void OnDisable()
    {
        GameLists = null;
    }
}
