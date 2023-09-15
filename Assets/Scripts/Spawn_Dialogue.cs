using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue_namespace;

public class Spawn_Dialogue : MonoBehaviour, IInteractable
{

    public Dialogue_Enabler dialogue_Enabler;
    public Dialogue lines;

    public void Interact()
    {
        dialogue_Enabler.startDialogue(lines);
    }
}
