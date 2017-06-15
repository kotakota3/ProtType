using UnityEngine;
using System.Collections;

public class OperationCanvas : MonoBehaviour
{
	public Camera mainCamera;

	void Start ()
	{
		transform.rotation = mainCamera.transform.rotation;
	}
}

