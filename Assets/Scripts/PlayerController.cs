using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    string horizontalInput;
    [SerializeField]
    string verticalInput;
    [SerializeField]
    float movementSpeed;

    CharacterController playerController;

    void Start() {
        playerController = GetComponent<CharacterController>();
    }

    void Update() {
        PlayerMovement();
    }

    private void PlayerMovement() {
        float horizontal = Input.GetAxis(horizontalInput) * movementSpeed;
        float vertical = Input.GetAxis(verticalInput) * movementSpeed;
        Vector3 right = transform.right * horizontal;
        Vector3 forward = transform.forward * vertical;

        playerController.SimpleMove(forward + right); // Moves our component
    }

}
