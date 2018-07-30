using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    Rigidbody playerRigidbody;
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;


    void Update() {
        if(Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        playerRigidbody = GetComponent<Rigidbody>();

        playerRigidbody.velocity = movement * speed;
        playerRigidbody.position = new Vector3 (Mathf.Clamp(playerRigidbody.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(playerRigidbody.position.z, boundary.zMin, boundary.zMax));
        playerRigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, playerRigidbody.velocity.x * -tilt);
    }
}
