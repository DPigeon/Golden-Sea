using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyFigurine : Projectile {
    [SerializeField]
    Camera cameraView = null;

    public override void Awake() {
        base.Awake();
        cameraView = GameObject.Find("PlayerCamera").GetComponent<Camera>();
    }

    public override void Update() {
        transform.position += cameraView.transform.forward * speed * Time.deltaTime;
    }

    public override float GetSpeed() {
        return speed;
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.name == "Fish(Clone)") {
            // Distract them
            Debug.Log("hi");
        }
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }
}
