using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuickMenu : MonoBehaviour {

    string fNameToLoad = null;
    public class fNameLoadEvent : UnityEvent<string> { };
    static public fNameLoadEvent SetFileToLoadEvent = new fNameLoadEvent();

    private void Start()
    {
        SetFileToLoadEvent.AddListener(name =>
        {
            fNameToLoad = name;
        });
    }

    public void Load()
    {
        try
        {
            SaveLoadSystem.System.Load(fNameToLoad);
        }
        catch (Exception ex)
        {
            UImanager.Alert("Can't load game status!!!", 2.5f);
            Debug.LogWarning(ex.Message);
        } finally { 
            CloseQuickMenu();
        }
    }

    public void CloseQuickMenu () {
        UImanager.UIPermentClose ();
    }
    public void QuitGame () {
        SceneManager.LoadScene ("MenuStart");
    }
    public void Save (RectTransform saveInput) {
        string fname = saveInput.GetComponent<Text>().text;
        try
        {
            if(SaveLoadSystem.System.Save(fname))
            {
                UImanager.Alert($"Game was save: {fname}", 4.5f);
            }
        } catch (Exception ex)
        {
            UImanager.Alert($"Can't save game: {ex.Message}", 2.5f);
            Debug.LogWarning(ex.Message);
        }
        CloseQuickMenu();
    }
}