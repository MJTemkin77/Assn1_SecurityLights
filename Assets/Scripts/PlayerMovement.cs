using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool doDebug = false;


    private bool keepMoving = true;
    private CharacterController characterController;

    Assn1_SecurityLights_Player inputActions;

    private void Awake()
    {
        inputActions = new Assn1_SecurityLights_Player();
        inputActions.Player.Move.started += Move_started;
        inputActions.Player.Move.canceled += Move_canceled;
    }



    /// <summary>
    /// All initialization occurs in Start
    /// </summary>
    /// 
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void Move_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        nextVector = Vector3.zero;
    }

    Vector3 nextVector = Vector3.zero;
    private void Move_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 temp = obj.ReadValue<Vector2>();
        nextVector = new Vector3(-temp.y, 0, temp.x);

    }

    // Update is called once per frame
    void Update()
    {
        if (nextVector != Vector3.zero)
        {
            Vector3 move = nextVector * speed * Time.deltaTime;
            Vector3 worldMove = transform.TransformDirection(move);
            characterController.Move(worldMove);
            if (CheckForReactiveFloor(out Collider collider))
            {
                var rcc =
                collider.GetComponentInParent<ReactiveFloorTileController>();
               if (rcc != null)
                {
                    rcc.DoSphere(this.gameObject);
                }

            }
        }

    }
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



    bool onReactiveFloor = false;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collider wcollider = hit.collider;
        if (wcollider.CompareTag("Wall"))
            nextVector = Vector3.zero;



    }
}
