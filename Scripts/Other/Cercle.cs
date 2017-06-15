using UnityEngine;
using System.Collections;

public class Cercle : MonoBehaviour
{
	public float speed = 2f;
	public float width = 4f;
	public float height = 4f;
	Vector3 startPosition;
	Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		startPosition = rb.position;
	}

	void FixedUpdate ()
	{
		Vector3 movement;
		movement.x = Mathf.Cos (Time.time * speed) * width;
		movement.y = 0f;
		movement.z = Mathf.Sin (Time.time * speed) * height;
		rb.position = startPosition + movement;
	}
}
