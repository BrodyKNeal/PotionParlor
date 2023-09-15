//This script can be added to any gameobject. 
//If the player interacts with the object, it will spawn dialogue.
//The dialogue can be edited per-object in the Untiy editor.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue_namespace;

                                             //IInteractable addition means the Interact() function will be triggered if player interacts with this object
public class Spawn_Dialogue : MonoBehaviour, IInteractable
{

    public Dialogue_Enabler dialogue_Enabler; //The prefab Dialogue_enabler.
    public Dialogue lines; //The list of lines to send to the Dialogue_enabler to be displayed when this object is interacted with.

    public void Interact()
    {
        dialogue_Enabler.startDialogue(lines);
    }
}
