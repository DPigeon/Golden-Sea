using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithoutFog : MonoBehaviour {
    bool fogInScene;

    private void Start() {
        fogInScene = RenderSettings.fog;
    }

    private void OnPreRender() {
        RenderSettings.fog = false;
    }
    private void OnPostRender() {
        RenderSettings.fog = fogInScene;
    }
}
