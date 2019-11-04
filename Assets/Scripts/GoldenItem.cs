using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenItem : MonoBehaviour {
    [SerializeField]
    RawImage SmallGoldenBar = null;
    [SerializeField]
    RawImage MediumGoldenBar = null;
    [SerializeField]
    RawImage GoldenLot = null;
    /*[SerializeField]
    GameObject NitroTank = null;*/

    PlayerController player;
    Boat boat;
    GameObject canvas;

    Vector3 collectedItemPosition;
    Vector2 nitroCollectedPosition = new Vector3(3.00F, 4.57F, 0.0F);
    float speedDecreaseSmallBar = 1.0F;
    float speedDecreaseMediumBar = 1.5F;
    float speedDecreaseLot = 2.5F;
    int scoreSmallBar = 1;
    int scoreMediumBar = 2;
    int scoreLot = 10;

    void Start() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        boat = GameObject.Find("Boat").GetComponent<Boat>();
        collectedItemPosition = new Vector3(137.0F, 176.0F, 0.0F);
        canvas = GameObject.Find("UI");
    }

    /* 
     * If golden item collides with Player, then we display a bar on top (shows that he has it in his inventory)
     * We look if our item collected inventory is empty as well
     */
    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "Player" && boat.itemsCollected.Count == 0) {
            if (gameObject.name == "SmallGoldenBar(Clone)") {
                // Display bar on top
                RawImage inventorySmallBar = Instantiate(SmallGoldenBar, collectedItemPosition, Quaternion.identity) as RawImage;
                inventorySmallBar.transform.SetParent(canvas.transform, false);
                inventorySmallBar.rectTransform.anchoredPosition = collectedItemPosition;

                boat.AddItem(scoreSmallBar, inventorySmallBar);
                player.DecreaseSpeed(speedDecreaseSmallBar);
                Destroy(gameObject);
            }
            if (gameObject.name == "MediumGoldenBar(Clone)") {
                // Display bar on top
                RawImage inventoryMediumBar = Instantiate(MediumGoldenBar, collectedItemPosition, Quaternion.identity) as RawImage;
                inventoryMediumBar.transform.SetParent(canvas.transform, false);
                inventoryMediumBar.rectTransform.anchoredPosition = collectedItemPosition;

                boat.AddItem(scoreMediumBar, inventoryMediumBar);
                player.DecreaseSpeed(speedDecreaseMediumBar);
                Destroy(gameObject);
            }
            if (gameObject.name == "GoldenLot(Clone)") {
                // Display bar on top
                RawImage inventoryGoldenLot = Instantiate(GoldenLot, collectedItemPosition, Quaternion.identity) as RawImage;
                inventoryGoldenLot.transform.SetParent(canvas.transform, false);
                inventoryGoldenLot.rectTransform.anchoredPosition = collectedItemPosition;

                boat.AddItem(scoreLot, inventoryGoldenLot);
                player.DecreaseSpeed(speedDecreaseLot);
                Destroy(gameObject);
            }
            /*if (gameObject.name == "Nitro Tank(Clone)" && player.nitroTankInventory.Count == 0) {
                player.ActivateNitro();
                GameObject nitroTank = Instantiate(NitroTank, nitroCollectedPosition, Quaternion.identity) as GameObject;
                player.nitroTankInventory.Add(nitroTank);
                Destroy(gameObject);
            }*/
        }
    }
}
