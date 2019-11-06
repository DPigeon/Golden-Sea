using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField]
    float speed;
    Rigidbody rigidbody;

    void Awake() {
        Transform player = GameObject.Find("Player").transform;
        rigidbody = GetComponent<Rigidbody>();
        Vector3 playerPosition = new Vector3(player.position.x, player.position.y + 1, player.position.z);
        transform.LookAt(player.position);
        //rigidbody.velocity = -Vector3.forward * speed;
    }

    void Update() {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider col) {
        // Will be used later once we have a player attacking the boss
        LifeGenerator playerLife = GameObject.Find("LifeHandler").GetComponent<LifeGenerator>();
        if (col.gameObject.name == "Player") {
            playerLife.RemoveLife();
            Destroy(gameObject);
        }
    }

}
