using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    #region Singleton

    public static PlayerManager Instance;

    void Awake () {
        Instance = this;
    }

    #endregion

    public GameObject Player;
}