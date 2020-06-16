using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class LoadGameList : MonoBehaviour
{
    private void OnEnable()
    {
        try { 
            string[] files = Directory.GetDirectories($"{Application.persistentDataPath}/Games/");
            int posYBtn = 0;
            for(int i = files.Length - 1; i >=0; i--)
            {
                int pos = files[i].LastIndexOf("/") + 1;
                string name = files[i].Substring(pos, files[i].Length - pos);
                GameObject go = Resources.Load("PreFabs/QuickMenuFileGameBtn") as GameObject;
                GameObject clone = Instantiate(go, Vector2.zero, Quaternion.identity);
                clone.transform.parent = transform;
                clone.transform.localScale = new Vector3Int(1, 1, 1);
                RectTransform rt = clone.GetComponent<RectTransform>();
                //rt.anchoredPosition = rt.transform.position;
                rt.anchoredPosition = new Vector2(0, posYBtn);
                posYBtn -= 115;
                clone.GetComponentInChildren<Text>().text = name;
                clone.GetComponent<Button>().onClick.AddListener(() =>
                {
                    QuickMenu.SetFileToLoadEvent.Invoke(name);
                });
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
           Destroy(child.gameObject);
    }
}
