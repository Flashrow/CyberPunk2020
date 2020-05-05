using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractedCube : Interacted
{
    DialogueParser parser;
    // Start is called before the first frame update
    void Start()
    {
        parser = GetComponent<DialogueParser>();
    }

    public override void OnInteract()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
