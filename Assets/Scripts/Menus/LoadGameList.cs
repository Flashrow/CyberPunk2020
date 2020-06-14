using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadGameList : MonoBehaviour
{
    // $"{Application.persistentDataPath}/Games/{fname}-{data.day}-{data.time}/"

    private void OnEnable()
    {
        string[] files = Directory.GetDirectories($"{Application.persistentDataPath}/Games/");
        int posYBtn = 0;
        foreach (var item in files)
        {
            int pos = item.LastIndexOf("/") + 1;
            string name = item.Substring(pos, item.Length - pos);
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
                // TODO
            });
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
           Destroy(child.gameObject);
    }
}
