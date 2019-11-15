using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour {
    [SerializeField]
    float waterLevel = 0.0F;

    PlayerController player = null;
    ParticleSystem bubbles = null;
    public bool isUnderwater;
    Color normalColor;
    Color underwaterColor;

    void Start() {
        normalColor = new Color(0.5F, 0.5F, 0.5F, 0.5F);
        underwaterColor = new Color(0.22F, 0.65F, 0.77F, 0.5F);
        player = GetComponent<PlayerController>();
        bubbles = GameObject.Find("Bubbles").GetComponent<ParticleSystem>();
        GameObject.Find("CausticEffectProjector").GetComponent<Projector>().enabled = false; // No caustic when starting
    }

    void Update() {
        InWater();  
    }

    private void InWater() {
        // Cube is 25, 0 center, -25 bottom
        if (transform.position.y < waterLevel)
            isUnderwater = true;
        else
            isUnderwater = false;
        if (isUnderwater)
            SetUnderwater();
        else
            SetNormal();
    }

    private void SetUnderwater() {
        RenderSettings.fogColor = underwaterColor;
        //RenderSettings.fogDensity = 0.030F; // Will have to change
        player.inWater = true;
        bubbles.Play();
        GameObject.Find("CausticEffectProjector").GetComponent<Projector>().enabled = true;
    }

    private void SetNormal() {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.0005F;
        player.inWater = false;
        bubbles.Stop();
        GameObject.Find("CausticEffectProjector").GetComponent<Projector>().enabled = false;
    }
}
