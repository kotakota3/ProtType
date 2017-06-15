using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
	public GameObject soundManager;

	void Awake ()
	{
		if (SoundManager.instance == null) {
			Instantiate (soundManager);
		}
	}
}
