using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCameraComponents : MonoBehaviour
{
    [SerializeField] GameObject go;
    void Update()
    {
        if (CameraManager.Instance.Brain.IsLive(CameraManager.Instance.FirstPearson) == false)
            go.SetActive(false);
        else go.SetActive(true);

    }
}
