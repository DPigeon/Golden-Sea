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

    OxygenBar oxygenBar;

    void Start() {
        oxygenBar = GameObject.Find("OxygenBar").GetComponent<OxygenBar>();
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

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "Player") {
            if (gameObject.name == "OxygenTank(Clone)") {
                oxygenBar.IncreaseOxygen(1); // Generates 1 oxygen bar
                Destroy(gameObject);
            }
            if (gameObject.name == "BubbleOxygenTank(Clone)") {
                oxygenBar.IncreaseOxygen(2); // Generates 2 oxygen bar
                Destroy(gameObject);
            }
        }
    }

}
