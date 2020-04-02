using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    ushort FirstPearsonCamera = 0;
    ushort ThirdPearsonCamera = 1;
    public ushort CameraStart;
    public ushort cameraType { get; private set; }

    #region Singleton
    public static CameraManager Instance;
    void Awake () {
        Instance = this;
    }
    #endregion

    public CameraManager () {
        cameraType = CameraStart;
    }

    void Start () {
        if (cameraType == FirstPearsonCamera) {
            FirstPearson.SetActive (true);
            ThirdPearson.SetActive (false);
            Current = FirstPearson;
        } else {
            FirstPearson.SetActive (true);
            ThirdPearson.SetActive (false);
            Current = ThirdPearson;
        }
    }

    void Update () {
        switchCamera ();
    }

    void switchCamera () {
        if (Input.GetKeyDown (KeyCode.V)) {
            if (cameraType == FirstPearsonCamera) {
                cameraType = ThirdPearsonCamera;
                FirstPearson.SetActive (false);
                ThirdPearson.SetActive (true);
                Current = ThirdPearson;
            } else {
                cameraType = FirstPearsonCamera;
                FirstPearson.SetActive (true);
                ThirdPearson.SetActive (false);
                Current = FirstPearson;
            }
        }
    }

    public Camera Brain;
    public GameObject FirstPearson;
    public GameObject ThirdPearson;
    public GameObject Current { get; private set; }
}