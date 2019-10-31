using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {
    [SerializeField]
    private string mouseXInputName = "", mouseYInputName = "";
    [SerializeField]
    private float mouseSensitivity = 0.0F, swimSpeed = 2.0F;

    [SerializeField]
    Transform player = null;

    float xAxisMax; // [-90 to 90]

    void Start() {
        xAxisMax = 0.0F;
        Cursor.lockState = CursorLockMode.Locked; // Removes the mouse cursor
    }

    void Update() {
        CameraRotation();
    }

    private void CameraRotation() {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;
        xAxisMax += mouseY; // If -90 --> downward and 90 --> upward
        if (xAxisMax > 90.0F) {
            xAxisMax = 90.0F;
            mouseY = 0.0F;
            MaxXAxisRotationToValue(270.0F);
        }
        else if (xAxisMax < -90.0F) {
            xAxisMax = -90.0F;
            mouseY = 0.0F;
            MaxXAxisRotationToValue(90.0F);
        }
        transform.Rotate(Vector3.left * mouseY);
        // We rotate the whole player to get the Y rotation view
        player.Rotate(Vector3.up * mouseX);
    }

    private void MaxXAxisRotationToValue(float value) {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
