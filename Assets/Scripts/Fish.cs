using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Enemy {
    bool distracted;
    float distractTimer = 0.0F;
    float distractDuration = 3.0F;

    public override void Start() {
        base.Start();
        limit = 22.0F;
        speed = enemySpawner.currentSpeedGameLevelFish;
        spawnedPositionY = transform.position.y;
    }

    public override void Update() {
        base.Update();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        CheckBoundaries();

        /* Distract enemy logic */ 
        if (distracted) {
            distractTimer += Time.deltaTime;
            if (distractTimer > distractDuration) {
                SetSpeed(4.0F);
                distractTimer = 0.0F;
                distracted = false;
            }
        }
    }

    public override float GetSpeed() {
        return speed;
    }

    public override void SetSpeed(float number) {
        base.SetSpeed(number);
    }

    public override void IncrementSpeed(float speed) {
        base.IncrementSpeed(speed);
    }

    public void Distract() {
        SetSpeed(0.0F);
        distracted = true;
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }

    private void CheckBoundaries() {
        // | Side1 - Side2 | in Z
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

}
