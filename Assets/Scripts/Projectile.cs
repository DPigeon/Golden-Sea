using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField]
    public float speed;

    public Rigidbody rigidbody;

    public virtual void Awake() {
    }

    public virtual void Update() {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public virtual float GetSpeed() {
        return speed;
    }

    public virtual void OnDestroy() {
        Destroy(gameObject);
    }

}
