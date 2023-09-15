using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue_namespace;

public class Cauldron : MonoBehaviour, IInteractable
{

    public Dialogue_Enabler dialogue_Enabler;
    public Dialogue lines;

    public void Interact()
    {
        print("Cauldron: Cauldron was interacted with!");
        dialogue_Enabler.startDialogue(lines);
    }
}
