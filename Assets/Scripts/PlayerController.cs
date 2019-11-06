using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    string horizontalInput = "";
    [SerializeField]
    string verticalInput = "";
    [SerializeField]
    float movementSpeed = 2.0F;
    [SerializeField]
    Camera cameraView = null;

    CharacterController playerController;

    void Start() {
        playerController = GetComponent<CharacterController>();
    }

    void Update() {
        PlayerMovement();
        CheckBoundaries();
    }

    private void PlayerMovement() {
        float horizontal = Input.GetAxis(horizontalInput) * movementSpeed;
        float vertical = Input.GetAxis(verticalInput) * movementSpeed;
        Vector3 right = transform.right * horizontal;
        Vector3 forward = transform.forward * vertical;
        playerController.SimpleMove(forward + right); // Moves our component

        if (Input.GetButton("Swim")) {
            transform.position += cameraView.transform.forward * Time.deltaTime * 10.0F;
        }
    }

    private void CheckBoundaries() {
        // | Side1 - Side2 | in Z
        float limit = 22.0F;
        if (transform.position.z < -limit)
            transform.position = new Vector3(transform.position.x, transform.position.y, limit);
        else if (transform.position.z > limit)
            transform.position = new Vector3(transform.position.x, transform.position.y, -limit);
        // | Side3 - Side4 | in X
        else if (transform.position.x < -limit)
            transform.position = new Vector3(limit, transform.position.y, transform.position.z);
        else if (transform.position.x > limit)
            transform.position = new Vector3(-limit, transform.position.y, transform.position.z);
    }

    public void IncreaseSpeed(float number) {
        movementSpeed = movementSpeed + number;
    }

    public void DecreaseSpeed(float number) {
        movementSpeed = movementSpeed - number;
    }

    public void ResetSpeed() {
        movementSpeed = 5.0F;
    }

}
