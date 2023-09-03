using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool doDebug = false;

    private bool keepMoving = true;
    private CharacterController characterController;
    /// <summary>
    /// All initialization occurs in Start
    /// </summary>
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keepMoving)
        {
            if (!characterController.isGrounded)
            {
                Vector3 move = Physics.gravity * speed * Time.deltaTime;

                Vector3 worldMove = transform.TransformDirection(move);
                if (doDebug)
                    Debug.LogFormat("Falling:local move = {0}, world move = {1}", move, worldMove);
                characterController.Move(worldMove);
            }
            else
            {
                Vector3 move = Vector3.back * speed * Time.deltaTime;

                Vector3 worldMove = transform.TransformDirection(move);
                if (doDebug)
                    Debug.LogFormat("Moving:local move = {0}, world move = {1}", move, worldMove);
                characterController.Move(worldMove);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.collider.CompareTag("Wall"))
        {
            keepMoving = false;    
        }

        
    }
}
