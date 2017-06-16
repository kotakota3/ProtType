using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	GameController gc;

	void Start ()
	{
		gc = GameObject.FindWithTag ("GameController").GetComponent <GameController> ();
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			gc.Goal ();
		}
	}
}
