using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
    [SerializeField]
    public GameObject thirdPersonCamera ; // 0
    [SerializeField]
    public GameObject firstPersonCamera; // 1

    public int cameraMode;

    void Start() {
        cameraMode = 0;
    }

    void Update() {
        if (Input.GetButtonDown("CameraSwitch")) {
            if (cameraMode == 1)
                cameraMode = 0;
            else
                cameraMode += 1;
        }
        StartCoroutine(CameraChange());
    }

    IEnumerator CameraChange() { // For the coroutine
        yield return new WaitForSeconds(0.01F);
        if (cameraMode == 0) {
            thirdPersonCamera.SetActive(true);
            thirdPersonCamera.GetComponent<AudioListener>().enabled = true;
            firstPersonCamera.SetActive(false);
            firstPersonCamera.GetComponent<AudioListener>().enabled = false;
        }
        if (cameraMode == 1) {
            thirdPersonCamera.SetActive(false);
            thirdPersonCamera.GetComponent<AudioListener>().enabled = false;
            firstPersonCamera.SetActive(true);
            firstPersonCamera.GetComponent<AudioListener>().enabled = true;
        }
    }
}
