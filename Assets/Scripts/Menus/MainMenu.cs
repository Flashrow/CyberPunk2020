using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void PlayGame () {
        Debug.Log("LOAD LAST SAVE!");

        //SceneManager.LoadScene ("City");
    }

    public void CreateNewGame () {
        SceneManager.LoadScene("OpenWorld");
    }

    public void QuitGame () {
        Debug.Log ("QUIT WORK!");
        Application.Quit ();
    }
}