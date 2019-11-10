using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTank : MonoBehaviour {
    // Rotates and moves up and down
    float angle = 220.0F;
    float frequency = 1F;
    float amplitude = 0.2F;

    Vector3 positionOffset = new Vector3();
    Vector3 temporatyPosition = new Vector3();

    void Start() {
        positionOffset = transform.position;
    }

    void Update() {
        Vector3 rotation = new Vector3(0.0F, Time.deltaTime * angle, 0.0F);
        float sinusFunction = frequency * Mathf.PI * Time.fixedTime; // A*pi*t 

        transform.Rotate(rotation, Space.World);
        temporatyPosition = positionOffset;
        temporatyPosition.y += amplitude * Mathf.Sin(sinusFunction);
        transform.position = temporatyPosition;
    }
}
