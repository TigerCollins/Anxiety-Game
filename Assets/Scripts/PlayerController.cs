using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private GameObject playerHolder;
    [SerializeField]
    private CharacterController characterCollider;


    [Header("Movement")]
    public bool canMove = false;
    [SerializeField]
    private float gravityForce;
    [SerializeField]
    private float movementMultiplier;
    [SerializeField]
    private Vector2 moveAxis;
    private Vector3 moveVector;
    [SerializeField]
    private Vector3 desiredMoveDirection;
    [SerializeField]
    private float maxMovementSpeed = 1;

    [Header("Raycast")]
    public GameObject raycastObject;
    public Transform rayCastingPoint;
    public float raycastDistance;
    public Color raycastColour;

    [Header("Raycast - Script References")]
    public DialogueTrigger dialogueTrigger;

    [Header("Startup Sequence")]
    public bool textMessageOver;
    [SerializeField]
    private bool playerMoved;
    [SerializeField]
    private float timeToMove;
    [SerializeField]
    private Transform startPosition;

    [Header("Direction Variables")]
    [SerializeField]
    private float lastMoveAxis;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void TouchMovement(float movementWeighting)   
    {
  moveAxis = new Vector2(movementWeighting, 0);
        lastMoveAxis = moveAxis.x;


    }

    public void CancelTouchMovement()
    {
        moveAxis = new Vector2(0, 0);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();

        //This needs to be in fixed update for smoothness
        MoveIntoHouse();
    }

    public void Update()
    {
        Raycast();
       
    }

    public void ChangeFacingDirection()
    {
 //If there is horizontal input 
        if(moveAxis.x!=0)
        {
            Vector3 movementDirection = new Vector3(0, 0, moveAxis.x);

            playerHolder.transform.rotation = Quaternion.LookRotation(movementDirection);
        }

       
    }


    //Only controls player auto moving through doorway on start (after text messages)
    public void MoveIntoHouse()
    {
        //When text messages are done, make player move through door
        if (textMessageOver == true && playerMoved == false)
        {
            playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, startPosition.transform.position, timeToMove * Time.deltaTime);

            if (Vector3.Distance(playerObject.transform.position, startPosition.transform.position) < 0.001f)
            {
                //Player regains character control and character stops auto moving
                canMove = true;
                playerMoved = true;
            }
        }
    }

    //Gets reference from Player Input script
    public void OnMove(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
        if(moveAxis.x != 0)
        {
            lastMoveAxis = moveAxis.x;
        }
    }

    public void Interactions(bool mouseUsed)
    {
        if(canMove)
        {
            //If the player is looking at an object with the dialogue trigger, do this.
            if (dialogueTrigger != null)
            {
                dialogueTrigger.StartTextPopup(mouseUsed);
            }

        }

    }

    public void Movement()
    {
      

        if (canMove)
        { 
              //Sets gravity for player
        moveVector = new Vector3(0, gravityForce * .2f * Time.deltaTime, 0);
        characterCollider.Move(moveVector);

            //Sets up movement and it from OnMove function
            float horizontal = moveAxis.x * movementMultiplier;
            desiredMoveDirection = Vector3.ClampMagnitude(new Vector3(horizontal, 0, 0), maxMovementSpeed);
            characterCollider.Move(desiredMoveDirection * Time.deltaTime * movementMultiplier);

        }

    }

    void Raycast()
    {
        // The raycast
        RaycastHit hit;
        Debug.DrawRay(rayCastingPoint.position, rayCastingPoint.transform.forward * raycastDistance, raycastColour, Time.deltaTime);

        //Checks a raycast for scripts with the If statement
        if (Physics.Raycast(rayCastingPoint.position, rayCastingPoint.transform.forward, out hit, raycastDistance))
        {
            //If the raycast hits an object with a dialogue trigger
            if(hit.collider.GetComponent<DialogueTrigger>() != null)
            {
                dialogueTrigger = hit.collider.GetComponent<DialogueTrigger>();
            }

            //If the ray cast isn't hitting an object with a dialogue trigger, the variable is null
            else
            {
                dialogueTrigger = null;
            }
        }

        //If raycast hits nothing
        else
        {
            dialogueTrigger = null;
        }
    }
}
