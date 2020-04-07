using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickMenu : MonoBehaviour {
    public void EndMission () {
        Debug.Log ("EndMission ()");
    }
    public void QuitGame () {
        SceneManager.LoadScene ("MenuStart");
    }
    public void Save () {
        Debug.Log ("Save ()");
    }
    public void Load () {
        Debug.Log ("Load ()");
    }
}