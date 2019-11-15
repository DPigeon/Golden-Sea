using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour {
    [SerializeField]
    int maxOxygen;
    int index;
    int barSize = 7;

    float nextTimer = 10.0F;
    float timerRate = 10.0F;

    RawImage[] oxygenBar; // Array of portions of the sword bar

    AudioSource barUp;
    AudioSource barDown;

    void Start() {
        oxygenBar = transform.Find("Bar").GetComponentsInChildren<RawImage>(); 
        maxOxygen = oxygenBar.Length;
        index = oxygenBar.Length - 1;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        barUp = audioSources[0];
        barDown = audioSources[1];
    }

    void Update() {
        Underwater();
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
        barUp.Play();
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
        nextTimer = Time.timeSinceLevelLoad; // Reset timer for the bar
    }

    public void DecreaseOxygen(int indexPosition) {
        barDown.Play();
        for (int i = 0; i < indexPosition; i++) {
            if (index >= 0) {
                oxygenBar[barSize - index].enabled = false;
                if (index == 0) // Dead game over
                    FindObjectOfType<GameInterfaces>().EndTheGame();
                if (index != 0)
                    index--;
            }
        }
    }
}
