using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    string horizontalInput = "";
    [SerializeField]
    string verticalInput = "";
    [SerializeField]
    float movementSpeed = 0.0F;
    [SerializeField]
    Camera cameraView = null;
    [SerializeField]
    GameObject ShinyFigurinePrefab = null;

    float dragVariable = 1.0F;

    bool isHurt;
    bool isGrounded;

    bool dead;
    float deadTimer;
    float deadDuration = 3.0F;

    float nextForce = 0.0F;
    float swimmingDuration = 0.2F;
    bool stopForce;
    bool swimming;

    bool throwing;
    float throwingTimer;
    float projectileDuration = 5.0F;
    float throwingDuration = 1.0F;

    Rigidbody rigidbody = null;
    Boat boat = null;
    LifeGenerator lifeGenerator = null;
	ParticleSystem nitroEffect = null;

    public bool inWater;
    public bool nitroTankInventory;
    public bool nitroActive;
    float nitroTimer;
    float nitroDuration = 3.0F; // 3 seconds
    bool nitroActivateTimer;

    AudioSource throwFiguringSound;
    AudioSource playerHurtSound;
    AudioSource playerDieSound;
    AudioSource collectNitroTankSound;
    AudioSource nitroTankSound;
    AudioSource waterSurface;
    AudioSource swimSubmarineSound;

    void Start() {
        boat = GameObject.Find("Boat").GetComponent<Boat>();
        lifeGenerator = GameObject.Find("LifeHandler").GetComponent<LifeGenerator>();
        rigidbody = GetComponent<Rigidbody>();
		nitroEffect = gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>();
		nitroEffect.Stop();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        throwFiguringSound = audioSources[0];
        playerHurtSound = audioSources[1];
        playerDieSound = audioSources[2];
        collectNitroTankSound = audioSources[3];
        nitroTankSound = audioSources[4];
        waterSurface = audioSources[5];
        swimSubmarineSound = audioSources[6];
    }

    void Update() {
        PlayerControls();
        ThrowFigurine();
        CheckBoundaries();
        HandleTimers();
    }

    private void PlayerControls() {
        if (inWater && !dead)
            rigidbody.useGravity = false;
        else
            rigidbody.useGravity = true;
        
        // When character is grounded, move normally
        if (isGrounded) {
            float horizontal = Input.GetAxis(horizontalInput) * movementSpeed;
            float vertical = Input.GetAxis(verticalInput) * movementSpeed;
            Vector3 right = transform.right * horizontal;
            Vector3 forward = transform.forward * vertical;
            Vector3 movement = new Vector3(horizontal, 0.0F, vertical);
            rigidbody.AddForce(movement);
        }

        if (Input.GetButtonDown("Swim") && !swimming) {
            cameraView = GameObject.Find("PlayerCamera").GetComponent<Camera>(); // Update camera for 1st & 3rd
            Vector3 constantForce = cameraView.transform.forward * 2.0F;
            rigidbody.AddForce(constantForce * Time.deltaTime * movementSpeed, ForceMode.Impulse);
            swimming = true;
            stopForce = false;
            swimSubmarineSound.Play();
        }

        // Drag
        rigidbody.drag = rigidbody.velocity.magnitude * dragVariable;

        if (Input.GetButtonUp("Swim")) {
            stopForce = true;
        }
        if (Input.GetButton("Pause")) {
            FindObjectOfType<GameInterfaces>().PauseTheGame();
        }
    }

    private void ThrowFigurine() {
        if (Input.GetButtonDown("Throw") && !throwing) {
            throwing = true;
            GameObject shinyFigurine = Instantiate(ShinyFigurinePrefab, transform.position, Quaternion.identity) as GameObject;
            throwFiguringSound.Play();
            Destroy(shinyFigurine, projectileDuration);
        }
    }

    private void CheckBoundaries() {
        // | Side1 - Side2 | in Z
        float limit = 22.0F;
        if (transform.position.z < -limit)
            transform.position = new Vector3(transform.position.x, transform.position.y, limit);
        else if (transform.position.z > limit)
            transform.position = new Vector3(transform.position.x, transform.position.y, -limit);
        // | Side3 - Side4 | in X
        else if (transform.position.x < -limit)
            transform.position = new Vector3(limit, transform.position.y, transform.position.z);
        else if (transform.position.x > limit)
            transform.position = new Vector3(-limit, transform.position.y, transform.position.z);
        else if (transform.position.y > 25.0F || transform.position.y < -25.0F) // When on top of water, don't go out
            rigidbody.velocity = Vector3.zero;
        else if (transform.position.y > 24.0F)
            waterSurface.Play();
    }

    private void HandleTimers() {
        if (dead) {
            deadTimer += Time.deltaTime;
            if (deadTimer > deadDuration) {
                dead = false;
                deadTimer = 0.0f;
                //FindObjectOfType<GameInterfaces>().EndTheGame();
            }
        }

        if (stopForce) {
            nextForce += Time.deltaTime;
            if (nextForce > swimmingDuration) {
                swimming = false;
                nextForce = 0.0F;
            }
        }

        if (throwing) {
            throwingTimer += Time.deltaTime;
            if (throwingTimer > throwingDuration) {
                throwing = false;
                throwingTimer = 0.0F;
            }
        }

        if (nitroActive && Input.GetButton("Boost") && !dead) {
            nitroActive = false;
            nitroActivateTimer = true;
            nitroTankInventory = false;
            // Invicible power
            nitroTankSound.Play();
			nitroEffect.Play();
            IncreaseSpeed(3000.0F);
        }
        if (nitroActivateTimer)
            nitroTimer += Time.deltaTime;
        if (nitroTimer >= nitroDuration && !dead) {
            ResetNitroTankEffect();
        }

    }

    private void ResetNitroTankEffect() {
        nitroActivateTimer = false;
        nitroTimer = 0.0f;
        ResetSpeed();
		nitroEffect.Stop();
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "Fish(Clone)" || collider.gameObject.name == "Whale(Clone)" || collider.gameObject.name == "WhaleProjectile(Clone)") {
            if (collider.gameObject.name == "WhaleProjectile(Clone)")
                Destroy(collider.gameObject);
            if (lifeGenerator.lives.Count == 2) {
                lifeGenerator.RemoveLife();
                isHurt = true;
                playerHurtSound.Play();
            }
            else if (lifeGenerator.lives.Count == 1) {
                if (boat.items.Count != 0) {
                    Destroy(boat.items[boat.items.Count - 1]); // If any item in player inventory, destroy
                    boat.items.Clear();
                    boat.itemsCollected.Clear();
                    ResetSpeed();
                }
                lifeGenerator.RemoveLife();
                Die();
            }
        }
    }

    private void Die() {
        playerDieSound.Play();
        dead = true;
        rigidbody.useGravity = true; // Drowns in water
    }

    public void IncreaseSpeed(float number) {
        movementSpeed = movementSpeed + number;
    }

    public void DecreaseSpeed(float number) {
        movementSpeed = movementSpeed - number;
    }

    public void ResetSpeed() {
        movementSpeed = 500.0F;
    }

    public void ActivateNitro() {
        nitroActive = true;
        collectNitroTankSound.Play();
    }
}
