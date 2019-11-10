using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour {
    [SerializeField]
    int maxOxygen;
    int index;
    int barSize = 7;

    float nextTimer = 0.0F;
    float timer = 1.5F;

    RawImage[] oxygenBar; // Array of portions of the sword bar

    void Start() {
        oxygenBar = transform.Find("Bar").GetComponentsInChildren<RawImage>(); 
        maxOxygen = oxygenBar.Length;
        index = oxygenBar.Length - 1;
    }

    void Update() {
        Underwater();
    }

    public void Underwater() {
        if (FindObjectOfType<Underwater>().isUnderwater)
            DecreaseOxygen(1);
        //else
            //IncreaseOxygen(1);
    }

    public void IncreaseOxygen(int indexPosition) {
        for (int i = 0; i < indexPosition; i++) {
            if (index < maxOxygen) {
                oxygenBar[index].enabled = true;
                index++;
            }
        }
    }

    public void DecreaseOxygen(int indexPosition) {
        for (int i = 0; i < indexPosition; i++) {
            if (index >= 0) {
                oxygenBar[barSize - index].enabled = false;
                index--;
            }
        }
    }

}
