using UnityEngine;
using System.Collections;

public class JumpPad : MonoBehaviour
{
	public float power;
	public AudioClip jumpClip;

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player") || other.CompareTag ("Follower"))
		{
			other.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			other.GetComponent<Rigidbody> ().AddForce (transform.up * power, ForceMode.VelocityChange);
			SoundManager.instance.PlayThreeD (jumpClip);
		}
	}
}
