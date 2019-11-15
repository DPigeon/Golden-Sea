using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : Enemy {
    [SerializeField]
    GameObject PrefabProjectile = null;

    float projectileDuration = 10.0F;
    float nextProjectileSpawn = 0.0F;
    float projectileFrequency = 0.0F;

    public override void Start() {
        base.Start();
        limit = 18.0F;
        speed = enemySpawner.currentSpeedGameLevelWhale;
    }

    public override void Update() {
        base.Update();
        projectileFrequency = Random.Range(3, 7);
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        HandleProjectiles();
        CheckBoundaries();
    }

    private void HandleProjectiles() {
        if (Time.time > nextProjectileSpawn)  {
            nextProjectileSpawn = Time.time + projectileFrequency;
            ShootProjectile();
        }
    }

    private void ShootProjectile() {
        GameObject projectileObject = Instantiate(PrefabProjectile, transform.position, Quaternion.identity) as GameObject;
        //projectileSound.Play();
        Destroy(projectileObject, projectileDuration);
    }

    public override float GetSpeed() {
        return speed;
    }

    public override void SetSpeed(float number)
    {
        base.SetSpeed(number);
    }

    public override void IncrementSpeed(float speed) {
        base.IncrementSpeed(speed);
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }

    private void CheckBoundaries() {
        // | Side1 - Side2 | in Z (Whale is inversed vector forward/back)
        if (transform.position.z < -limit)
            transform.rotation = Quaternion.LookRotation(-Vector3.forward);
        else if (transform.position.z > limit)
            transform.rotation = Quaternion.LookRotation(-Vector3.back);
        // | Side3 - Side4 | in X
        else if (transform.position.x < -limit)
            transform.rotation = Quaternion.LookRotation(Vector3.right);
        else if (transform.position.x > limit)
            transform.rotation = Quaternion.LookRotation(-Vector3.right);
    }
}
