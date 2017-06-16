using UnityEngine;
using System.Collections;

public class BothWay : MonoBehaviour
{
	public float width;
	public float speed;
	Rigidbody rb;
	Vector3 startPosition;

	public enum Mode
	{
		X,
		Y,
		Z
	}

	public Mode mode;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		startPosition = rb.position;
	}

	void FixedUpdate ()
	{
		float movement = Mathf.Sin (Time.time * speed) * width;
		switch (mode)
		{
			case Mode.X:
				rb.position = startPosition + new Vector3 (movement, 0.0f, 0.0f);
				break;
			case Mode.Y:
				rb.position = startPosition + new Vector3 (0.0f, movement, 0.0f);
				break;
			case Mode.Z:
				rb.position = startPosition + new Vector3 (0.0f, 0.0f, movement);
				break;
		}
	}
}
