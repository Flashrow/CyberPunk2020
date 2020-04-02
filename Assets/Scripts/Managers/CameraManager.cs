using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    #region Singleton

    public static CameraManager Instance;

    void Awake () {
        FirstPearson.SetActive (true);
        ThirdPearson.SetActive (false);
        Current = FirstPearson;
        Instance = this;
    }

    #endregion

    public Camera Brain;
    public GameObject FirstPearson;
    public GameObject ThirdPearson;
    public GameObject Current { get; private set; }
}