using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    [SerializeField]
    GameObject SmallGoldenBarPrefab = null;
    [SerializeField]
    GameObject MediumGoldenBarPrefab = null;
    [SerializeField]
    GameObject GoldenLotPrefab = null;
    [SerializeField]
    GameObject BubbleOxygenTankPrefab = null;
    [SerializeField]
    GameObject OxygenTankPrefab = null;
    [SerializeField]
    GameObject GoldenOxygenTankPrefab = null;

    public bool variantSpecial;

    Vector3 spawnedPosition;
    Vector3 rotation;
    float randomX;
    float randomY;
    float randomZ;
    float coordsX = 25.0F;
    float coordsY = 20.0F; // For the oxygen tanks
    float coordsZ = 25.0F;
    float spawnRate = 2F;
    float nextSpawn = 0.0F;
    float nextNitroSpawn = 0.0F;
    float nitroSpawnRate;
    float randomItem; // Either 0 (small bar), 1 (medium bar) or 2 (bag)
    float nextTankSpawn = 0.0F;
    float spawnTankRate = 2.0F;
    float randomTank;
    float aliveSmallBarTime = 18.3F;
    float aliveMediumBarTime = 14.7f;
    float aliveBagTime = 8.5f;
    float aliveNitroTankTime = 30.0f; // 10 before
    float aliveBubbleOxygenTank = 12.0F;
    float aliveOxygenTank = 16.0F;
    public List<GameObject> enemies = new List<GameObject>(); // Enemies stored here

    void Start() {
        //variantSpecial = true;
        variantSpecial = ModeSelection.mode;
        VariantVariationSpawn();
    }

    void Update() {
        // Random place in plane XZ within Y = -25
        VariantVariationSpawn();
        HandleGoldenItems();
        HandleVariantVersion();
        HandleOxygenTank();
    }

    private void HandleGoldenItems() {
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            spawnRate = Random.Range(2, 6);
            randomX = Random.Range(-coordsX, coordsX);
            randomZ = Random.Range(-coordsZ, coordsZ);
            spawnedPosition = new Vector3(randomX, -24.9F, randomZ);
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
                Vector3 rotation = new Vector3(90, 0, 0);
                GameObject bag = Instantiate(GoldenLotPrefab, spawnedPosition, Quaternion.Euler(rotation)) as GameObject;
                Destroy(bag, aliveBagTime);
            }
        }
    }

    private void HandleVariantVersion() {
        /* Variant Special Version */
        if (variantSpecial) {
            if (Time.time > nextNitroSpawn) {
                nextNitroSpawn = Time.time + nitroSpawnRate;
                float x = Random.Range(-coordsX, coordsX);
                float y = Random.Range(-coordsX, coordsX);
                float z = Random.Range(-coordsZ, coordsZ);
                Vector3 nitroSpawn = new Vector3(x, y, z);
                GameObject nitroTank = Instantiate(GoldenOxygenTankPrefab, nitroSpawn, Quaternion.identity) as GameObject;
                Destroy(nitroTank, aliveNitroTankTime);
            }
        }
    }

    private void HandleOxygenTank() {
        if (Time.time > nextTankSpawn) {
            nextTankSpawn = Time.time + spawnTankRate;
            randomX = Random.Range(-coordsX, coordsX);
            randomY = Random.Range(-coordsY, coordsY);
            randomZ = Random.Range(-coordsZ, coordsZ);
            spawnedPosition = new Vector3(randomX, randomY, randomZ);
            float randomItem = Random.Range(-1, 2);

            if (randomItem == 0) { // Dirty tank
                GameObject bubbleOxygenTank = Instantiate(BubbleOxygenTankPrefab, spawnedPosition, Quaternion.identity) as GameObject;
                Destroy(bubbleOxygenTank, aliveBubbleOxygenTank);
            }
            else if (randomItem == 1) { // Dirty tank
                GameObject oxygenTank = Instantiate(OxygenTankPrefab, spawnedPosition, Quaternion.identity) as GameObject;
                Destroy(oxygenTank, aliveOxygenTank);
            }
        }
    }

    private void VariantVariationSpawn() {
        if (variantSpecial)
            nitroSpawnRate = Random.Range(4, 6);
    }
}
