using UnityEngine;
using System.Collections;

public class ActiveChecker : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player"))
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild (i).gameObject.SetActive (true);
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.CompareTag ("Player"))
		{
			if (other.GetComponent<PlayerMovement> ().isDead)
				return;
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild (i).gameObject.SetActive (false);
			}
		}
	}
}
