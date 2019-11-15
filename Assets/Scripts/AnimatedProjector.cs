using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedProjector : MonoBehaviour {
    [SerializeField]
    public float FPS = 30.0F;
    [SerializeField]
    public Texture2D[] frames;

    int frameIndex;
    Projector projector;

    void Start() {
        projector = GetComponent<Projector>();
        NextFrame();
        InvokeRepeating("NextFrame", 1 / FPS, 1 / FPS);
    }

    void Update() {
        
    }

    private void NextFrame() {
        projector.material.SetTexture("_ShadowTex", frames[frameIndex]);
        frameIndex = (frameIndex + 1) % frames.Length;
    }

}
