using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to move the character controller component of the attached Game Object
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Designer can choose the speed that the character will move.
    /// </summary>
    [SerializeField] private float speed = 2f;

    /// <summary>
    /// Used to enable Console.Log messages.
    /// </summary>
    [SerializeField] private bool doDebug = false;

    /// <summary>
    /// Cache the reference to the characterController component
    /// </summary>
    private CharacterController characterController;

    

    /// <summary>
    /// Reference to the generated Input Class.
    /// </summary>
    Assn1_SecurityLights_Player inputActions;

    /// <summary>
    /// Stores the vector3 values of the next move by the character controller.
    /// </summary>
    private Vector3 nextVector = Vector3.zero;

    /// <summary>
    /// Initialize input prior to Start.
    /// Move.started is used so that the game player(user)
    /// can hold down the key. 
    /// Move.canceled is called with the game player releases the key.
    /// </summary>
    private void Awake()
    {
        inputActions = new Assn1_SecurityLights_Player();
        inputActions.Player.Move.started += Move_started;
        inputActions.Player.Move.canceled += Move_canceled;
    }


    /// <summary>
    /// Any initializations of components occur here.
    /// </summary>
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Standard action required when using classes that need to call Enable such as the Input System.
    /// </summary>
    private void OnEnable()
    {
        inputActions.Enable();
    }

    /// <summary>
    /// Standard actions required when using classes that need to call Disable such as the Input System.
    /// </summary>
    private void OnDisable()
    {
        inputActions.Disable();
    }


    /// <summary>
    /// Whenever the user presses down and holds down on a key then the vector2 read by 
    /// the input system is store in the class variable nextVector.
    /// </summary>
    /// <param name="obj">The input system data</param>
    private void Move_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 temp = obj.ReadValue<Vector2>();
        nextVector = new Vector3(temp.y, 0, -temp.x);

    }

    /// <summary>
    /// Clear the nextVector when the user releases the key.
    /// </summary>
    /// <param name="obj">The input system data</param>
    private void Move_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        nextVector = Vector3.zero;
    }

   
    /// <summary>
    /// Update does two things.
    /// <list type="number">
    /// <item>Creates the parameter for the Move method</item>
    /// <item>Calls track target if the controller is on a Reactive Floor</item>
    /// </list>
    /// </summary>
    
    void Update()
    {
        // Only perform when nextVector has a value
        if (nextVector != Vector3.zero)
        {
            // Scale and make frame-rate independent the nextVector vaue.
            Vector3 move = nextVector * speed * Time.deltaTime;
            // As the player is a root node then it is not necessary to use TransformDirection.
            characterController.Move(move);

            // If the game object is on top of a reactive floor then call the parent object 
            // of the contact object and start tracking this game object.
            if (CheckForReactiveFloor(out Collider collider))
            {
                var rcc =
                collider.GetComponentInParent<ReactiveFloorTileController>();
               if (rcc != null)
                {
                    if (!rcc.TrackTarget(this.gameObject))
                    {
                        Debug.LogFormat("{0} does not have a SpotLight!", rcc.gameObject.name);
                    }
                }

            }
        }

    }

    /// <summary>
    /// Sitting on the floor, or moving on it, does not cause a collision as in OnCollisionEnter.
    /// Because of that the Player object needs to check below to see if it is on top of a Reactive Floor.
    /// In this version of the project the scene is setup so that no other objects are below the Floor. 
    /// If there were levels below the Floor then the hit would need a range limit or the distance property of
    /// the hit would need to be validated against some reasonable range.
    /// </summary>
    /// <param name="collider">Return through an out parameter the collider, which is the floor.</param>
    /// <returns>True if a ReactiveFloor game object is below the player</returns>
    private bool CheckForReactiveFloor(out Collider collider)
    {
        bool fndReactiveFloor = false;
        collider = null;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down),
            out hit))
        {

            if (hit.collider.CompareTag("ReactiveFloor"))
            {
                fndReactiveFloor = true;
                collider = hit.collider;
            }
        }

        return fndReactiveFloor;


    }

    /// <summary>
    /// Stop the character controller movement when the Player touches a Wall.
    /// </summary>
    /// <param name="hit">The object that was hit</param>

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collider wcollider = hit.collider;
        if (wcollider.CompareTag("Wall"))
            nextVector = Vector3.zero;

        
        float distanceBetweenObjects = hit.collider.contactOffset;
        Debug.LogFormat("OnControllerColliderHit distance = {0}", distanceBetweenObjects);
    }
}
