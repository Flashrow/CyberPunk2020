using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private List<Sound> soundList;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene");
        }
        else
        {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound sound in soundList)
        {
            GameObject go = new GameObject("sound_" + sound.name);
            go.transform.SetParent(this.transform);
            go.AddComponent < AudioSource >();
            sound.setAudioSource(go.GetComponent<AudioSource>());
        }
    }


public void playSound(string soundName)
    {
        foreach(Sound sound in soundList)
        {
            if(sound.name == soundName)
            {
                sound.play();
                return;
            }
        }
        Debug.Log("AudioManager: sound not found, " + soundName );
    }
}
