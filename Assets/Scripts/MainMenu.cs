using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void PlayGame () {
        SceneManager.LoadScene ("City");
    }

    public void CreateNewGame () {
        Debug.Log ("NEW GAME!");
    }

    public void QuitGame () {
        Debug.Log ("QUIT WORK!");
        Application.Quit ();
    }
}