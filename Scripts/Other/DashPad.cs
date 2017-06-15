using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DashPad : MonoBehaviour
{
	public float power;
	public AudioClip dashPadClip;
	public List<GameObject> goList = new List<GameObject> ();

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player") || other.CompareTag ("Follower")) {
			if (goList.Contains (other.gameObject)) {
				return;
			} else {
				goList.Add (other.gameObject);
				other.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				other.GetComponent<Rigidbody> ().AddForce (-transform.forward * power, ForceMode.VelocityChange);
				SoundManager.instance.PlaySingle (dashPadClip);
				Invoke ("ListClear", 0.5f);
			}
		}
	}

	void ListClear ()
	{
		goList.Clear ();
	}
}
