using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManuever : MonoBehaviour {

    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 manueverTime;
    public Vector2 manueverWait;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManuever;
    private Rigidbody rigidbody;
    
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        currentSpeed = rigidbody.velocity.z;
        StartCoroutine(Evade());
	}

    IEnumerator Evade() {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true) {
            targetManuever = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(manueverTime.x, manueverTime.y));
            targetManuever = 0;
            yield return new WaitForSeconds(Random.Range(manueverWait.x, manueverWait.y));
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float newManuever = Mathf.MoveTowards(rigidbody.velocity.x, targetManuever, Time.deltaTime * smoothing);
        rigidbody.velocity = new Vector3(newManuever, 0.0f, currentSpeed);
        rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax));

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
}
