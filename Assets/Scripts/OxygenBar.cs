using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour {
    [SerializeField]
    int maxOxygen;
    int index;
    int barSize = 7;

    float nextTimer = 5.0F;
    float timerRate = 5.0F;

    RawImage[] oxygenBar; // Array of portions of the sword bar

    void Start() {
        oxygenBar = transform.Find("Bar").GetComponentsInChildren<RawImage>(); 
        maxOxygen = oxygenBar.Length;
        index = oxygenBar.Length - 1;
    }

    void Update() {
        Underwater();
        //Debug.Log(index);
    }

    public void Underwater() {
        if (FindObjectOfType<Underwater>().isUnderwater) {
            if (Time.time > nextTimer) {
                nextTimer = Time.time + timerRate;
                DecreaseOxygen(1);
            }
        }
        else if (!FindObjectOfType<Underwater>().isUnderwater)
            RefillOxygen(8);
    }

    public void IncreaseOxygen(int indexPosition) {
        for (int i = 0; i < indexPosition; i++) {
            if (index < maxOxygen) {
                oxygenBar[barSize - index].enabled = true;
                index++;
            }
        }
    }

    public void RefillOxygen(int indexPosition) {
        for (int i = 0; i < indexPosition; i++) {
            if (index < maxOxygen) {
                oxygenBar[barSize - index].enabled = true;
                index++;
            }
        }
        index = oxygenBar.Length - 1;
    }

    public void DecreaseOxygen(int indexPosition) {
        for (int i = 0; i < indexPosition; i++) {
            if (index >= 0) {
                oxygenBar[barSize - index].enabled = false;
                if (index != 0)
                    index--;
            }
        }
    }
}
