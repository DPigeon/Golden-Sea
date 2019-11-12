using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyFigurine : Projectile {
    [SerializeField]
    Camera cameraView = null;

    public override void Awake() {
        base.Awake();
        cameraView = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        transform.rotation = Quaternion.LookRotation(cameraView.transform.forward);
    }

    public override void Update() {
        base.Update();
    }

    public override float GetSpeed() {
        return speed;
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.name == "Fish(Clone)") {
            // Distract them by putting their speed to 0 for 3 seconds
            Fish fish = col.gameObject.GetComponent<Fish>();
            fish.Distract();
            Destroy(gameObject);
        }
        if (col.gameObject.name == "Whale(Clone)") {
            // No distraction
            Destroy(gameObject);
        }
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }
}
