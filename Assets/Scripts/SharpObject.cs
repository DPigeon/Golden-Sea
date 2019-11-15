using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpObject : Projectile {
    public override void Awake() {
        base.Awake();
        Transform player = GameObject.Find("Player").transform;
        rigidbody = GetComponent<Rigidbody>();
        Vector3 playerPosition = new Vector3(player.position.x, player.position.y + 1, player.position.z);
        transform.LookAt(player.position);
    }

    public override void Update() {
        base.Update();
    }

    public override float GetSpeed() {
        return speed;
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.name == "Fish(Clone)") {
            // Collision with another npc
            Destroy(gameObject);
        }
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }
}