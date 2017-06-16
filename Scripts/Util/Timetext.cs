using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timetext : MonoBehaviour
{
	public GameObject flying_Carpet;
	Text text;
	float timer;

	void Start ()
	{
		text = GetComponent <Text> ();
	}

	void Update ()
	{
		timer += Time.deltaTime;
		text.text = timer.ToString ("f1");
		if (timer > 60.0f)
			flying_Carpet.SetActive (true);
	}
}
