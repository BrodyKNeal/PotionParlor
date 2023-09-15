//This script should be attached to the player character. 
/*It will send out a short ray in the direction the character is facing 
 * If the ray hits an object with an "Interactable" component then it will trigger
 * the script of the Interactable object.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add the Interactable modifer after any script's monoBehavior. This will allow you to call the Interact() script
interface IInteractable
{
    void Interact();
}

public class Interact : MonoBehaviour
{
    public Transform playerObject; //The location of the player object in the scene
    public KeyCode interactKey = KeyCode.E; //What key to be pressed to interact
    public float InteractRange; //How long is the ray sent out to trigger the interact on the object.
    private int playerLayer; //The layermask that ignores the playerObject. Just put objects you want to ignore on the player layer


    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Interact_Ignore"); 
    }

    // Update is called once per frame
    void Update()
    {
        //This draws the ray in the game window if you have "Gizmos" button turned on.
        //It will help visualize the interact range
        Debug.DrawRay(transform.position, Vector2.left, Color.green);
        if (Input.GetKeyDown(interactKey))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), InteractRange, playerLayer); //Sends out a raycast to hit objects, ignoring the "interact_ignore" layer
            if(hit)
            {
                if (hit.collider.TryGetComponent<IInteractable>(out IInteractable hitobject))
                {
                    hitobject.Interact();
                }
                
            }
        }
    }
}
