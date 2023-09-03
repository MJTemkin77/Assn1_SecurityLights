using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
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
        if (!characterController.isGrounded)
        {
            characterController.Move( speed *
             Physics.gravity * Time.deltaTime);
        }
    }
}
