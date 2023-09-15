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

enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Interact : MonoBehaviour
{
    public Transform playerObject; //The location of the player object in the scene
    public KeyCode interactKey = KeyCode.E; //What key to be pressed to interact
    public float InteractRange; //How long is the ray sent out to trigger the interact on the object.
    private int playerLayer; //The layermask that ignores the playerObject. Just put objects you want to ignore on the player layer
    private Direction direction; //What direction is the player moving in?

    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Interact_Ignore"); //Get the layer to ignore.
    }

    //Sends out a raycast based on where the player is facing
    //Also ignores the "interact_ignore" layer
    Direction getDirection(Vector2 movement)
    {
        //Send ray left/right
        if( Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            if(movement.x < 0)
            {
                return Direction.LEFT;
            }
            else
            {
                return Direction.RIGHT;
            }
        }
        else //send ray up/down
        {
            if (movement.y < 0)
            {
                return Direction.DOWN;
            }
            else
            {
                return Direction.UP;
            }
        }
    }

    //Sends out a raycast based on the direction of the player. Ignores the playerLayer, defined as Interact_Ignore
    RaycastHit2D sendRaycast()
    {
        switch(direction)
        {
            case Direction.UP:
                return Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), InteractRange, playerLayer);
            case Direction.DOWN:
                return Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), InteractRange, playerLayer);
            case Direction.LEFT:
                return Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), InteractRange, playerLayer);
            case Direction.RIGHT:
                return Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), InteractRange, playerLayer);
            default:
                print("Interact.cs ERROR: PLAYER DIRECTION IS UNDEFINED. ASSUMING DOWN");
                return Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), InteractRange, playerLayer);
        }
    }

    //Draws out a raycast based on the direction of the player. Only visible in the editor
    void drawRaycast()
    {
        switch (direction)
        {
            case Direction.UP:
                Debug.DrawRay(transform.position, Vector2.up, Color.green);
                break;
            case Direction.DOWN:
                Debug.DrawRay(transform.position, Vector2.down, Color.green);
                break;
            case Direction.LEFT:
                Debug.DrawRay(transform.position, Vector2.left, Color.green);
                break;
            case Direction.RIGHT:
                Debug.DrawRay(transform.position, Vector2.right, Color.green);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //If the player is moving, update their direction
        if (movement.magnitude > 0)
        {
            direction = getDirection(movement); 
        }

        //This draws the ray in the game window if you have "Gizmos" button turned on.
        //It will help visualize the interact range.
        drawRaycast();
        if (Input.GetKeyDown(interactKey))
        {
            RaycastHit2D hit = sendRaycast(); //Sends out a raycast to hit objects, ignoring the "interact_ignore" layer

            if (hit)
            {
                if (hit.collider.TryGetComponent<IInteractable>(out IInteractable hitobject))
                {
                    hitobject.Interact();
                }
                
            }
        }
    }
}
