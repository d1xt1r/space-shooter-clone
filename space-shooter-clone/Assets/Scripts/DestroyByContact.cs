﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject asteroidExplosion;
    public GameObject playerExplosion;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Boundary") {
            return;
        }
        Instantiate(asteroidExplosion, transform.position, transform.rotation);
        if(other.tag == "Player") {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }
        Destroy(other.gameObject); // destorys the laser bolt
        Destroy(gameObject); // destorys the asteriod
    }
}
