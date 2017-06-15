using UnityEngine;
using System.Collections;

public class FlyingCarpet : MonoBehaviour
{
	public float width;
	public float speed;
	public float waitTime;
	public float lightRange = 10f;

	Vector3 startPosition;
	Vector3 targetPosition;
	IEnumerator routine;
	GameObject spotLight;
	Transform player;
	bool back;
	bool onPlayer;

	public enum Mode
	{
		X,
		Y,
		Z
	}

	public Mode mode;

	void Start ()
	{
		spotLight = transform.Find ("Spotlight").gameObject;
		player = GameObject.FindWithTag ("Player").transform;
		startPosition = transform.position;
	}

	void Update ()
	{
		if (!onPlayer && Mathf.Sign (transform.position.z - player.position.z) == -1)
		{
			spotLight.SetActive (false);
		}
		else if (!onPlayer && (transform.position - player.position).sqrMagnitude < lightRange * lightRange)
		{
			spotLight.SetActive (true);
		}

		if (!back)
			return;
		if (transform.position == startPosition)
		{
			back = false;
		}
		transform.position = Vector3.MoveTowards (transform.position, startPosition, Time.deltaTime * speed);
	}

	IEnumerator Flying ()
	{
		while (Application.isPlaying)
		{
			yield return new WaitForSeconds (waitTime);
			switch (mode)
			{
				case Mode.X:
					targetPosition = startPosition + transform.right * width;
					while (transform.position != targetPosition)
					{
						transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * speed);
						yield return null;
					}
					yield return new WaitForSeconds (waitTime);
					targetPosition = targetPosition + -transform.right * width;
					while (transform.position != targetPosition)
					{
						transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * speed);
						yield return null;
					}
					break;

				case Mode.Y:
					targetPosition = startPosition + transform.up * width;
					while (transform.position != targetPosition)
					{
						transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * speed);
						yield return null;
					}
					yield return new WaitForSeconds (waitTime);
					targetPosition = targetPosition + -transform.up * width;
					while (transform.position != targetPosition)
					{
						transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * speed);
						yield return null;
					}
					break;

				case Mode.Z:
					targetPosition = startPosition + transform.forward * width;
					while (transform.position != targetPosition)
					{
						transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * speed);
						yield return null;
					}
					yield return new WaitForSeconds (waitTime);
					targetPosition = targetPosition + -transform.forward * width;
					while (transform.position != targetPosition)
					{
						transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * speed);
						yield return null;
					}
					break;
			}
		}
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			onPlayer = true;
			other.gameObject.transform.SetParent (transform);
			CancelInvoke ();
			routine = Flying ();
			StartCoroutine (routine);
			spotLight.SetActive (false);
		}
	}

	void OnCollisionExit (Collision other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			onPlayer = false;
			other.gameObject.transform.parent = null;
			StopCoroutine (routine);
			Invoke ("DelayBack", waitTime);
		}
	}

	void DelayBack ()
	{
		back = true;
	}
}
