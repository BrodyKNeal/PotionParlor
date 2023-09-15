using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        print("Cauldron: Cauldron was interacted with!");
    }
}
