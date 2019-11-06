using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    GameObject FishPrefab = null;

    [SerializeField]
    GameObject WhalePrefab = null;

    GameObject currentWhale;

    ItemSpawner itemSpawner;
    //LevelManager levelManager;

    float fishLimit = 28.0F; // in Z
    float whaleLimit = 30.0F; // in Z
    float planeLimit = 23.0F; // in XY plane

    float leftScreenX = -7.0F;
    float leftScreenY1 = 2.20F;
    float leftScreenY2 = -4.15F;
    float rightScreenX = 7.0F;

    float offsetXLeft = 0.0F;
    float offsetXRight = 0.0F;

    float nextFishSpawn = 0.0F;
    float nextWhaleSpawn = 0.0F;
    float spawnFishInterval = 10.0F; // Will change according to levels
    float spawnWhaleInterval; // Have to appear twice in a level at random times
    float fishAliveTime;
    float whaleAliveTime;
    public float currentSpeedGameLevelFish;
    public float currentSpeedGameLevelWhale;
    public int fishWaveSize = 3;

    void Start() {
        fishAliveTime = 20.0F;
        nextWhaleSpawn = Random.Range(1, 10);
        spawnWhaleInterval = Random.Range(12, 20);
        currentWhale = null;

        itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
        //levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        currentSpeedGameLevelFish = 1.8F; // initial speed for Fishs
        currentSpeedGameLevelWhale = 2.3F; // initial speed for Whale
    }

    void Update() {
        //currentSpeedGameLevelFish = 1.8F + (float)levelManager.GetLevel() * levelManager.GetSpeedLevelRate();
        //currentSpeedGameLevelWhale = 2.3F + (float)levelManager.GetLevel() * levelManager.GetSpeedLevelRate();
        whaleAliveTime = Random.Range(7, 11);
        spawnWhaleInterval = Random.Range(12, 20);
        HandleFishEnemy();
        HandleWhaleEnemy();
    }

    // Move in Z plane, spawn in XY plane from x: -25, 25 to y: -25, 25, limit 18 for Whale & 22 for Fish
    void HandleFishEnemy() {
        if (Time.time > nextFishSpawn) {
            nextFishSpawn = Time.time + spawnFishInterval;
            for (int i = 0; i < fishWaveSize; i++) {
                Vector3 spawnedPosition;
                Vector3 spriteDirection;
                float x = Random.Range(-planeLimit, planeLimit);
                float y = Random.Range(-planeLimit, planeLimit);
                float randomSize = Random.Range(7.00F, 12.00F);
                float randomSide = Random.Range(-1, 2); // 0 is left, 1 is right
                offsetXRight = Random.Range(0, 7);
                offsetXLeft = Random.Range(-7, 0);

                if (randomSide == 0) {
                    spawnedPosition = new Vector3(-x - Random.Range(0, 7), y, fishLimit);
                    spriteDirection = Vector3.forward;
                }
                else {
                    spawnedPosition = new Vector3(x + Random.Range(0, 7), y, fishLimit);
                    spriteDirection = Vector3.back;
                }
                GameObject fish = Instantiate(FishPrefab, spawnedPosition, Quaternion.LookRotation(spriteDirection)) as GameObject;
                fish.GetComponent<Enemy>().SetSpeed(currentSpeedGameLevelFish);
                fish.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                itemSpawner.enemies.Add(fish);
                Destroy(fish, Random.Range(fishAliveTime / 2, fishAliveTime));
            }
        }
    }

    void HandleWhaleEnemy() {
        if (Time.time > nextWhaleSpawn) {
            Vector3 spawnedPosition;
            Vector3 spriteDirection;
            nextWhaleSpawn = Time.time + spawnWhaleInterval;
            float x = Random.Range(-planeLimit, planeLimit);
            float y = Random.Range(-planeLimit, planeLimit);
            float randomSide = Random.Range(-1, 2); // 0 is left, 1 is right

            if (randomSide == 0) {
                spawnedPosition = new Vector3(-x, y, whaleLimit);
                spriteDirection = Vector3.forward;
            }
            else {
                spawnedPosition = new Vector3(x, y, whaleLimit);
                spriteDirection = Vector3.back;
            }
            currentWhale = Instantiate(WhalePrefab, spawnedPosition, Quaternion.LookRotation(spriteDirection)) as GameObject;
            currentWhale.GetComponent<Enemy>().SetSpeed(currentSpeedGameLevelWhale);
            itemSpawner.enemies.Add(currentWhale);
            Destroy(currentWhale, whaleAliveTime);
        }
    }

    public void RemoveEnemy(GameObject enemy) {
        int index = itemSpawner.enemies.IndexOf(enemy);
        itemSpawner.enemies.RemoveAt(index);
    }

    public void DeleteAll() {
        if (itemSpawner.enemies.Count != 0) {
            for (int i = 0; i < itemSpawner.enemies.Count; i++) {
                Destroy(itemSpawner.enemies[i]);
            }
            itemSpawner.enemies.Clear();
        }
    }
}
