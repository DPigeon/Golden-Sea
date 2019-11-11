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

    bool isHurt;

    bool dead;
    float deadTimer;
    float deadDuration = 3.0F;

    float nextForce = 0.0F;
    float swimmingDuration = 0.2F;
    bool stopForce;
    bool swimming;

    CharacterController playerController = null;
    Rigidbody rigidbody = null;
    Boat boat = null;
    LifeGenerator lifeGenerator = null;

    // Buoyancy variables
    float gravity = 0.0F;
    float gravityInWater = 2.0F / 1000F;
    float gravityOutsideWater = 9.81F / 100F;
    public bool inWater;

    void Start() {
        playerController = GetComponent<CharacterController>();
        boat = GameObject.Find("Boat").GetComponent<Boat>();
        lifeGenerator = GameObject.Find("LifeHandler").GetComponent<LifeGenerator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        PlayerMovement();
        ThrowFigurine();
        CheckBoundaries();
        HandleTimers();
    }

    private void PlayerMovement() {
        gravity -= IsInWater(inWater) * Time.deltaTime;

        // When character is grounded, move normally
        if (playerController.isGrounded) {
            float horizontal = Input.GetAxis(horizontalInput) * movementSpeed;
            float vertical = Input.GetAxis(verticalInput) * movementSpeed;
            Vector3 right = transform.right * horizontal;
            Vector3 forward = transform.forward * vertical;
            // Add fluid movement in water later here
            playerController.Move(right + forward);
        }

        Vector3 gravityMovement = new Vector3(0.0F, gravity, 0.0F);
        playerController.Move(gravityMovement); // Moves our component

        if (Input.GetButtonDown("Swim") && inWater && !swimming) {
            Vector3 constantForce = cameraView.transform.forward * 2.0F;
            playerController.Move(constantForce);
            gravity = 0.0F;
            swimming = true;
            stopForce = false;
        }
        if (Input.GetButtonUp("Swim") && inWater) {
            stopForce = true;
        }
        if (Input.GetKey(KeyCode.C) && inWater) {
            Vector3 constantForce = new Vector3(0.0F, 200.0F / 5000F, 0.0F);
            playerController.Move(constantForce);
            gravity = 0.0F; // Reseting gravity
        }
    }

    private void ThrowFigurine() {
        if (Input.GetButton("Throw")) {
            // Create new projectile
            // Aim them towards the lookAt vector of the pc
        }
    }

    public float IsInWater(bool inTheWater) {
        if (inTheWater) {
            return gravityInWater;
        }
        else {
            return gravityOutsideWater;
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
    }

    private void HandleTimers() {
        if (dead) {
            deadTimer += Time.deltaTime;
            if (deadTimer > deadDuration) {
                dead = false;
                deadTimer = 0.0f;
                //FindObjectOfType<GameOver>().EndTheGame();
            }
        }

        if (stopForce) {
            nextForce += Time.deltaTime;
            if (nextForce > swimmingDuration) {
                swimming = false;
                nextForce = 0.0F;
            }
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "Fish(Clone)" || collider.gameObject.name == "Whale(Clone)") {
            if (lifeGenerator.lives.Count == 2) {
                lifeGenerator.RemoveLife();
                isHurt = true;
                //hurtSound.Play();
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
        //dieSound.Play();
        dead = true;
        transform.localScale = new Vector3(0, 0, 0); // Hide player (deleted and dead)
        //bubbles.Stop();
    }

    public void IncreaseSpeed(float number) {
        movementSpeed = movementSpeed + number;
    }

    public void DecreaseSpeed(float number) {
        movementSpeed = movementSpeed - number;
    }

    public void ResetSpeed() {
        movementSpeed = 5.0F;
    }

}
