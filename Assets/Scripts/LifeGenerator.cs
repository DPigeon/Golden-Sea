using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGenerator : MonoBehaviour {
    [SerializeField]
    RawImage Life = null;

    GameObject canvas;

    public List<RawImage> lives = new List<RawImage>();

    void Start() {
        canvas = GameObject.Find("UI");
        Generate();
    }

    void Update() {
    }

    public void Generate() {
        Vector3 position1 = new Vector3(28.5F, -66.0F, 0F);
        Vector3 position2 = new Vector3(68.5F, -66.0F, 0F);
        RawImage life1 = Instantiate(Life) as RawImage;
        life1.transform.SetParent(canvas.transform, false);
        life1.rectTransform.anchoredPosition = position1;
        RawImage life2 = Instantiate(Life) as RawImage;
        life2.transform.SetParent(canvas.transform, false);
        life2.rectTransform.anchoredPosition = position2;
        lives.Add(life1);
        lives.Add(life2);
    }

    public void GenerateLivesAfterLevelUp() {
        // We remove twice and add new lives.
        RemoveLife();
        RemoveLife();
        Generate();
    }

    public void RemoveLife() {
        if (lives.Count != 0) {
            Destroy(lives[lives.Count - 1]);
            lives.RemoveAt(lives.Count - 1);
        }
    }
}
