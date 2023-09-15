//This script preps the dialogue box to display a list of dialogue lines, then enables and disables it as needed.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue_namespace;

public class Dialogue_Enabler : MonoBehaviour
{
    public GameObject dialogue_box;
    private Dialogue_Manager dialogue_script;
    private bool inDialogue = false;

    private void Start()
    {
        dialogue_script = dialogue_box.GetComponent<Dialogue_Manager>();
    }

    //Update the lines that will be shown. This should be called by the interacted object.
    //Remember, the interacted object will need to include the dialogue namespace to properly format the lines it wants to show!
    public void startDialogue(Dialogue received_lines)
    {
        dialogue_box.SetActive(true); //Enable the dialogue box
        dialogue_script.beginSpeaking(received_lines); //Now show all the lines one by one
        inDialogue = true;
    }

    private void Update()
    {
        //If dialogue box is disabled, then dialogue has been completed!
        if(!dialogue_box.activeSelf && inDialogue)
        {
            print("Dialogue is over!");
            inDialogue = false;
        }
    }
}
