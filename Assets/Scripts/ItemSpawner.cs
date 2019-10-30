﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    [SerializeField]
    GameObject SmallGoldenBarPrefab = null;
    [SerializeField]
    GameObject MediumGoldenBarPrefab = null;
    [SerializeField]
    GameObject GoldenLotPrefab = null;
    /*[SerializeField]
    GameObject NitroTankPrefab = null;*/

    public bool variantSpecial;

    //Boat boat;
    Vector3 spawnedPosition;
    Vector3 rotation;
    float randomX;
    float randomZ;
    float coordsX = 25.0F;
    float coordsZ = 25.0F;
    float spawnRate = 2F;
    float nextSpawn = 0.0F;
    float nextNitroSpawn = 0.0F;
    float nitroSpawnRate;
    float randomItem; // Either 0 (small bar), 1 (medium bar) or 2 (bag)
    float aliveSmallBarTime = 9.3F;
    float aliveMediumBarTime = 7.7f;
    float aliveBagTime = 4.5f;
    float aliveNitroTankTime = 4.0f;
    public List<GameObject> enemies = new List<GameObject>(); // Enemies stored here

    void Start() {
        //boat = GameObject.Find("Boat").GetComponent<Boat>();
        //variantSpecial = true;
        //variantSpecial = ModeSelection.mode;
        VariantVariationSpawn();
    }

    void Update() {
        // Random place in plane XZ within Y = -25
        VariantVariationSpawn();
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            spawnRate = Random.Range(2, 6);
            randomX = Random.Range(-coordsX, coordsX);
            randomZ = Random.Range(-coordsZ, coordsZ);
            spawnedPosition = new Vector3(randomX, -25, randomZ);
            float randomItem = Random.Range(-1, 3);

            if (randomItem == 0) {
                GameObject smallBar = Instantiate(SmallGoldenBarPrefab, spawnedPosition, Quaternion.identity) as GameObject;
                Destroy(smallBar, aliveSmallBarTime); // Destroy within a delay
            }
            else if (randomItem == 1) {
                GameObject mediumBar = Instantiate(MediumGoldenBarPrefab, spawnedPosition, Quaternion.identity) as GameObject;
                Destroy(mediumBar, aliveMediumBarTime);
            }
            else if (randomItem == 2) {
                GameObject bag = Instantiate(GoldenLotPrefab, spawnedPosition, Quaternion.identity) as GameObject;
                Destroy(bag, aliveBagTime);
            }

            /* Variant Special Version */
            if (variantSpecial) {
                if (Time.time > nextNitroSpawn) {
                    nextNitroSpawn = Time.time + nitroSpawnRate;
                    float x = Random.Range(-coordsX, coordsX);
                    float y = Random.Range(-coordsX, coordsX);
                    float z = Random.Range(-coordsZ, coordsZ);
                    Vector3 nitroSpawn = new Vector3(x, y, z);
                    /*GameObject nitroTank = Instantiate(NitroTankPrefab, nitroSpawn, Quaternion.identity) as GameObject;
                    Destroy(nitroTank, aliveNitroTankTime);*/
                }
            }
        }
    }

    private void VariantVariationSpawn() {
        if (variantSpecial)
            nitroSpawnRate = Random.Range(3, 5); // Spawns in between 10 to 30 seconds
    }

}
