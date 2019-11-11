using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyFigurine : Projectile {
    public override void Awake() {
        base.Awake();
    }

    public override void Update() {
        base.Update();
    }

    public override float GetSpeed() {
        return speed;
    }

    void OnTriggerEnter(Collider col) {
        LifeGenerator playerLife = GameObject.Find("LifeHandler").GetComponent<LifeGenerator>();
        if (col.gameObject.name == "Player") {
            playerLife.RemoveLife();
            Destroy(gameObject);
        }
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }
}
