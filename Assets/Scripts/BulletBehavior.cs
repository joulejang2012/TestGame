using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {
	
	private float speed, bulletLifeSpan, startTime;
	private Vector3 lookVector;

	// Use this for initialization
	void Start () {
		speed = 1.0f;
		bulletLifeSpan = 3.0f;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	public void Draw () {
		InvokeRepeating("Move", 0.0f, 0.05f);
		InvokeRepeating("CheckLifeSpan", 0.0f, 1.0f);
	}

	void Move() 
	{
		this.gameObject.transform.position = this.gameObject.transform.position + speed * lookVector;
	}

	void CheckLifeSpan() 
	{
		if (Time.time - startTime >= bulletLifeSpan) 
		{
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision collision) {
		Destroy (this.gameObject);
	}

	public void setLookVector(Vector3 l)
	{
		lookVector = l;
	}
}
