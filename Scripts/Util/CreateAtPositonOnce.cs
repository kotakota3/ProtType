using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateAtPositonOnce : MonoBehaviour
{
	public Transform[ ] createPositions;
	public GameObject[ ] prefubs;

	int positionIndex;
	List<int> IndexList = new List<int> ();

	void Start ()
	{
		while (true)
		{
			positionIndex = Random.Range (0, createPositions.Length);
			if (IndexList.Contains (positionIndex))
				continue;
			
			IndexList.Add (positionIndex);

			if (IndexList.Count == 2)
				break;
		}

		for (int i = 0; i < prefubs.Length; i++)
		{
			GameObject go = (GameObject)Instantiate (prefubs [i], Vector3.zero, createPositions [IndexList [i]].rotation);
			go.transform.SetParent (createPositions [IndexList [i]], false);
		}
	}
}
